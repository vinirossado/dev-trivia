# DevTrivia API

API RESTful para o aplicativo DevTrivia - um jogo de trivia para desenvolvedores. Desenvolvida com .NET 10 e PostgreSQL.

## Tecnologias

- **.NET 10** - Framework web
- **ASP.NET Core** - Web API
- **Entity Framework Core 10** - ORM com PostgreSQL (Npgsql)
- **jose-jwt** - Tokens JWT (HS256)
- **BCrypt.Net** - Hash de senhas (work factor 12)
- **FluentValidation** - Validacao de input
- **Swashbuckle** - Swagger/OpenAPI
- **Azure Key Vault** - Gerenciamento de secrets (opcional)

## Pre-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- PostgreSQL 14+
- Azure Key Vault (opcional, para producao)

## Configuracao

### 1. Clonar o repositorio

```bash
git clone <repository-url>
cd DevTrivia
```

### 2. Configurar appsettings.json

Crie o arquivo `DevTrivia.API/appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=devtrivia;Username=postgres;Password=sua-senha"
  },
  "JwtSettings": {
    "SecretKey": "SuaChaveSecretaDeveSerPeloMenos32CaracteresParaHS256!",
    "Issuer": "DevTrivia",
    "Audience": "DevTrivia.Users",
    "ExpirationInMinutes": 60
  },
  "ApiKeySettings": {
    "Keys": ["sua-api-key-aqui"]
  }
}
```

### 3. Criar o banco de dados

```bash
dotnet ef database update --project DevTrivia.API
```

### 4. Executar

```bash
cd DevTrivia.API && dotnet run
```

Swagger disponivel em: `http://localhost:5288/swagger`

## Arquitetura

### Capability-based Feature-Sliced Architecture

O projeto segue uma arquitetura **capability-based** (feature-sliced) com camadas. Cada dominio/feature vive isolado dentro de `Capabilities/`, com sua propria estrutura interna de camadas. Isso combina os beneficios de:

- **Feature slicing**: cada feature e autonoma, facil de encontrar e modificar sem impactar outras
- **Layered architecture**: separacao clara de responsabilidades dentro de cada feature

```
DevTrivia.API/
├── Capabilities/                  # Cada feature e uma capability isolada
│   ├── User/                      # Autenticacao, CRUD de usuarios, roles
│   ├── Category/                  # Categorias de perguntas
│   ├── Question/                  # Perguntas do trivia
│   ├── AnswerOptions/             # Alternativas de resposta
│   ├── Match/                     # Sessoes de jogo e gameplay
│   ├── PlayerAnswer/              # Respostas dos jogadores
│   └── Shared/                    # Base classes compartilhadas
├── Infrastructure/                # Concerns transversais
│   ├── Authentication/            # Custom auth handlers (API Key)
│   ├── Filters/                   # Global validation filter
│   ├── Logging/                   # Compile-time structured logging
│   └── Swagger/                   # JWT Bearer config no Swagger
├── Migrations/                    # EF Core migrations + DbContext
└── Program.cs                     # Composicao da aplicacao
```

### Estrutura interna de cada Capability

```
Capability/
├── Controllers/                   # Endpoints HTTP (entrada)
├── Services/                      # Logica de negocio
│   └── Interfaces/
├── Repositories/                  # Acesso a dados (EF Core)
│   └── Interfaces/
├── Database/
│   ├── Entities/                  # Modelos EF Core (herdam BaseEntity)
│   └── EntityTypeConfiguration/   # Fluent API config
├── Models/                        # DTOs (Request/Response)
├── Validators/                    # FluentValidation rules
├── Extensions/                    # Entity <-> DTO mapping
└── Enums/                         # Enumeracoes do dominio
```

### Fluxo de uma request

```
HTTP Request
    │
    ▼
Controller          Recebe request, chama service, retorna ApiResponse<T>
    │
    ▼
Service             Logica de negocio, validacoes, orquestracao
    │
    ▼
Repository          Acesso a dados via EF Core (herda BaseRepository<T>)
    │
    ▼
PostgreSQL          Persistencia
```

### Padroes utilizados

| Padrao | Onde | Motivo |
|--------|------|--------|
| **Repository Pattern** | `BaseRepository<T>` + interfaces | Abstrai acesso a dados, facilita testes |
| **DTO Pattern** | `Models/` em cada capability | Separacao estrita entre entidades e contratos de API |
| **Extension Methods** | `Extensions/` mapping | Conversao Entity <-> DTO sem poluir as classes |
| **Envelope Pattern** | `ApiResponse<T>` | Resposta padronizada em toda a API |
| **Strategy Pattern** | Authentication Schemes | JWT Bearer + API Key como esquemas intercambiaveis |
| **Validator Pattern** | FluentValidation + Global Filter | Validacao declarativa, desacoplada dos controllers |

## Autenticacao e Autorizacao

O sistema possui **dois esquemas de autenticacao**:

### JWT Bearer (default)
Login com email/password gera um token JWT com claims de role. Usado por usuarios finais.

### API Key (AuthKey scheme)
Header `X-Api-Key` para acesso administrativo sem login. Usado para integracao e operacoes de servico.

### Roles

| Role | Valor | Permissoes |
|------|-------|------------|
| Admin | 1 | CRUD de categorias, perguntas, answer options, gerenciar matches e usuarios |
| Player | 2 | Jogar matches, gerenciar proprio perfil |

### Como os schemes sao usados nos endpoints

```csharp
[Authorize]                                          // JWT - qualquer user logado
[Authorize(Roles = "Admin")]                         // JWT - so admin
[Authorize(AuthenticationSchemes = "AuthKey")]        // API Key apenas
[Authorize(AuthenticationSchemes = "Bearer,AuthKey", Roles = "Admin")]  // JWT ou API Key, admin
```

