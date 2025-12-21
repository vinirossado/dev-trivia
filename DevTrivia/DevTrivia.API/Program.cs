using System.Text;
using System.Text.Json;
using Azure.Identity;
using DevTrivia.API.Capabilities.User.Repositories;
using DevTrivia.API.Capabilities.User.Repositories.Interfaces;
using DevTrivia.API.Capabilities.User.Services;
using DevTrivia.API.Capabilities.User.Services.Interfaces;
using DevTrivia.API.Infrastructure.Swagger;
using DevTrivia.API.Migrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

using var loggerFactory = LoggerFactory.Create(loggingBuilder =>
{
    loggingBuilder.AddConfiguration(builder.Configuration.GetSection("Logging"));
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

var logger = loggerFactory.CreateLogger<Program>();

var keyVaultName = builder.Configuration["KeyVault:Vault"];

if (!string.IsNullOrWhiteSpace(keyVaultName))
{
    try
    {
        logger.LogInformation("Attempting to connect to Key Vault: {KeyVaultName}", keyVaultName);
        builder.Configuration.AddAzureKeyVault(
            new Uri($"https://{keyVaultName}.vault.azure.net/"),
            new DefaultAzureCredential());
        logger.LogInformation("Successfully connected to Key Vault: {KeyVaultName}", keyVaultName);
    }
    catch (Exception ex)
    {
        logger.LogWarning("Failed to connect to Key Vault '{KeyVaultName}': {Message}. Falling back to local configuration.",
            keyVaultName, ex.Message);
    }
}

var connectionString = builder.Configuration["Postgres--TriviaConnectionString"];

// LAST resort: local appsettings.json for development
if (string.IsNullOrEmpty(connectionString))
{
    logger.LogWarning("No connection string in Key Vault/App Settings. Using local appsettings.json (development only)");
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}

logger.LogInformation("PostgresSQL connection from Key Vault (Postgres--TriviaConnectionString): {Available}",
    !string.IsNullOrEmpty(connectionString));

// Add Database Context
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Database connection string is not configured.");
}

builder.Services.AddDbContext<TriviaDbContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorCodesToAdd: null);
    }));

// Register User Repositories and Services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// Add Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.ConfigureSwagger());

// Add TimeProvider
builder.Services.AddSingleton(TimeProvider.System);

// Configure JWT settings
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

// Validate JWT configuration
if (string.IsNullOrWhiteSpace(secretKey))
{
    // logger.LogError("JWT SecretKey is not configured. KeyVault: {KeyVault}, SecretKey: {SecretKey}",
    //     keyVaultName ?? "NOT SET",
    //     string.IsNullOrEmpty(secretKey) ? "EMPTY" : "HAS VALUE");

    // Try to read directly from different paths
    var altKey1 = builder.Configuration["JwtSettings:SecretKey"];
    var altKey2 = builder.Configuration["JwtSettings__SecretKey"];
    logger.LogError("Alternative reads - JwtSettings:SecretKey={Alt1}, JwtSettings__SecretKey={Alt2}",
        altKey1 != null ? "EXISTS" : "NULL",
        altKey2 != null ? "EXISTS" : "NULL");

    throw new InvalidOperationException(
        "JWT SecretKey is not configured. " +
        "Please set 'JwtSettings__SecretKey' in Azure App Settings or add 'JwtSettings--SecretKey' to Azure Key Vault. " +
        "The key must be at least 256 bits (32 characters) long.");
}

if (secretKey.Length < 32)
{
    logger.LogWarning("JWT SecretKey is too short ({Length} characters). Recommended minimum is 32 characters (256 bits).", secretKey.Length);
}

// Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"] ?? "DevTrivia",
        ValidAudience = jwtSettings["Audience"] ?? "DevTrivia.Users",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero // Remove the default 5-minute tolerance
    };

    // Enable detailed authentication errors in development
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception != null)
            {
                logger.LogError("Authentication failed: {Error}", context.Exception.Message);
            }
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            logger.LogInformation("Token validated successfully for user: {User}", 
                context.Principal?.Identity?.Name ?? "Unknown");
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            logger.LogWarning("Authentication challenge: {Error}", context.Error ?? "Unknown");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddMemoryCache();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSwiftUI", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Redirect root to Swagger
app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevTrivia API v1");
        c.RoutePrefix = "swagger";
    });
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowSwiftUI");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
