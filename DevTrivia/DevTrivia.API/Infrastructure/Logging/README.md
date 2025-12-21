# Compile-time Logging with Event IDs

## Overview

Este projeto utiliza **LoggerMessage Source Generators** do .NET para logging de alta performance com Event IDs estruturados.

## O que são Event IDs?

Event IDs são identificadores numéricos únicos para cada tipo de evento de log. Eles permitem:

- 🔍 **Filtrar logs específicos** em sistemas de monitoramento (Application Insights, Elastic, Datadog)
- 📈 **Criar alertas** baseados em eventos críticos
- 📊 **Análise de métricas** (ex: contar quantos logins falharam em 1 hora)
- 🐛 **Debugging** mais eficiente em produção
- 🎯 **Correlação de eventos** relacionados

## Event ID Convention (Microsoft Guidelines)

### Padrão de Numeração por Categoria

| Range | Categoria | Descrição | Exemplos |
|-------|-----------|-----------|----------|
| **1000-1999** | Authentication/Authorization | Eventos de autenticação e autorização | Login, Logout, Token generation, OAuth |
| **2000-2999** | CRUD Operations | Operações de Create, Read, Update, Delete | User created, Fetching data, Update success |
| **3000-3999** | Business Logic | Regras de negócio específicas | Payment processed, Order completed, Workflow state |
| **4000-4999** | Database/Infrastructure | Operações de infraestrutura | Database errors, Cache operations, Queue messages |
| **5000-5999** | Validation/Input | Validação de dados de entrada | Model validation, Request validation, Data format errors |
| **6000-6999** | External Services | Integração com serviços externos | API calls, Email sending, SMS, Third-party services |
| **7000-7999** | Security Events | Eventos de segurança | Brute force attempts, Unauthorized access, Permission denied |
| **8000-8999** | Performance | Eventos relacionados a performance | Slow queries, Timeouts, Resource exhaustion |
| **9000-9999** | Application Lifecycle | Eventos de ciclo de vida da aplicação | Startup, Shutdown, Configuration changes |

---

## Event IDs do DevTrivia API

### 1000-1999: User Authentication

| Event ID | Level | Event Name | Descrição |
|----------|-------|------------|-----------|
| 1001 | Information | UserLoginAttempt | Tentativa de login do usuário |
| 1002 | Information | UserLoginSuccessful | Login realizado com sucesso |
| 1003 | Warning | UserLoginFailed | Login falhou - credenciais inválidas |
| 1004 | Information | UserRegistered | Novo usuário registrado |
| 1005 | Warning | UserRegistrationEmailExists | Registro falhou - email já existe |

### 2000-2999: User CRUD Operations

| Event ID | Level | Event Name | Descrição |
|----------|-------|------------|-----------|
| 2001 | Information | FetchingUserById | Buscando usuário por ID |
| 2002 | Warning | UserNotFound | Usuário não encontrado |
| 2003 | Information | UpdatingUser | Atualizando dados do usuário |
| 2004 | Information | UserUpdated | Usuário atualizado com sucesso |
| 2005 | Information | DeletingUser | Deletando usuário |
| 2006 | Information | UserDeleted | Usuário deletado com sucesso |
| 2007 | Information | FetchingAllUsers | Buscando todos os usuários (paginado) |

### 3000-3999: JWT Token Operations

| Event ID | Level | Event Name | Descrição |
|----------|-------|------------|-----------|
| 3001 | Information | GeneratingJwtToken | Gerando token JWT para usuário |
| 3002 | Error | JwtTokenGenerationFailed | Falha ao gerar token JWT |

### 4000-4999: Database Operations

| Event ID | Level | Event Name | Descrição |
|----------|-------|------------|-----------|
| 4001 | Error | DatabaseError | Erro em operação de banco de dados |
| 4002 | Information | DatabaseOperationSuccess | Operação de banco concluída com sucesso |

### 5000-5999: Validation

| Event ID | Level | Event Name | Descrição |
|----------|-------|------------|-----------|
| 5001 | Warning | ValidationFailed | Validação de entrada falhou |
| 5002 | Warning | InvalidPasswordFormat | Formato de senha inválido |

---

## Como Usar

### 1. Definir LoggerMessage (LogMessages.cs)

```csharp
public static partial class LogMessages
{
    [LoggerMessage(
        EventId = 1001,
        Level = LogLevel.Information,
        Message = "User login attempt for email: {Email}")]
    public static partial void UserLoginAttempt(this ILogger logger, string email);
}
```

### 2. Usar no código

```csharp
public class UserService
{
    private readonly ILogger<UserService> _logger;

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        _logger.UserLoginAttempt(request.Email);
        
        // ... lógica de login
        
        if (loginSuccess)
        {
            _logger.UserLoginSuccessful(request.Email, user.Id);
        }
        else
        {
            _logger.UserLoginFailed(request.Email);
        }
    }
}
```

---

## Benefícios do Compile-time Logging

### Performance

- ✅ **Zero alocações** - Sem boxing/unboxing
- ✅ **Source generators** - Código gerado em tempo de compilação
- ✅ **Até 6x mais rápido** que logging tradicional
- ✅ **Menos pressão no GC**

### Type Safety

- ✅ **Compile-time checking** - Erros detectados antes da execução
- ✅ **IntelliSense** completo
- ✅ **Refactoring seguro**

### Exemplo de código gerado:

```csharp
// Você escreve:
_logger.UserLoginAttempt("user@example.com");

// O compilador gera (simplificado):
if (_logger.IsEnabled(LogLevel.Information))
{
    _logger.Log(
        LogLevel.Information,
        new EventId(1001, "UserLoginAttempt"),
        "User login attempt for email: user@example.com");
}
```

---

## Exemplos de Uso em Produção

### 1. Filtrar logs por Event ID

```bash
# Azure Application Insights (KQL)
traces
| where customDimensions.EventId == 1003
| summarize count() by bin(timestamp, 1h)

# Elastic/Kibana
event.code: 1003 AND @timestamp:[now-1h TO now]

# CloudWatch Logs Insights
fields @timestamp, @message
| filter eventId = 1003
| stats count() by bin(5m)
```

### 2. Criar alertas

```csharp
// Azure Monitor Alert Rule
// Trigger when more than 100 failed logins in 5 minutes
customDimensions.EventId == 1003
| summarize count() by bin(timestamp, 5m)
| where count_ > 100
```

### 3. Dashboards de métricas

```csharp
// Contar por tipo de evento
traces
| summarize count() by tostring(customDimensions.EventId)
| render piechart

// Taxa de sucesso de login
let total = traces | where customDimensions.EventId in (1002, 1003) | count;
let success = traces | where customDimensions.EventId == 1002 | count;
print SuccessRate = (success * 100.0) / total
```

### 4. Debugging em produção

```bash
# Encontrar todos os eventos relacionados a um usuário específico
grep "user@example.com" logs.txt | grep -E "EventId: (1001|1002|1003|2001|2003)"

# Trace de uma requisição completa
grep "CorrelationId: abc-123" logs.txt | sort
```

---

## Best Practices

### ✅ DO

- **Use Event IDs consistentes** - Mantenha o padrão de numeração
- **Agrupe por categoria** - Facilita organização e filtros
- **Documente todos os eventos** - Mantenha este README atualizado
- **Use níveis apropriados**:
  - `Information` - Fluxo normal da aplicação
  - `Warning` - Algo inesperado mas recuperável
  - `Error` - Falha que precisa atenção
  - `Critical` - Sistema em estado crítico
- **Inclua contexto relevante** - UserId, Email, RequestId, etc.

### ❌ DON'T

- **Não reutilize Event IDs** - Cada evento único deve ter seu próprio ID
- **Não logue dados sensíveis** - Senhas, tokens, informações de cartão
- **Não use Event IDs aleatórios** - Siga a convenção de ranges
- **Não logue em excesso** - Balance entre informação e ruído
- **Não use strings interpoladas** - Use parâmetros para melhor performance

---

## Exemplos de Anti-patterns

### ❌ RUIM

```csharp
// String interpolation - Aloca memória desnecessária
_logger.LogInformation($"User {userId} logged in");

// Sem Event ID - Difícil de filtrar
_logger.LogInformation("User logged in");

// Event ID duplicado
[LoggerMessage(EventId = 1001, ...)] UserLoginAttempt
[LoggerMessage(EventId = 1001, ...)] UserLogoutAttempt // ❌ Mesmo ID!

// Logging de dados sensíveis
_logger.LogInformation("Password attempt: {Password}", password); // ❌ NUNCA!
```

### ✅ BOM

```csharp
// LoggerMessage com Event ID único
_logger.UserLoginAttempt("user@example.com");

// Event IDs únicos e organizados
[LoggerMessage(EventId = 1001, ...)] UserLoginAttempt
[LoggerMessage(EventId = 1010, ...)] UserLogoutAttempt

// Sem dados sensíveis
_logger.UserLoginSuccessful("user@example.com", userId);
```

---

## Expandindo o Sistema de Logging

### Adicionar novos eventos

1. **Escolha o Event ID** apropriado baseado na categoria
2. **Adicione em LogMessages.cs**:

```csharp
[LoggerMessage(
    EventId = 6001,  // External Services range
    Level = LogLevel.Information,
    Message = "Sending email to {Email} with subject: {Subject}")]
public static partial void SendingEmail(
    this ILogger logger, 
    string email, 
    string subject);
```

3. **Documente neste README** na tabela correspondente
4. **Use no código**:

```csharp
_logger.SendingEmail(user.Email, "Welcome to DevTrivia!");
```

---

## Monitoring em Produção

### Application Insights (Azure)

```csharp
// appsettings.json
{
  "ApplicationInsights": {
    "ConnectionString": "InstrumentationKey=..."
  },
  "Logging": {
    "ApplicationInsights": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft": "Warning"
      }
    }
  }
}
```

### Serilog (Estruturado)

```csharp
// Program.cs
builder.Host.UseSerilog((context, config) =>
{
    config
        .WriteTo.Console()
        .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
        .WriteTo.Seq("http://localhost:5341") // Seq server
        .Enrich.WithProperty("Application", "DevTrivia")
        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName);
});
```

---

## Referências

- [Microsoft Logging Guidelines](https://learn.microsoft.com/en-us/dotnet/core/extensions/logging)
- [LoggerMessage Source Generators](https://learn.microsoft.com/en-us/dotnet/core/extensions/logger-message-generator)
- [High-performance logging](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/loggermessage)
- [Event IDs Best Practices](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.eventid)

---

## Próximos Eventos a Adicionar (Roadmap)

Conforme o projeto crescer, considere adicionar:

- **6000-6999**: Email notifications, SMS, Push notifications
- **7000-7999**: Rate limiting, Suspicious activity, IP blocking
- **8000-8999**: Slow database queries, API response times
- **9000-9999**: Application startup, Configuration loaded, Health checks