## API Endpoints

### Autenticacao (Publico)

```http
POST /api/user/register     # Registrar novo usuario (role Player por padrao)
POST /api/user/login        # Login, retorna JWT com role
```

### Usuario (Requer autenticacao)

```http
GET    /api/user/me                  # Perfil do usuario autenticado
GET    /api/user/{id}                # Buscar por ID
GET    /api/user?page=1&pageSize=10  # Listar (paginado)
PUT    /api/user/{id}                # Atualizar perfil (proprio ou admin)
DELETE /api/user/{id}                # Deletar (proprio ou admin)
POST   /api/user/{id}/change-password # Alterar senha
PUT    /api/user/{id}/role           # Alterar role (Admin ou API Key)
```

### Categorias

```http
GET    /api/category          # Listar todas (publico)
GET    /api/category/{id}     # Buscar por ID (publico)
POST   /api/category          # Criar (Admin)
PUT    /api/category/{id}     # Atualizar (Admin)
DELETE /api/category/{id}     # Deletar (Admin)
```

### Perguntas

```http
GET    /api/question                                           # Listar todas (publico)
GET    /api/question/{id}                                      # Buscar por ID (publico)
GET    /api/question/category/{categoryId}                     # Por categoria (publico)
GET    /api/question/category/{categoryId}/difficulty/{level}  # Por categoria e dificuldade (publico)
POST   /api/question                                           # Criar (Admin)
PUT    /api/question/{id}                                      # Atualizar (Admin)
DELETE /api/question/{id}                                      # Deletar (Admin)
```

### Answer Options

```http
GET    /api/answeroption                        # Listar todas (publico)
GET    /api/answeroption/{id}                   # Buscar por ID (publico)
GET    /api/answeroption/question/{questionId}  # Por pergunta (publico)
POST   /api/answeroption                        # Criar (Admin)
PUT    /api/answeroption/{id}                   # Atualizar (Admin)
DELETE /api/answeroption/{id}                   # Deletar (Admin)
```

### Match (Gameplay)

```http
POST   /api/match                    # Criar match Pending (autenticado)
GET    /api/match                    # Listar matches (publico)
GET    /api/match/{id}               # Buscar por ID (publico)
PUT    /api/match/{id}               # Atualizar (Admin)
DELETE /api/match/{id}               # Deletar (Admin)
POST   /api/match/{id}/start         # Iniciar match (autenticado)
GET    /api/match/{id}/next-question  # Proxima pergunta (autenticado)
POST   /api/match/{id}/answer        # Responder pergunta (autenticado)
GET    /api/match/{id}/results        # Resultados finais (autenticado)
```

### Fluxo de jogo

```
1. POST /api/match  { "status": 1, "selectedCategoryId": 1 }   → Cria match Pending
2. POST /api/match/{id}/start                                    → Seleciona 10 perguntas, status InProgress
3. GET  /api/match/{id}/next-question                            → Retorna pergunta + opcoes embaralhadas
4. POST /api/match/{id}/answer  { questionId, selectedAnswerOptionId }  → Feedback imediato (correto/incorreto)
5. Repete 3-4 ate isLastQuestion: true
6. GET  /api/match/{id}/results                                  → Score final e breakdown
```

## Banco de Dados

| Tabela | Status | Descricao |
|--------|--------|-----------|
| Users | Implementado | Usuarios, autenticacao, roles (Admin/Player) |
| Categories | Implementado | Categorias de perguntas |
| Questions | Implementado | Perguntas com dificuldade e categoria |
| AnswerOptions | Implementado | Alternativas com marcacao de correta |
| Matches | Implementado | Sessoes de jogo com estado (Pending/InProgress/Finished) |
| PlayerAnswers | Implementado | Respostas dos jogadores por match |

### Shared base

Todas as entidades herdam `BaseEntity` que fornece:
- `Id` (long, auto-increment)
- `CreatedAt` (nullable, default `NOW()`)
- `UpdatedAt` (nullable, default `NOW()`)

## Seguranca

- Senhas hashadas com BCrypt (work factor 12)
- JWT com assinatura HS256, clock skew desabilitado
- Validacao de senha forte (8+ chars, maiuscula, minuscula, numero, especial)
- Email unico por usuario
- Ownership check (usuarios so modificam proprios dados, admin pode tudo)
- API Key scheme para acesso administrativo externo
- CORS configurado
- `appsettings*.json` no `.gitignore`

## Logging

Logs estruturados com compile-time source generators (`LoggerMessage`).

| Range | Categoria |
|-------|-----------|
| 1000-1999 | Authentication (login, register, token) |
| 2000-2999 | User CRUD |
| 3000-3999 | JWT Operations |
| 4000-4999 | Database errors |
| 5000-5999 | Validation |

## Docker

```bash
# Build
docker build -t devtrivia-api .

# Docker Compose (API + PostgreSQL)
docker-compose -f compose.yaml up
```

## Testes

```bash
cd DevTrivia.Tests && dotnet test
```

## Migrations

```bash
# Criar
dotnet ef migrations add <NomeDaMigracao> --project DevTrivia.API

# Aplicar
dotnet ef database update --project DevTrivia.API

# Reverter
dotnet ef database update <MigracaoAnterior> --project DevTrivia.API
```

## Variaveis de Ambiente (Producao)

```
ConnectionStrings__DefaultConnection
JwtSettings__SecretKey
JwtSettings__Issuer
JwtSettings__Audience
JwtSettings__ExpirationInMinutes
ApiKeySettings__Keys__0
KeyVault__Vault
```

## Licenca

MIT
