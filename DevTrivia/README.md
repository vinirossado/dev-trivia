# DevTrivia API

API RESTful para o aplicativo DevTrivia, desenvolvida com .NET 10 e PostgreSQL.

## 🚀 Tecnologias

- **.NET 10** - Framework web
- **Entity Framework Core 10** - ORM
- **PostgreSQL** - Banco de dados
- **jose-jwt** - Geração de tokens JWT
- **BCrypt.Net** - Hash de senhas
- **Swagger/OpenAPI** - Documentação de API

## 📋 Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- PostgreSQL 14+ (local ou Azure)
- Azure Key Vault (opcional, para produção)

## ⚙️ Configuração

### 1. Clonar o repositório

```bash
git clone <repository-url>
cd DevTrivia
```

### 2. Configurar appsettings.Development.json

Copie o arquivo de exemplo e preencha com suas credenciais:

```bash
cd DevTrivia.API
cp appsettings.Development.json.example appsettings.Development.json
```

Edite `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=devtrivia;Port=5432;User Id=postgres;Password=sua-senha;"
  },
  "JwtSettings": {
    "SecretKey": "SuaChaveSecretaDeveSerPeloMenos32CaracteresParaHS256!",
    "Issuer": "DevTrivia",
    "Audience": "DevTrivia.Users",
    "ExpirationInMinutes": 60
  }
}
```

### 3. Criar o banco de dados

```bash
# Aplicar migrations
dotnet ef database update

# Ou criar migration se necessário
dotnet ef migrations add NomeDaMigracao
```

### 4. Executar o projeto

```bash
dotnet run
```

A API estará disponível em:
- **HTTPS**: `https://localhost:5001`
- **HTTP**: `http://localhost:5000`
- **Swagger**: `https://localhost:5001/swagger`

## 📚 API Endpoints

### Autenticação (Público)

#### Registrar usuário
```http
POST /api/user/register
Content-Type: application/json

{
  "name": "João Silva",
  "email": "joao@example.com",
  "password": "SenhaSegura123!",
  "preferredLanguage": "pt-BR"
}
```

#### Login
```http
POST /api/user/login
Content-Type: application/json

{
  "email": "joao@example.com",
  "password": "SenhaSegura123!"
}
```

**Resposta:**
```json
{
  "success": true,
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "tokenType": "Bearer",
    "userId": 1,
    "email": "joao@example.com",
    "name": "João Silva",
    "expiresAt": "2025-12-21T14:00:00Z"
  },
  "message": "Login successful"
}
```

### Usuários (Requer autenticação)

Incluir o token JWT no header:
```
Authorization: Bearer <seu-token>
```

#### Obter perfil do usuário autenticado
```http
GET /api/user/me
```

#### Obter usuário por ID
```http
GET /api/user/{id}
```

#### Listar todos os usuários (paginado)
```http
GET /api/user?page=1&pageSize=10
```

#### Atualizar perfil
```http
PUT /api/user/{id}
Content-Type: application/json

{
  "name": "João Silva Atualizado",
  "bio": "Desenvolvedor .NET",
  "location": "São Paulo, Brasil",
  "profileImageUrl": "https://example.com/avatar.jpg"
}
```

#### Alterar senha
```http
POST /api/user/{id}/change-password
Content-Type: application/json

{
  "currentPassword": "SenhaAntiga123!",
  "newPassword": "NovaSenha456!"
}
```

#### Deletar usuário
```http
DELETE /api/user/{id}
```

## 🏗️ Arquitetura

```
DevTrivia.API/
├── Capabilities/
│   ├── User/
│   │   ├── Controllers/       # API Endpoints
│   │   ├── Services/          # Lógica de negócio
│   │   ├── Repositories/      # Acesso a dados
│   │   ├── Models/            # DTOs
│   │   └── Database/
│   │       ├── Entities/      # Entidades EF Core
│   │       └── EntityTypeConfiguration/
│   ├── Category/              # Gerenciamento de categorias
│   ├── Question/              # Gerenciamento de perguntas
│   └── Trivia/
│       └── Models/            # Models compartilhados
├── Infrastructure/
│   └── Logging/               # Compile-time logging
├── Migrations/                # EF Core migrations
└── Program.cs                 # Configuração da aplicação
```

