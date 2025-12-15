# Itzin - I Ching Divination System

## Overview
Itzin is a web-based I Ching divination application implementing the ancient Book of Changes system through digital coin tossing methodology. The system provides authentic hexagram-based guidance with comprehensive interpretations in English and Russian.

## Technology Stack

### Backend
- **Framework:** ASP.NET Core 8.0 (C#)
- **Database:** SQLite with Entity Framework Core 8.0
- **Authentication:** JWT-based with BCrypt password hashing
- **Logging:** Serilog
- **API Documentation:** Swagger/OpenAPI

### Architecture
The solution follows a clean architecture pattern with three main projects:

1. **Itzin.Core** - Domain entities and interfaces
2. **Itzin.Infrastructure** - Data access and external services
3. **Itzin.Api** - REST API controllers and DTOs

## Current Implementation Status

### ✅ Phase 1 - MVP: COMPLETED (December 15, 2025)

#### Sprint 1: Authentication & Setup ✅
- [x] Project structure with layered architecture (Core, Infrastructure, API)
- [x] SQLite database with Entity Framework Core
- [x] User entity with authentication fields
- [x] Hexagram entity with multilingual support
- [x] Consultation entity for storing divination history
- [x] Repository pattern implementation
- [x] JWT authentication service with BCrypt password hashing
- [x] User registration endpoint
- [x] User login endpoint
- [x] Email verification system (token-based)
- [x] Password reset functionality
- [x] CORS configuration for Angular frontend
- [x] Swagger documentation

#### Sprint 2: Hexagram Library ✅
- [x] Complete hexagram seed data system (all 64 hexagrams)
- [x] Russian descriptions for all hexagrams (14 fields each)
- [x] Cryptographically secure coin toss service
- [x] Hexagram binary calculation algorithm
- [x] Changing lines detection
- [x] Relating hexagram calculation
- [x] Consultation creation service
- [x] Hexagram API endpoints (list, get by ID, get by number)
- [x] Consultation API endpoints (create, list, get, update notes)
- [x] Multi-language support for hexagrams (EN/RU)
- [x] Authorization with JWT for consultations

#### Sprint 3: Frontend & Consultation Flow ✅
- [x] Angular 20 frontend with standalone components
- [x] Authentication UI (register, login, password reset)
- [x] Hexagram library browsing (list and detail views)
- [x] Interactive coin toss animation
- [x] Complete consultation flow (question → tosses → result)
- [x] Hexagram visual display with line images
- [x] History list component
- [x] Dashboard component
- [x] Routing and navigation

#### Sprint 4: Advanced Features ✅
- [x] Advanced Consultation mode
- [x] Anti-Hexagram calculation (inverse pattern)
- [x] Changing Hexagram algorithm (transformation pattern)
- [x] Additional Changing Hexagrams (progressive transformations)
- [x] Enhanced visual display with changing line indicators
- [x] Language toggle (EN/RU) in hexagram details
- [x] Comprehensive consultation result display
- [x] Bug fixes and navigation improvements

### ⏳ Phase 1 - Sprint 5: Testing & Deployment (Planned)
- [ ] Email service implementation
- [ ] Complete i18n integration
- [ ] History detail component
- [ ] User profile/settings
- [ ] Unit and integration tests
- [ ] E2E tests
- [ ] Docker containerization
- [ ] CI/CD pipeline

## Database Schema

### Users Table
- Id, Email, PasswordHash, FirstName, LastName
- PreferredLanguage (en/ru), Timezone
- Email verification tokens and expiry
- Password reset tokens and expiry
- CreatedAt, UpdatedAt timestamps

### Hexagrams Table
- 64 hexagrams with Chinese names, Pinyin, English and Russian translations
- Binary representation and Unicode symbols
- Judgment and Image texts in both languages
- Line interpretations (1-6) in both languages

### Consultations Table
- User relationship
- Question and consultation date
- Toss results (coin flip values)
- Primary and Relating hexagrams
- Changing lines information
- Notes field for user reflections

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQLite (embedded, no installation needed)

### Running the Application

1. **Clone the repository** (if applicable)

2. **Navigate to the API project:**
   ```bash
   cd Itzin.Api
   ```

3. **Run the application:**
   ```bash
   dotnet run
   ```

4. **Access Swagger UI:**
   Open your browser and navigate to:
   - HTTP: http://localhost:5095/swagger
   - HTTPS: https://localhost:7041/swagger

### Configuration

The application uses `appsettings.json` for configuration:

- **Database:** SQLite file-based database (`itzin.db`)
- **JWT Settings:** Configure secret key, issuer, audience, and token expiry
- **Logging:** Serilog with console and file outputs

⚠️ **Security Note:** Change the JWT SecretKey in production!

## API Endpoints

### Authentication (Public)

#### POST /api/auth/register
Register a new user account.

**Request Body:**
```json
{
  "email": "user@example.com",
  "password": "SecurePassword123",
  "firstName": "John",
  "lastName": "Doe",
  "preferredLanguage": "en"
}
```

**Response:**
```json
{
  "token": "jwt-token-here",
  "refreshToken": "refresh-token-here",
  "user": {
    "id": 1,
    "email": "user@example.com",
    "firstName": "John",
    "lastName": "Doe",
    "preferredLanguage": "en",
    "isEmailVerified": false
  }
}
```

#### POST /api/auth/login
Authenticate a user.

**Request Body:**
```json
{
  "email": "user@example.com",
  "password": "SecurePassword123"
}
```

#### POST /api/auth/verify-email?token={token}
Verify user email address.

#### POST /api/auth/forgot-password
Request a password reset token.

**Request Body:**
```json
{
  "email": "user@example.com"
}
```

#### POST /api/auth/reset-password
Reset password using a token.

**Request Body:**
```json
{
  "token": "reset-token-here",
  "newPassword": "NewSecurePassword123"
}
```

---

### Hexagrams (Public)

#### GET /api/hexagrams
Get all hexagrams (summary view).

**Query Parameters:**
- `language` (optional): "en" or "ru" (default: "en")

**Response:**
```json
[
  {
    "id": 1,
    "number": 1,
    "chineseName": "乾",
    "pinyin": "Qián",
    "englishName": "The Creative",
    "russianName": "Творчество",
    "unicode": "☰"
  }
]
```

#### GET /api/hexagrams/number/{number}
Get hexagram by number with full details.

**Path Parameters:**
- `number`: Hexagram number (1-64)

**Query Parameters:**
- `language` (optional): "en" or "ru" (default: "en")

**Response:**
```json
{
  "id": 1,
  "number": 1,
  "chineseName": "乾",
  "pinyin": "Qián",
  "englishName": "The Creative",
  "russianName": "Творчество",
  "unicode": "☰",
  "judgment": "THE CREATIVE works sublime success...",
  "image": "The movement of heaven is full of power...",
  "lines": [
    "Hidden dragon. Do not act.",
    "Dragon appearing in the field...",
    "..."
  ]
}
```

---

### Consultations (Authenticated)

**Authentication Required:** All consultation endpoints require a valid JWT token in the Authorization header:
```
Authorization: Bearer {your-jwt-token}
```

#### POST /api/consultations
Create a new I Ching consultation (performs coin toss and generates hexagram).

**Request Body:**
```json
{
  "question": "What is the best path forward for my career?",
  "language": "en"
}
```

**Response:**
```json
{
  "id": 1,
  "question": "What is the best path forward for my career?",
  "consultationDate": "2025-12-02T14:30:00Z",
  "primaryHexagram": {
    "id": 1,
    "number": 1,
    "chineseName": "乾",
    "englishName": "The Creative",
    "judgment": "...",
    "image": "...",
    "lines": ["...", "..."]
  },
  "relatingHexagram": {
    "id": 2,
    "number": 2,
    "chineseName": "坤",
    "englishName": "The Receptive",
    "judgment": "...",
    "image": "...",
    "lines": ["...", "..."]
  },
  "changingLines": [1, 4],
  "tossValues": [3, 2, 1, 0, 2, 1],
  "notes": null
}
```

#### GET /api/consultations
Get current user's consultation history.

**Query Parameters:**
- `skip` (optional): Number of records to skip (default: 0)
- `take` (optional): Number of records to return (default: 50, max: 50)
- `language` (optional): "en" or "ru" (default: "en")

**Response:**
```json
[
  {
    "id": 1,
    "question": "What is the best path forward?",
    "consultationDate": "2025-12-02T14:30:00Z",
    "primaryHexagram": {
      "id": 1,
      "number": 1,
      "chineseName": "乾",
      "englishName": "The Creative",
      "unicode": "☰"
    },
    "hasChangingLines": true
  }
]
```

#### GET /api/consultations/{id}
Get a specific consultation by ID.

**Path Parameters:**
- `id`: Consultation ID

**Query Parameters:**
- `language` (optional): "en" or "ru" (default: "en")

#### PATCH /api/consultations/{id}/notes
Update notes for a consultation.

**Request Body:**
```json
{
  "notes": "This reading resonated with my current situation..."
}
```

## Project Structure

```
ItzinCopilot/
├── Itzin.Api/              # REST API project
│   ├── Controllers/        # API endpoints
│   ├── DTOs/              # Data transfer objects
│   ├── Services/          # Application services (future)
│   └── Program.cs         # Application startup
├── Itzin.Core/            # Domain layer
│   ├── Entities/          # Domain models (User, Hexagram, Consultation)
│   └── Interfaces/        # Repository and service interfaces
├── Itzin.Infrastructure/  # Infrastructure layer
│   ├── Data/             # DbContext and migrations
│   ├── Repositories/     # Repository implementations
│   └── Services/         # Service implementations (Auth, etc.)
└── ItzinCopilot.sln      # Solution file
```

## Next Steps (Phase 1 - Sprint 2)

### Hexagram Library & Consultation Engine
- [ ] Implement coin tossing simulation service
- [ ] Create hexagram generation logic
- [ ] Seed database with 64 hexagrams data
- [ ] Build consultation endpoints
- [ ] Implement consultation history retrieval

### Remaining Phase 1 Features
- [ ] User profile management endpoints
- [ ] Localization/i18n support
- [ ] Email service integration
- [ ] Frontend Angular application

## Development

### Building the Solution
```bash
dotnet build
```

### Running Tests (when implemented)
```bash
dotnet test
```

### Database Migrations
The database is automatically created on first run using `EnsureCreated()`.

For production, Entity Framework migrations should be used:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Contributing

This project follows the development plan outlined in `DEVELOPMENT_PLAN.md`.

## License

[To be determined]

## References

- **BRD:** See `itzin_brd.md` for complete business requirements
- **Development Plan:** See `DEVELOPMENT_PLAN.md` for implementation roadmap
- **I Ching Sources:**
  - English: Wilhelm/Baynes translation
  - Russian: Karcher translation

---

**Status:** MVP Phase 1 - Authentication Module Complete ✅
**Last Updated:** December 2025
