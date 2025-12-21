using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DevTrivia.API.Infrastructure.Swagger;

public static class SwaggerConfiguration
{
    private const string SchemeId = "Bearer";

    public static void ConfigureSwagger(this SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new()
        {
            Title = "DevTrivia API",
            Version = "v1",
            Description = "API para o aplicativo DevTrivia - Autenticação com JWT"
        });

        // Adiciona suporte para autenticação JWT no Swagger (.NET 10)
        options.AddSecurityDefinition(SchemeId, new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "JWT Authorization header usando o esquema Bearer. Cole apenas o token (sem 'Bearer' prefix).",
            Name = "Authorization",
            In = ParameterLocation.Header
        });

        options.AddSecurityRequirement(document => 
            new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference(SchemeId, document)] = new List<string>([])
            });
    }
}