### Padrão de camadas

```
Controller → Service → Repository → Database
     ↓          ↓           ↓
   DTOs    Business Logic  EF Core
```

## 🗄️ Banco de Dados

Para informações detalhadas sobre o esquema do banco de dados, incluindo todas as tabelas planejadas e implementadas, consulte:

📖 **[DATABASE_SCHEMA.md](./DATABASE_SCHEMA.md)** - Documentação completa do esquema

### Status Atual

| Tabela | Status | Descrição |
|--------|--------|-----------|
| Users | ✅ Implementado | Gerenciamento de usuários e autenticação |
| Categories | ✅ Implementado | Categorias de perguntas (C#, JavaScript, etc) |
| Questions | ✅ Implementado | Perguntas do trivia |
| **AnswerOptions** | ❌ **PLANEJADO** | **Alternativas e resposta correta** |
| Matches | ❌ Planejado | Sessões de jogo |
| PlayerAnswers | ❌ Planejado | Registro de respostas |
| PlayerStats | ❌ Planejado | Estatísticas e ranking |
| AuthSessions | ❌ Planejado | Refresh tokens |

> ⚠️ **Nota**: Atualmente o sistema não possui tabela de alternativas (AnswerOptions), que é crítica para o funcionamento do jogo. Veja DATABASE_SCHEMA.md para detalhes.

## 🔐 Segurança

- ✅ Senhas hashadas com BCrypt (work factor 12)
- ✅ JWT com assinatura HS256
- ✅ Validação de senha forte (mínimo 8 caracteres, maiúscula, minúscula, número, caractere especial)
- ✅ Email único por usuário
- ✅ Usuários só podem modificar seus próprios dados
- ✅ CORS configurado

## 📊 Event IDs de Logging

Os logs seguem um padrão de Event IDs por categoria:

| Range | Categoria | Exemplos |
|-------|-----------|----------|
| 1000-1999 | Authentication | Login (1001), Login Success (1002), Login Failed (1003) |
| 2000-2999 | CRUD Operations | Fetch User (2001), Update User (2003) |
| 3000-3999 | JWT Operations | Generate Token (3001), Token Failed (3002) |
| 4000-4999 | Database | DB Error (4001), DB Success (4002) |
| 5000-5999 | Validation | Validation Failed (5001) |

### Exemplos de filtros de log:

```bash
# Ver todos os logins
grep "EventId: 1001" logs.txt

# Ver falhas de autenticação
grep "EventId: 1003" logs.txt

# Contar operações de DB
grep "EventId: 400" logs.txt | wc -l
```

## 🧪 Testes

```bash
cd DevTrivia.Tests
dotnet test
```

## 🐳 Docker

### Build
```bash
docker build -t devtrivia-api .
```

### Run
```bash
docker run -p 5000:8080 \
  -e ConnectionStrings__DefaultConnection="<connection-string>" \
  -e JwtSettings__SecretKey="<jwt-secret>" \
  devtrivia-api
```

### Docker Compose
```bash
docker-compose up
```

## 📝 Migrations

### Criar nova migration
```bash
dotnet ef migrations add <NomeDaMigracao>
```

### Aplicar migrations
```bash
dotnet ef database update
```

### Reverter migration
```bash
dotnet ef database update <MigracaoAnterior>
```

### Remover última migration
```bash
dotnet ef migrations remove
```

## 🌍 Variáveis de Ambiente (Produção)

Configure no Azure App Service ou Key Vault:

```
ConnectionStrings__DefaultConnection
JwtSettings__SecretKey
JwtSettings__Issuer
JwtSettings__Audience
JwtSettings__ExpirationInMinutes
KeyVault__Vault
```

## 📦 Pacotes Principais

```xml
<PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />
<PackageReference Include="jose-jwt" Version="5.2.0" />
<PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="10.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="10.0.1" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="10.0.1" />
<PackageReference Include="Azure.Identity" Version="1.17.1" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="10.1.0" />
```

## 📄 Licença

MIT

## 🤝 Contribuindo

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona MinhaFeature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request

## 👥 Autores

- Seu Nome - [GitHub](https://github.com/seu-usuario)
