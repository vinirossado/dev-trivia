# Guia de Autenticação JWT - DevTrivia API

## 🔐 Como Funciona a Autenticação

A API utiliza **JWT (JSON Web Tokens)** com assinatura HS256 para autenticação.

### Fluxo de Autenticação

```
1. Cliente → POST /api/user/register (Registrar usuário)
2. Cliente → POST /api/user/login (Login com email/senha)
3. Server → Retorna JWT token
4. Cliente → Envia token em requisições protegidas
   Header: Authorization: Bearer {token}
5. Server → Valida token e autoriza acesso
```

---

## 🧪 Testando no Swagger

### 1. Iniciar a aplicação

```bash
cd DevTrivia.API
dotnet run
```

Acesse: `https://localhost:5001/swagger`

### 2. Registrar um novo usuário

**Endpoint:** `POST /api/user/register`

```json
{
  "name": "Teste User",
  "email": "teste@example.com",
  "password": "Teste123!@#",
  "preferredLanguage": "pt-BR"
}
```

**Validação de senha:**
- Mínimo 8 caracteres
- Pelo menos 1 letra maiúscula
- Pelo menos 1 letra minúscula
- Pelo menos 1 número
- Pelo menos 1 caractere especial (@$!%*?&#)

### 3. Fazer login

**Endpoint:** `POST /api/user/login`

```json
{
  "email": "teste@example.com",
  "password": "Teste123!@#"
}
```

**Resposta esperada:**

```json
{
  "success": true,
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwiaXNzIjoiRGV2VHJpdmlhIiwiYXVkIjoiRGV2VHJpdmlhLlVzZXJzIiwiaWF0IjoxNzM0Nzg5NjAwLCJleHAiOjE3MzQ3OTMyMDAsImp0aSI6IjEyMzQ1Njc4LTkwYWItY2RlZi0xMjM0LTU2Nzg5MGFiY2RlZiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiMSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJUZXN0ZSBVc2VyIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoidGVzdGVAZXhhbXBsZS5jb20iLCJlbWFpbCI6InRlc3RlQGV4YW1wbGUuY29tIiwibmFtZSI6IlRlc3RlIFVzZXIifQ.Kl5Z8gHxY7mN3qPw2RtVc4XyZ9AbCdEfGhIjKlMnOpQ",
    "tokenType": "Bearer",
    "userId": 1,
    "email": "teste@example.com",
    "name": "Teste User",
    "expiresAt": "2025-12-21T14:00:00Z"
  },
  "message": "Login successful"
}
```

### 4. Autenticar no Swagger

**Opção 1: Botão "Authorize" (Global)**

1. Clique no botão **🔓 Authorize** no topo do Swagger UI
2. No campo **Value**, cole: `Bearer {seu-token-aqui}`
   
   **Exemplo:**
   ```
   Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
   ```
   
3. Clique em **Authorize**
4. Clique em **Close**

**IMPORTANTE:** 
- ✅ **CORRETO**: `Bearer eyJhbGc...` (com espaço após "Bearer")
- ❌ **ERRADO**: `eyJhbGc...` (sem "Bearer")
- ❌ **ERRADO**: `bearer eyJhbGc...` (minúsculo)
- ❌ **ERRADO**: `Bearer: eyJhbGc...` (com dois pontos)

### 5. Testar endpoints protegidos

Agora você pode chamar endpoints que requerem `[Authorize]`:

- `GET /api/user/me` - Retorna perfil do usuário autenticado
- `GET /api/user/{id}` - Buscar usuário por ID
- `PUT /api/user/{id}` - Atualizar perfil
- `DELETE /api/user/{id}` - Deletar usuário
- `POST /api/user/{id}/change-password` - Alterar senha

---

## 🔧 Testando com cURL

### Registrar

```bash
curl -X POST https://localhost:5001/api/user/register \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Teste User",
    "email": "teste@example.com",
    "password": "Teste123!@#"
  }'
```

### Login

```bash
curl -X POST https://localhost:5001/api/user/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "teste@example.com",
    "password": "Teste123!@#"
  }'
```

**Salve o token da resposta:**

```bash
TOKEN="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

### Acessar endpoint protegido

```bash
curl -X GET https://localhost:5001/api/user/me \
  -H "Authorization: Bearer $TOKEN"
```

### Atualizar perfil

```bash
curl -X PUT https://localhost:5001/api/user/1 \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Teste User Atualizado",
    "bio": "Desenvolvedor .NET",
    "location": "São Paulo, Brasil"
  }'
```

---

## 🧪 Testando com Postman

### 1. Criar coleção

1. Crie uma nova Collection: **DevTrivia API**
2. Adicione variáveis:
   - `baseUrl`: `https://localhost:5001`
   - `token`: (será preenchido automaticamente)

### 2. Configurar autenticação automática

