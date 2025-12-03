npm start# Itzin Frontend - Angular Application

## Overview
This is the Angular frontend for the Itzin I Ching Divination System. Built with Angular 20, it provides a modern, responsive interface with traditional Chinese design elements.

## Technology Stack
- **Angular:** 20.3.0
- **TypeScript:** 5.9.2
- **SCSS:** For styling
- **Angular Material:** 20.2.14 (optional components)
- **RxJS:** 7.8.0

## Project Structure
```
src/
├── app/
│   ├── core/                 # Core functionality (singleton services)
│   │   ├── guards/          # Route guards (auth.guard.ts)
│   │   ├── interceptors/    # HTTP interceptors (auth.interceptor.ts)
│   │   ├── models/          # TypeScript interfaces/models
│   │   └── services/        # Core services (auth.service.ts)
│   ├── features/            # Feature modules
│   │   ├── auth/           # Authentication features
│   │   │   └── components/
│   │   │       ├── login/
│   │   │       ├── register/
│   │   │       └── password-reset/
│   │   └── dashboard/      # Dashboard component
│   └── shared/             # Shared components, directives, pipes
├── environments/           # Environment configurations
└── styles.scss            # Global styles
```

## Features Implemented

### Authentication System ✅
- **Login Component:** Email/password authentication with JWT
- **Register Component:** User registration with multi-language support
- **Auth Service:** Centralized authentication management
- **Auth Guard:** Route protection for authenticated routes
- **Auth Interceptor:** Automatic JWT token injection and refresh

### Dashboard ✅
- Basic dashboard with welcome message
- User info display
- Quick action cards (placeholders for future features)

## Getting Started

### Prerequisites
- Node.js 18+ and npm
- Angular CLI (`npm install -g @angular/cli`)

### Development Server
```bash
npm start
```
Navigate to `http://localhost:4200/`

### Build
```bash
npm run build
```

## API Integration
The frontend connects to the backend API at:
- **Development:** `http://localhost:5095/api`
- **Production:** `/api` (relative URL)

Configuration is in `src/environments/environment.ts`

## Routes
- `/` - Redirects to login
- `/login` - Login page
- `/register` - Registration page  
- `/dashboard` - Main dashboard (protected)
- `/password-reset` - Password reset (TODO)

## Next Steps
- [ ] Complete password reset flow
- [ ] Add hexagram library components
- [ ] Implement consultation/coin toss interface
- [ ] Add internationalization (i18n) for EN/RU
