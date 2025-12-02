# Itzin Quick Start Guide

## 🚀 Get Started in 5 Minutes

### Prerequisites
- .NET 8.0 SDK installed
- Terminal/Command Prompt
- (Optional) Postman or curl for API testing

### Step 1: Navigate to Project
```bash
cd /Users/viktorshershnov/AI/Projects/ItzinCopilot
```

### Step 2: Build the Solution
```bash
dotnet build
```

### Step 3: Run the API
```bash
cd Itzin.Api
dotnet run
```

The API will start on:
- HTTP: http://localhost:5095
- HTTPS: https://localhost:7041

### Step 4: Open Swagger UI
Open your browser and go to:
```
http://localhost:5095/swagger
```

You'll see interactive API documentation where you can test all endpoints directly!

---

## 🧪 Quick API Test

### Using the Test Scripts (Recommended)

**Test Authentication:**
```bash
./test-api.sh
```
Tests:
- ✅ User registration
- ✅ User login
- ✅ Invalid login attempt
- ✅ Duplicate email prevention

**Test Consultation System:**
```bash
./test-consultations.sh
```
Tests:
- ✅ Hexagram retrieval (all 64)
- ✅ I Ching consultation creation
- ✅ English and Russian language support
- ✅ Consultation history
- ✅ Notes functionality
- ✅ Changing lines and relating hexagrams

### Manual Testing with curl

#### Register a User
```bash
curl -X POST http://localhost:5095/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "your@email.com",
    "password": "SecurePass123!",
    "firstName": "John",
    "lastName": "Doe",
    "preferredLanguage": "en"
  }'
```

**Response:**
```json
{
  "token": "eyJhbGci...",
  "refreshToken": "UXDf9GL...",
  "user": {
    "id": 1,
    "email": "your@email.com",
    "firstName": "John",
    "lastName": "Doe",
    "preferredLanguage": "en",
    "isEmailVerified": false
  }
}
```

#### Login
```bash
curl -X POST http://localhost:5095/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "your@email.com",
    "password": "SecurePass123!"
  }'
```

---

## 📁 Project Structure

```
ItzinCopilot/
├── Itzin.Api/              # API Layer (Controllers, DTOs)
│   ├── Controllers/        # AuthController
│   ├── DTOs/              # Request/Response models
│   ├── appsettings.json   # Configuration
│   └── Program.cs         # Startup
│
├── Itzin.Core/            # Domain Layer
│   ├── Entities/          # User, Hexagram, Consultation
│   └── Interfaces/        # Repository interfaces
│
├── Itzin.Infrastructure/  # Data Layer
│   ├── Data/             # DbContext
│   ├── Repositories/     # Repository implementations
│   └── Services/         # AuthService
│
├── itzin.db              # SQLite database (auto-created)
├── README.md             # Full documentation
├── QUICKSTART.md         # This file
└── test-api.sh           # API test script
```

---

## 🎯 Current Features

### ✅ Implemented (Sprint 1 & 2)
- **Authentication System:**
  - User Registration with email/password
  - User Login with JWT tokens
  - Password hashing with BCrypt
  - Email verification token system
  - Password reset functionality

- **I Ching Consultation System:**
  - Complete 64 hexagram library with Chinese/English/Russian text
  - Cryptographically secure three-coin toss simulation
  - Hexagram generation with changing lines
  - Relating hexagram calculation
  - Consultation history with timestamps
  - Personal notes for each consultation
  - Multi-language support (EN/RU)

- **Infrastructure:**
  - SQLite database with Entity Framework Core
  - Swagger API documentation
  - Structured logging with Serilog
  - CORS support for Angular frontend
  - JWT-based API authorization

### ⏳ Coming Next (Sprint 3)
- Angular frontend application
- User profile management
- Email service integration
- Enhanced hexagram interpretations

---

## 📊 Database Location

The SQLite database is automatically created at:
```
Itzin.Api/itzin.db
```

To view/edit the database, use any SQLite browser tool like:
- [DB Browser for SQLite](https://sqlitebrowser.org/)
- [SQLite Viewer (VS Code extension)](https://marketplace.visualstudio.com/items?itemName=qwtel.sqlite-viewer)

---

## 🔧 Configuration

### JWT Settings (appsettings.json)
```json
{
  "JwtSettings": {
    "SecretKey": "Your-Secret-Key-Here-Change-In-Production-Min-32-Chars-12345",
    "Issuer": "ItzinApi",
    "Audience": "ItzinClient",
    "ExpiryMinutes": "60"
  }
}
```

⚠️ **IMPORTANT:** Change the SecretKey before deploying to production!

### Database Connection
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=itzin.db"
  }
}
```

---

## 🐛 Troubleshooting

### Port Already in Use
If port 5095 is busy:
```bash
dotnet run --urls "http://localhost:5000"
```

### Database Issues
Delete and recreate the database:
```bash
rm Itzin.Api/itzin.db
dotnet run  # Database will be recreated
```

### Build Errors
Clean and rebuild:
```bash
dotnet clean
dotnet build
```

---

## 📚 Learn More

- **Full Documentation:** See [README.md](README.md)
- **Business Requirements:** See [itzin_brd.md](itzin_brd.md)
- **Development Plan:** See [DEVELOPMENT_PLAN.md](DEVELOPMENT_PLAN.md)
- **Progress Tracking:** See [IMPLEMENTATION_PROGRESS.md](IMPLEMENTATION_PROGRESS.md)

---

## 💡 Tips

1. **Swagger UI** is your friend - use it to test endpoints interactively
2. **Watch the logs** - they're detailed and helpful for debugging
3. **JWT tokens** expire in 60 minutes - you'll need to login again
4. **Email verification** tokens are generated but not sent yet (coming soon)

---

## ✨ Next Steps

1. ✅ You've completed Sprint 1 (Authentication)
2. 📝 Next: Implement hexagram library (Sprint 2)
3. 🎲 Then: Build consultation engine (Sprint 3)
4. 🎨 Finally: Create Angular frontend (Sprint 4)

---

**Happy Coding! 🎉**

For questions or issues, check the documentation or review the implementation progress.
