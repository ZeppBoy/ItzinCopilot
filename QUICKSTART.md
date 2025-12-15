# Itzin Quick Start Guide

**Last Updated:** December 15, 2025  
**Status:** ✅ MVP Complete - Full Application Ready

## 🚀 Get Started in 5 Minutes

### Prerequisites
- .NET 8.0 SDK installed
- Node.js 18+ (for frontend)
- Terminal/Command Prompt
- Modern web browser
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

### ✅ Fully Implemented (MVP Complete)

#### Backend API
- **Authentication System:**
  - User Registration with email/password
  - User Login with JWT tokens
  - Password hashing with BCrypt
  - Email verification token system
  - Password reset functionality
  
- **Hexagram Library:**
  - Complete 64 hexagram database
  - Chinese names, Pinyin, Unicode symbols
  - English translations (names, judgments, images, lines)
  - Russian translations with 14 detailed fields per hexagram
  - Binary representation and trigram relationships
  
- **Consultation Engine:**
  - Cryptographically secure three-coin toss simulation
  - Hexagram generation with changing lines (old yin/yang)
  - Relating hexagram calculation
  - **Advanced Consultation Mode:**
    - Anti-Hexagram (all lines flipped)
    - Changing Hexagram (pattern visualization)
    - Additional Changing Hexagrams (progressive transformations)
  - Consultation history with timestamps
  - Notes functionality for reflections
  
- **API Endpoints:**
  - 3 Auth endpoints (register, login, reset)
  - 3 Hexagram endpoints (list, get by ID, get by number)
  - 4 Consultation endpoints (create, list, get, update notes)
  - Multi-language support (EN/RU)
  - Full Swagger documentation

#### Frontend Application
- **User Interface:**
  - Modern Angular 20 application
  - Traditional Chinese aesthetic design
  - Responsive layout
  - Loading states and error handling
  
- **Authentication Pages:**
  - Registration form with validation
  - Login form
  - Forgot password flow
  - Reset password with token
  
- **Hexagram Browsing:**
  - Grid view of all 64 hexagrams
  - Individual hexagram detail pages
  - Language toggle (EN/RU)
  - Visual line display with images
  
- **Consultation Flow:**
  - Question input (optional) with advanced mode toggle
  - Interactive coin toss animation (6 tosses)
  - Real-time hexagram building visualization
  - Changing line indicators
  - Comprehensive result display:
    - Primary Hexagram
    - Relating Hexagram (if changing lines)
    - Anti-Hexagram (advanced mode)
    - Changing Hexagram pattern (advanced mode)
    - Progressive transformations (advanced mode)
  
- **History & Dashboard:**
  - Dashboard with quick actions
  - History list of past consultations
  - Navigation to full hexagram interpretations
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

## ✨ Current Status & Next Steps

### ✅ Completed
1. ✅ Sprint 1: Authentication System
2. ✅ Sprint 2: Hexagram Library & Consultation Engine
3. ✅ Sprint 3: Frontend UI & Consultation Flow
4. ✅ Sprint 4: Advanced Consultation & Visual Enhancements

### 📋 Coming Next (Sprint 5)
1. ⏳ Email service implementation
2. ⏳ Complete i18n integration
3. ⏳ History detail component
4. ⏳ User profile/settings page
5. ⏳ Automated testing suite
6. ⏳ Docker containerization
7. ⏳ CI/CD pipeline

---

## 🚀 Running the Full Application

### Backend + Frontend Together

**Terminal 1 - Backend API:**
```bash
cd Itzin.Api
dotnet run
# API runs on http://localhost:5095
```

**Terminal 2 - Frontend:**
```bash
cd Itzin.Web
npm install  # First time only
npm start
# Frontend runs on http://localhost:4200
```

**Open Browser:**
```
http://localhost:4200
```

You can now:
- Register a new account
- Log in
- Browse all 64 hexagrams
- Perform I Ching consultations
- View consultation history
- Use advanced consultation mode

---

## 📖 Quick Guide to Using the Application

1. **Register:** Create an account at `/register`
2. **Login:** Sign in at `/login`
3. **Browse Hexagrams:** View library at `/hexagrams`
4. **New Consultation:** Click "Begin Consultation" from dashboard
5. **Ask Question:** (Optional) Enter your question
6. **Advanced Mode:** Toggle for additional hexagram patterns
7. **Toss Coins:** Click to perform 6 coin tosses
8. **View Results:** See your hexagrams and interpretations
9. **View History:** Access past consultations from dashboard

---

**Happy Coding! 🎉**

For questions or issues, check the documentation or review the implementation progress.
