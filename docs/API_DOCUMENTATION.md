# DevTrivia API Documentation

**Base URL:** `https://<your-host>/api`
**Authentication:** JWT Bearer tokens (`Authorization: Bearer {token}`)
**Content-Type:** `application/json`

All responses are wrapped in:
```json
{
  "success": true|false,
  "data": { ... },
  "message": "optional message",
  "errors": { "field": ["error messages"] }
}
```

---

## 1. User Controller (`/api/user`)

Handles authentication, registration, and user profile management.

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| POST | `/login` | None | Authenticate with email/password, returns JWT token |
| POST | `/register` | None | Register a new user account |
| GET | `/me` | Bearer | Get current authenticated user's profile |
| GET | `/{id}` | Bearer | Get user by ID |
| GET | `/` | Bearer | Get all users (paginated: `?page=1&pageSize=10`) |
| PUT | `/{id}` | Bearer | Update user profile (self or admin only) |
| DELETE | `/{id}` | Bearer | Delete user account (self or admin only) |
| POST | `/{id}/change-password` | Bearer | Change user password |
| PUT | `/{id}/role` | Admin | Change user role |

### Key Request/Response Models

**POST /login**
```json
// Request
{ "email": "user@example.com", "password": "P@ssw0rd!" }

// Response (data)
{
  "token": "eyJhb...",
  "tokenType": "Bearer",
  "userId": 1,
  "email": "user@example.com",
  "name": "John",
  "role": "Player",
  "expiresAt": "2026-03-29T14:00:00Z"
}
```

**POST /register**
```json
// Request
{
  "name": "John Doe",           // 2-100 chars
  "email": "john@example.com",
  "password": "P@ssw0rd!",     // 8+ chars, 1 upper, 1 lower, 1 number, 1 special
  "preferredLanguage": "en"     // optional
}

// Response (data) → UserDto
```

**UserDto (used in multiple responses)**
```json
{
  "id": 1,
  "name": "John Doe",
  "email": "john@example.com",
  "role": "Player",
  "profileImageUrl": "https://...",
  "bio": "Developer",
  "location": "Brazil",
  "dateOfBirth": "1990-01-15",
  "lastLoginAt": "2026-03-29T12:00:00Z",
  "preferredLanguage": "en",
  "createdAt": "2026-01-01T00:00:00Z",
  "updatedAt": "2026-03-29T12:00:00Z"
}
```

**PUT /{id} (UpdateUserRequest)**
```json
{
  "name": "New Name",
  "bio": "Senior Dev",
  "location": "Berlin",
  "dateOfBirth": "1990-01-15",
  "profileImageUrl": "https://example.com/photo.jpg",
  "preferredLanguage": "pt"
}
```

---

## 2. Category Controller (`/api/category`)

Manages trivia categories (e.g., "JavaScript", "Python", "DevOps").

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/` | None | Get all categories |
| GET | `/{id}` | None | Get category by ID |
| POST | `/` | Admin | Create category |
| PUT | `/{id}` | Admin | Update category |
| DELETE | `/{id}` | Admin | Delete category |

### Models

**CategoryRequest**
```json
{ "name": "JavaScript", "description": "Questions about JavaScript fundamentals" }
```

**CategoryResponse**
```json
{
  "id": 1,
  "name": "JavaScript",
  "description": "Questions about JavaScript fundamentals",
  "createdAt": "2026-01-01T00:00:00Z",
  "updatedAt": "2026-01-01T00:00:00Z"
}
```

---

## 3. Question Controller (`/api/question`)

Manages trivia questions within categories.

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/` | None | Get all questions |
| GET | `/{id}` | None | Get question by ID |
| GET | `/category/{categoryId}` | None | Get questions by category |
| GET | `/category/{categoryId}/difficulty/{difficulty}` | None | Filter by category + difficulty |
| POST | `/` | Admin | Create question |
| PUT | `/{id}` | Admin | Update question |
| DELETE | `/{id}` | Admin | Delete question |

### Difficulty Levels (Enum)
| Value | Name |
|-------|------|
| 1 | Easy |
| 2 | Medium |
| 3 | Hard |
| 4 | Super |

### Models

**QuestionRequest**
```json
{
  "title": "What is a closure in JavaScript?",
  "description": "Detailed explanation of the question",
  "difficulty": 2,
  "categoryId": 1
}
```

**QuestionResponse**
```json
{
  "id": 1,
  "title": "What is a closure in JavaScript?",
  "description": "...",
  "difficulty": 2,
  "categoryId": 1,
  "categoryName": "JavaScript",
  "createdAt": "2026-01-01T00:00:00Z",
  "updatedAt": "2026-01-01T00:00:00Z"
}
```

---

## 4. Answer Option Controller (`/api/answeroption`)

