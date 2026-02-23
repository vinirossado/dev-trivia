# DevTrivia API - Quick Start

## 🚀 Como começar

### 1. Executar a aplicação

```bash
cd DevTrivia.API
dotnet run
```

A API estará disponível em: `http://localhost:5288`
Swagger UI: `http://localhost:5288/swagger`

---

## 📝 Testando a Autenticação

### Passo 1: Registrar um usuário

**Endpoint:** `POST /api/user/register`

```bash
curl -X POST http://localhost:5288/api/user/register \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Teste User",
    "email": "teste@example.com",
    "password": "Teste123!@#"
  }'
```

### Passo 2: Fazer login

**Endpoint:** `POST /api/user/login`

```bash
curl -X POST http://localhost:5288/api/user/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "teste@example.com",
    "password": "Teste123!@#"
  }'
```

**Resposta (copie o token):**
```json
{
  "success": true,
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "userId": 1,
    "email": "teste@example.com"
  }
}
```

### Passo 3: Usar o token

**Endpoint:** `GET /api/user/me`

```bash
curl -X GET http://localhost:5288/api/user/me \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

---

## 🔧 Usando o Swagger

### Para endpoints públicos (sem autenticação):
1. Abra `http://localhost:5288/swagger`
2. Teste normalmente

### Para endpoints com `[Authorize]`:

**No Swagger atual (.NET 10)**, você precisa adicionar o header manualmente:

1. Expanda o endpoint (ex: `GET /api/user/me`)
2. Clique em **"Try it out"**
3. Adicione um novo parâmetro:
   - **Name:** `Authorization`
   - **Location:** `header`
   - **Value:** `Bearer eyJhbGc...seu-token-aqui`
4. Clique em **Execute**

---

## ⚠️ Problemas comuns

### 401 Unauthorized

**Causa:** Token inválido, expirado ou formato incorreto

**Solução:**
- Verifique se incluiu "Bearer " antes do token
- Faça login novamente para obter um token novo
- Verifique os logs da aplicação para detalhes

### Formato correto do header:
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

❌ **ERRADO:**
- `eyJhbGc...` (sem "Bearer")
- `bearer eyJhbGc...` (minúsculo)
- `Bearer: eyJhbGc...` (com dois pontos)

---

## 🧪 Testando com Postman (Recomendado)

### Configurar Collection

1. **Base URL:** `http://localhost:5288`
2. **Token variable:** `{{token}}`

### Script para capturar token automaticamente

No request de Login, adicione em **Tests**:

```javascript
if (pm.response.code === 200) {
    const response = pm.response.json();
    pm.collectionVariables.set("token", response.data.token);
}
```

### Configurar autenticação

Em cada request protegida:
- **Authorization tab** → **Type:** Bearer Token
- **Token:** `{{token}}`

Agora, após fazer login uma vez, todas as requisições usarão o token automaticamente! 🎉

---

## 📊 Endpoints disponíveis

### Públicos (sem autenticação)
- `POST /api/user/register` - Registrar usuário
- `POST /api/user/login` - Login

### Protegidos (requer token JWT)
- `GET /api/user/me` - Perfil do usuário autenticado
- `GET /api/user/{id}` - Buscar usuário por ID
- `GET /api/user` - Listar usuários (paginado)
- `PUT /api/user/{id}` - Atualizar perfil
- `DELETE /api/user/{id}` - Deletar usuário
- `POST /api/user/{id}/change-password` - Alterar senha

---

## 🔍 Verificar logs de autenticação

Os logs mostram detalhes sobre a validação do JWT:

```bash
# Token validado com sucesso
info: Program[0]
      Token validated successfully for user: Teste User

# Falha na autenticação
fail: Program[0]
      Authentication failed: IDX10223: Lifetime validation failed...
```

---

## 💡 Dicas

1. **Use Postman** para testes mais eficientes que o Swagger
2. **Salve o token** em uma variável de ambiente
3. **Tokens expiram em 60 minutos** (configurável em `appsettings.json`)
4. **Verifique os logs** da aplicação para debugging

---

## 📚 Documentação completa

- [README.md](README.md) - Documentação completa do projeto
- [AUTHENTICATION_GUIDE.md](AUTHENTICATION_GUIDE.md) - Guia detalhado de autenticação
- [Infrastructure/Logging/README.md](DevTrivia.API/Infrastructure/Logging/README.md) - Sistema de logging