**Em cada request protegida:**

1. Aba **Authorization**
2. Type: **Bearer Token**
3. Token: `{{token}}`

### 3. Script para capturar token automaticamente

No request de **Login**, adicione em **Tests**:

```javascript
if (pm.response.code === 200) {
    const response = pm.response.json();
    pm.collectionVariables.set("token", response.data.token);
    console.log("Token salvo:", response.data.token);
}
```

Agora, após fazer login, o token será automaticamente usado em todas as requisições!

---

## 🐛 Debugging de Autenticação

### Logs úteis no console da aplicação:

```bash
# Token validado com sucesso
info: Program[0]
      Token validated successfully for user: Teste User

# Falha na autenticação
fail: Program[0]
      Authentication failed: IDX10223: Lifetime validation failed...
      
# Challenge de autenticação
warn: Program[0]
      Authentication challenge: invalid_token
```

### Erros comuns e soluções:

#### ❌ **401 Unauthorized - Token expirado**

**Erro:**
```
IDX10223: Lifetime validation failed. The token is expired.
```

**Solução:** Faça login novamente para obter um novo token.

---

#### ❌ **401 Unauthorized - Token inválido**

**Erro:**
```
IDX10214: Audience validation failed
```

**Solução:** Verifique se o `Issuer` e `Audience` no `appsettings.Development.json` estão corretos:

```json
{
  "JwtSettings": {
    "Issuer": "DevTrivia",
    "Audience": "DevTrivia.Users"
  }
}
```

---

#### ❌ **401 Unauthorized - Formato do header incorreto**

**Sintomas:** Token válido mas ainda retorna 401

**Soluções:**

1. **Verifique o formato do header:**
   ```
   ✅ CORRETO: Authorization: Bearer eyJhbGc...
   ❌ ERRADO:  Authorization: eyJhbGc...
   ❌ ERRADO:  Bearer eyJhbGc...
   ```

2. **No Swagger, use:**
   ```
   Bearer eyJhbGc...
   ```
   (O Swagger adiciona "Authorization:" automaticamente)

---

#### ❌ **403 Forbidden - Usuário tentando acessar recurso de outro**

**Sintomas:** Token válido mas 403 ao atualizar/deletar outro usuário

**Explicação:** Usuários só podem modificar seus próprios dados.

```csharp
// UserController.cs
var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
if (userIdClaim != id.ToString())
{
    return Forbid(); // 403 Forbidden
}
```

**Solução:** Use o ID do usuário autenticado (do token) ou chame `/api/user/me`.

---

## 🔍 Decodificar JWT (Debug)

### Online: jwt.io

1. Acesse: https://jwt.io
2. Cole o token no campo **Encoded**
3. Veja o payload decodificado

**Exemplo de payload:**

```json
{
  "sub": "1",
  "iss": "DevTrivia",
  "aud": "DevTrivia.Users",
  "iat": 1734789600,
  "exp": 1734793200,
  "jti": "12345678-90ab-cdef-1234-567890abcdef",
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier": "1",
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name": "Teste User",
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress": "teste@example.com",
  "email": "teste@example.com",
  "name": "Teste User"
}
```

### Claims importantes:

- `sub`: Subject (User ID)
- `exp`: Expiration time (Unix timestamp)
- `iss`: Issuer (DevTrivia)
- `aud`: Audience (DevTrivia.Users)
- `http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier`: Usado pelo ASP.NET Core para `ClaimTypes.NameIdentifier`

---

## 🔐 Segurança

### Boas práticas implementadas:

✅ **Token expira em 60 minutos** (configurável)
✅ **Senha hashada com BCrypt** (work factor 12)
✅ **HTTPS obrigatório em produção**
✅ **Validação de issuer e audience**
✅ **ClockSkew = 0** (sem tolerância de tempo)

### Não fazer:

❌ **Armazenar token em localStorage** (vulnerável a XSS)
   - Use cookies HttpOnly ou sessionStorage

❌ **Compartilhar tokens**
   - Cada usuário deve ter seu próprio token

❌ **Tokens de longa duração**
   - Use refresh tokens para sessões longas

---

## 📝 Próximos passos

Para produção, considere adicionar:

1. **Refresh Tokens** - Renovar tokens sem fazer login novamente
2. **Token Blacklist** - Invalidar tokens antes da expiração
3. **Rate Limiting** - Prevenir brute force attacks
4. **Two-Factor Authentication (2FA)**
5. **OAuth2 / OpenID Connect** - Login social (Google, GitHub, etc.)

---

## 🆘 Suporte

Se você ainda tiver problemas:

1. Verifique os logs da aplicação (console)
2. Use jwt.io para decodificar o token
3. Teste com cURL para isolar problemas do Swagger
4. Verifique se o `appsettings.Development.json` está configurado corretamente