Manages answer options for questions (multiple choice).

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/` | None | Get all answer options |
| GET | `/{id}` | None | Get answer option by ID |
| GET | `/question/{questionId}` | None | Get options for a question |
| POST | `/` | Admin | Create answer option |
| PUT | `/{id}` | Admin | Update answer option |
| DELETE | `/{id}` | Admin | Delete answer option |

### Models

**AnswerOptionRequest**
```json
{ "text": "A function inside another function", "isCorrect": true, "questionId": 1 }
```

**AnswerOptionResponse**
```json
{
  "id": 1,
  "text": "A function inside another function",
  "isCorrect": true,
  "questionId": 1,
  "questionTitle": "What is a closure in JavaScript?"
}
```

---

## 5. Match Controller (`/api/match`)

Manages trivia matches and the full game flow. This is the core gameplay controller.

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/` | None | Get all matches |
| GET | `/{id}` | None | Get match by ID |
| POST | `/` | Bearer | Create a new match |
| PUT | `/{id}` | Admin | Update match |
| DELETE | `/{id}` | Admin | Delete match |
| POST | `/{id}/start` | Bearer | Start a match (selects 10 random questions) |
| GET | `/{id}/next-question` | Bearer | Get the next unanswered question |
| POST | `/{id}/answer` | Bearer | Submit an answer for a question |
| GET | `/{id}/results` | Bearer | Get final results (only when match is finished) |

### Match Status (Enum)
| Value | Name | Description |
|-------|------|-------------|
| 1 | Pending | Match created, not yet started |
| 2 | InProgress | Match started, questions being answered |
| 3 | Finished | All questions answered |

### Game Flow

```
1. CREATE MATCH  →  POST /api/match  { "status": 1, "selectedCategoryId": 3 }
                    Returns matchId, status=Pending

2. START MATCH   →  POST /api/match/{id}/start
                    Selects 10 random questions from category
                    Returns { matchId, totalQuestions: 10, categoryName, status: "InProgress" }

3. GET QUESTION  →  GET /api/match/{id}/next-question  (repeat for each question)
                    Returns { questionId, title, questionNumber, totalQuestions, options: [...] }

4. SUBMIT ANSWER →  POST /api/match/{id}/answer  { "questionId": 5, "selectedAnswerOptionId": 12 }
                    Returns { isCorrect, correctAnswerOptionId, isLastQuestion }
                    (selectedAnswerOptionId can be null to skip)

5. GET RESULTS   →  GET /api/match/{id}/results  (only after match is Finished)
                    Returns { matchId, totalQuestions, correctAnswers, score, questions: [...] }
```

### Models

**MatchRequest (Create)**
```json
{ "status": 1, "selectedCategoryId": 3 }
```

**MatchResponse**
```json
{ "id": 1, "status": 2, "selectedCategoryId": 3, "categoryName": "Python" }
```

**GameStartResponse**
```json
{ "matchId": 1, "totalQuestions": 10, "categoryName": "Python", "status": "InProgress" }
```

**GameQuestionResponse**
```json
{
  "questionId": 5,
  "title": "What does 'self' refer to in Python?",
  "questionNumber": 3,
  "totalQuestions": 10,
  "options": [
    { "id": 11, "text": "The current instance" },
    { "id": 12, "text": "The parent class" },
    { "id": 13, "text": "A global variable" },
    { "id": 14, "text": "The module" }
  ]
}
```

**SubmitAnswerRequest**
```json
{ "questionId": 5, "selectedAnswerOptionId": 11 }
```

**SubmitAnswerResponse**
```json
{ "isCorrect": true, "correctAnswerOptionId": 11, "isLastQuestion": false }
```

**GameResultsResponse**
```json
{
  "matchId": 1,
  "totalQuestions": 10,
  "correctAnswers": 7,
  "score": 70,
  "questions": [
    {
      "questionId": 5,
      "title": "What does 'self' refer to in Python?",
      "selectedAnswerOptionId": 11,
      "correctAnswerOptionId": 11,
      "isCorrect": true
    }
  ]
}
```

---

## Authentication Details

### JWT Token
- **Algorithm:** HS256
- **Issuer:** `DevTrivia`
- **Audience:** `DevTrivia.Users`
- **Expiration:** 60 minutes (default)
- **Header:** `Authorization: Bearer {token}`

### Roles
| Value | Name | Permissions |
|-------|------|-------------|
| 1 | Admin | Full CRUD on all resources |
| 2 | Player | Create/play matches, manage own profile |

### Password Requirements
- Minimum 8 characters
- At least 1 uppercase letter
- At least 1 lowercase letter
- At least 1 number
- At least 1 special character (`@$!%*?&#`)

---

## Error Responses

| HTTP Code | Meaning |
|-----------|---------|
| 400 | Bad Request / Validation Error |
| 403 | Forbidden (insufficient permissions) |
| 404 | Resource Not Found |
| 409 | Conflict (e.g., email already exists) |
| 500 | Internal Server Error |

```json
{
  "success": false,
  "data": null,
  "message": "Validation failed",
  "errors": {
    "email": ["The email field is required."],
    "password": ["Password must be at least 8 characters."]
  }
}
```
