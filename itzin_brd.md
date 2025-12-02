# Business Requirements Document: Itzin - I Ching Divination System

## Document Control

| Version | Date | Author | Changes |
|---------|------|--------|---------|
| 1.0 | November 2025 | Business Analysis Team | Initial Release |

---

## 1. Executive Summary

### 1.1 Purpose
This document outlines the business and functional requirements for **Itzin**, a web-based divination application implementing the ancient I Ching (Book of Changes) system through digital coin tossing methodology.

### 1.2 Project Overview
Itzin will provide users with an authentic I Ching consultation experience, enabling them to receive hexagram-based guidance through a traditional three-coin tossing method, digitally simulated. The system will maintain consultation history, provide detailed interpretations, and offer an intuitive user experience for both novice and experienced practitioners.

### 1.3 Business Objectives
- Digitize the traditional I Ching consultation process while maintaining authenticity
- Provide accessible divination services to users worldwide
- Create a knowledge base for I Ching hexagrams and their interpretations
- Enable users to track and reflect on their consultation history
- Build a scalable platform for future enhancements (community features, advanced analytics)

---

## 2. Scope

### 2.1 In Scope
- Digital coin tossing simulation (three-coin method)
- Hexagram generation and identification
- Comprehensive hexagram interpretation library (all 64 hexagrams)
- Changing lines calculation and resulting hexagram derivation
- User consultation history and journaling
- User authentication and profile management
- Responsive web interface
- RESTful API backend services

### 2.2 Out of Scope (Future Phases)
- Mobile native applications (iOS/Android)
- Yarrow stalk divination method
- Social sharing features
- Community forums or discussions
- Payment processing for premium features
- Additional languages beyond English and Russian
- Integration with external divination systems

---

## 3. Stakeholders

| Stakeholder Group | Interest | Influence Level |
|-------------------|----------|-----------------|
| End Users | Primary system users seeking I Ching guidance | High |
| Product Owner | Business vision and ROI | High |
| Development Team | Technical implementation | High |
| I Ching Scholars/Consultants | Content accuracy and authenticity | Medium |
| System Administrators | System maintenance and monitoring | Medium |

---

## 4. Business Requirements

### 4.1 Functional Requirements

#### FR-001: User Management
**Priority:** High  
**Description:** The system shall provide comprehensive user account management.

**Acceptance Criteria:**
- Users can register with email and password
- Email verification required for account activation
- Users can log in and log out securely
- Password reset functionality via email
- User profile management (name, preferences, timezone)
- JWT-based authentication for API security

#### FR-002: Coin Tossing Simulation
**Priority:** High  
**Description:** The system shall simulate the traditional three-coin I Ching method.

**Acceptance Criteria:**
- Each toss generates three random coin results (heads/tails)
- Six tosses performed to generate a complete hexagram
- Numerical values assigned: 3 for three heads (Yang changing), 2 for two heads + one tail (Yin static), 1 for two tails + one head (Yang static), 0 for three tails (Yin changing)
- Visual representation of coin toss animation
- User-initiated tossing (manual trigger per toss or auto-complete option)
- Randomization algorithm ensuring cryptographically secure random generation

#### FR-003: Hexagram Generation
**Priority:** High  
**Description:** The system shall generate and identify hexagrams based on coin tosses.

**Acceptance Criteria:**
- Build hexagram from bottom line to top line (traditional order)
- Identify the primary hexagram from 64 possible combinations
- Identify changing lines (positions with value 3 or 0)
- Generate relating hexagram when changing lines exist
- Display hexagram with traditional Unicode symbols (☰☷☵☶☴☳☲☱)
- Show hexagram number, Chinese name, and English translation

#### FR-004: Hexagram Interpretation Library
**Priority:** High  
**Description:** The system shall maintain a comprehensive database of all 64 hexagrams with detailed interpretations in multiple languages.

**Acceptance Criteria:**
- Store hexagram number (1-64)
- Chinese name (traditional characters and pinyin)
- English translation/name
- Russian translation/name
- Judgment (primary interpretation) in English and Russian
- Image (symbolic description) in English and Russian
- Line interpretations (for all 6 lines) in English and Russian
- Additional commentary sections (optional) in both languages
- Attribution to source translations (Wilhelm/Baynes for English, Karcher for Russian)
- Support for language-specific content retrieval via API
- Proper copyright attribution and compliance with source websites terms of use

#### FR-005: Consultation Reading
**Priority:** High  
**Description:** The system shall present consultation results with appropriate interpretations.

**Acceptance Criteria:**
- Display primary hexagram with full interpretation
- Highlight changing lines with specific line readings
- Display relating hexagram when applicable
- Present reading in logical, sequential format
- Allow users to add personal notes/reflections to reading
- Provide context on how to interpret results

#### FR-006: Consultation History
**Priority:** Medium  
**Description:** The system shall maintain a history of user consultations.

**Acceptance Criteria:**
- Store all consultation details (date, question, hexagrams, notes)
- Users can view chronological list of past consultations
- Search and filter consultations by date, hexagram, or keyword
- Users can edit notes on past consultations
- Users can delete their own consultations
- Export consultation history (JSON/PDF format)

#### FR-007: Question Recording
**Priority:** Medium  
**Description:** Users shall be able to record their question or intention before consultation.

**Acceptance Criteria:**
- Text input field for question (max 500 characters)
- Optional field - users can proceed without entering a question
- Question stored with consultation record
- Question displayed in consultation results and history

#### FR-008: Dashboard and Analytics
**Priority:** Low  
**Description:** Users shall have a personal dashboard showing consultation patterns.

**Acceptance Criteria:**
- Total consultations count
- Most frequently received hexagrams
- Calendar view of consultation dates
- Recent consultations quick access

#### FR-009: Multi-Language Support
**Priority:** High  
**Description:** The system shall support both English and Russian languages for the user interface and hexagram content.

**Acceptance Criteria:**
- Language selector in application header (English/Russian toggle)
- All UI labels, buttons, messages, and navigation in both languages
- Hexagram names displayed in selected language
- Hexagram interpretations (Judgment, Image, Line readings) in selected language
- User preference for language stored in profile
- Default language based on browser settings
- Consultation history displays in user's selected language
- Language setting persists across sessions
- RTL support not required (both languages use LTR)

### 4.2 Non-Functional Requirements

#### NFR-001: Performance
- API response time < 200ms for standard requests
- Hexagram lookup and calculation < 50ms
- Page load time < 2 seconds on standard broadband
- Support for 100 concurrent users (initial deployment)
- Database query optimization with proper indexing

#### NFR-002: Security
- HTTPS/TLS encryption for all communications
- Password hashing using bcrypt or Argon2
- JWT tokens with 24-hour expiration
- Protection against SQL injection, XSS, CSRF attacks
- Rate limiting on API endpoints
- Secure random number generation for coin tosses

#### NFR-003: Usability
- Responsive design supporting desktop, tablet, and mobile browsers
- Intuitive navigation requiring minimal instructions
- Accessible to WCAG 2.1 AA standards
- Consistent UI/UX patterns throughout application
- Loading indicators for asynchronous operations

#### NFR-004: Reliability
- 99.5% uptime during business hours
- Graceful error handling with user-friendly messages
- Automated database backups daily
- Transaction rollback on consultation save failures
- Application logging for debugging and monitoring

#### NFR-005: Maintainability
- Clean code architecture with separation of concerns
- Comprehensive inline code documentation
- API documentation using Swagger/OpenAPI
- Unit test coverage minimum 70%
- Database migrations using Entity Framework

#### NFR-006: Scalability
- Database design supporting horizontal scaling
- Stateless API design for load balancing capability
- Pagination for list views (20 items per page)
- Caching strategy for hexagram interpretation data

---

## 5. Technical Architecture

### 5.1 Technology Stack

#### Backend
- **Framework:** ASP.NET Core Web API (.NET 8.0 or latest LTS)
- **Database:** SQLite with Entity Framework Core
- **Authentication:** JWT Bearer tokens
- **API Documentation:** Swagger/Swashbuckle

#### Frontend
- **Framework:** Angular (v17 or latest stable)
- **State Management:** RxJS/NgRx (as needed)
- **UI Components:** Angular Material or similar component library with custom theming
- **Internationalization:** Angular i18n or ngx-translate for multi-language support
- **HTTP Client:** Angular HttpClient with interceptors
- **Fonts:** Google Fonts API (Noto Serif SC, Noto Serif, custom display fonts)

#### Development Tools
- **Version Control:** Git
- **API Testing:** Postman/Insomnia
- **Code Quality:** ESLint (frontend), StyleCop (backend)

### 5.2 System Architecture Overview

```
┌─────────────────────────────────────────────┐
│           Angular Frontend (SPA)             │
│  - Components (Consultation, History, etc.)  │
│  - Services (API communication)              │
│  - Guards (Authentication)                   │
└────────────────┬────────────────────────────┘
                 │ HTTPS/JSON
                 │
┌────────────────▼────────────────────────────┐
│         ASP.NET Core Web API                 │
│  - Controllers (REST endpoints)              │
│  - Services (Business logic)                 │
│  - Middleware (Auth, Logging, Error)         │
└────────────────┬────────────────────────────┘
                 │
┌────────────────▼────────────────────────────┐
│      Entity Framework Core (ORM)             │
└────────────────┬────────────────────────────┘
                 │
┌────────────────▼────────────────────────────┐
│            SQLite Database                   │
│  - Users, Consultations, Hexagrams, etc.    │
└─────────────────────────────────────────────┘
```

---

## 6. Data Model

### 6.1 Core Entities

#### User
- UserId (PK, Guid)
- Email (unique, indexed)
- PasswordHash
- FirstName
- LastName
- PreferredLanguage (string, "en" or "ru", default: "en")
- CreatedDate
- LastLoginDate
- IsEmailVerified
- TimeZone

#### Hexagram
- HexagramId (PK, int, 1-64)
- Number (int, 1-64)
- ChineseName
- Pinyin
- EnglishName
- RussianName
- BinarySequence (string, e.g., "111000")
- JudgmentEN (text)
- JudgmentRU (text)
- ImageEN (text)
- ImageRU (text)
- TrigramAbove (string)
- TrigramBelow (string)
- SourceAttributionEN (string, e.g., "Wilhelm/Baynes translation, iching-online.com")
- SourceAttributionRU (string, e.g., "Karcher translation, 64hex.ru")

#### HexagramLine
- LineId (PK, Guid)
- HexagramId (FK)
- LinePosition (int, 1-6)
- LineTextEN (text)
- LineTextRU (text)

#### Consultation
- ConsultationId (PK, Guid)
- UserId (FK)
- Question (string, nullable)
- ConsultationDate (DateTime)
- PrimaryHexagramId (FK)
- RelatingHexagramId (FK, nullable)
- ChangingLines (string, e.g., "2,5")
- UserNotes (text, nullable)
- CoinTossResults (JSON string)

### 6.2 Database Relationships
- One User has Many Consultations
- One Consultation references one Primary Hexagram
- One Consultation references zero or one Relating Hexagram
- One Hexagram has Six HexagramLines

---

## 7. API Endpoints Specification

### 7.1 Authentication APIs

```
POST   /api/auth/register          - Register new user
POST   /api/auth/login             - User login (returns JWT)
POST   /api/auth/refresh-token     - Refresh JWT token
POST   /api/auth/forgot-password   - Request password reset
POST   /api/auth/reset-password    - Reset password with token
GET    /api/auth/verify-email      - Verify email with token
```

### 7.2 User APIs

```
GET    /api/users/profile          - Get current user profile
PUT    /api/users/profile          - Update user profile
PUT    /api/users/change-password  - Change password
DELETE /api/users/account          - Delete user account
```

### 7.3 Hexagram APIs

```
GET    /api/hexagrams              - Get all hexagrams (list)
GET    /api/hexagrams/{id}         - Get hexagram by ID
GET    /api/hexagrams/{id}/lines   - Get hexagram lines
GET    /api/hexagrams/search?q={query} - Search hexagrams
```

**Query Parameters for Hexagram APIs:**
- `lang` - Language code (en/ru) for content retrieval
- Example: `GET /api/hexagrams/1?lang=ru`

### 7.4 Consultation APIs

```
POST   /api/consultations/toss      - Generate single coin toss result
POST   /api/consultations           - Create consultation (6 tosses complete)
GET    /api/consultations           - Get user consultations (paginated)
GET    /api/consultations/{id}      - Get consultation by ID
PUT    /api/consultations/{id}/notes - Update consultation notes
DELETE /api/consultations/{id}      - Delete consultation
GET    /api/consultations/stats     - Get user statistics
GET    /api/consultations/export    - Export consultations
```

---

## 8. User Interface Requirements

### 8.1 Key Screens

#### 8.1.1 Landing Page
- Brief introduction to I Ching and Itzin
- Call-to-action buttons (New Consultation, Login, Register)
- Overview of features
- Responsive hero section with traditional imagery

#### 8.1.2 Registration/Login
- Email and password fields
- Form validation with clear error messages
- "Remember me" checkbox
- Links to password reset
- Social authentication (future phase)

#### 8.1.3 Consultation Screen
- Question input field (optional)
- "Begin Consultation" button
- Coin toss interface with animation
- Progress indicator (tosses 1-6)
- Manual or auto-toss toggle
- Clear visual feedback on each toss result

#### 8.1.4 Results Screen
- Primary hexagram display with Unicode symbol
- Hexagram name (Chinese and English)
- Full interpretation (Judgment, Image)
- Highlighted changing lines with interpretations
- Relating hexagram (if applicable)
- Personal notes textarea
- Save/Edit buttons
- Share functionality (future)

#### 8.1.5 History Screen
- Chronological list of consultations
- Filter by date range
- Search by question keyword
- Each item shows: date, question snippet, hexagram number
- Click to view full consultation
- Pagination controls

#### 8.1.6 Dashboard
- Welcome message with user name
- Quick stats cards (total consultations, recent hexagram, etc.)
- "New Consultation" prominent button
- Recent consultations widget
- Calendar heat map (future enhancement)

#### 8.1.7 Profile Settings
- Edit personal information
- Change password
- Notification preferences (future)
- Export data option
- Delete account option

### 8.2 UI/UX Guidelines
- **Color scheme:** Traditional I Ching aesthetics with ancient Chinese influence
  - Primary colors: Deep red (#8B0000), imperial gold (#FFD700), ink black (#1a1a1a)
  - Secondary colors: Jade green (#00A86B), warm earth tones
  - Background: Aged parchment texture or subtle rice paper effect
- **Typography:** 
  - Headers: Traditional Chinese-inspired serif fonts (e.g., "Noto Serif SC" for Chinese characters)
  - Body text: Clean, readable fonts with subtle Eastern influence
  - Consider "Cinzel" or "IM Fell English" for English headers to evoke ancient text feel
  - Russian Cyrillic fonts: "Noto Serif" or similar with full Cyrillic support
- **Visual Elements:**
  - Traditional Chinese borders with corner ornaments (云纹 cloud patterns, 回纹 meander patterns)
  - Hexagram symbols rendered with traditional calligraphic style
  - Decorative frames inspired by ancient Chinese manuscripts
  - Subtle textures: rice paper, silk, bamboo
  - Chinese knot motifs for dividers or section separators
  - Yin-yang symbol integration where appropriate
  - Traditional Chinese seal (印章) stamps for accents
- **Animations:** 
  - Smooth, purposeful (coin toss with slight ink-brush effect, page transitions)
  - Fade-in effects reminiscent of ink spreading on paper
  - Parallax scrolling for landing page with traditional imagery
- **Responsive breakpoints:** Mobile (< 768px), Tablet (768-1024px), Desktop (> 1024px)
- **UI Components Styling:**
  - Buttons: Rounded corners with subtle border patterns, gold highlights on hover
  - Cards: Shadow effects with traditional border decorations
  - Input fields: Subtle bamboo or traditional frame styling
  - Modals: Center-aligned with ornamental frame borders
- **Cultural Authenticity:**
  - Balance modern usability with traditional aesthetics
  - Avoid over-decoration that impairs functionality
  - Ensure ancient elements enhance rather than distract from content
- **Loading states:** Animated yin-yang or traditional Chinese spinner
- **Toast notifications:** Styled with subtle ancient Chinese border elements
- **Confirmation dialogs:** Framed with traditional decorative patterns

---

## 9. Business Rules

### BR-001: Hexagram Calculation
- Lines are built from bottom (Line 1) to top (Line 6)
- Three heads = 3 (old yang, changing)
- Two heads, one tail = 2 (young yin)
- Two tails, one head = 1 (young yang)
- Three tails = 0 (old yin, changing)
- Only lines with value 0 or 3 are changing lines

### BR-002: Relating Hexagram
- Relating hexagram only exists if at least one changing line is present
- Changing yang lines (3) become yin in relating hexagram
- Changing yin lines (0) become yang in relating hexagram
- Static lines remain unchanged

### BR-003: User Data Privacy
- Users can only access their own consultations
- User email addresses are never displayed to other users
- Deleted consultations are permanently removed (not soft-deleted)
- User data export includes all consultation history

### BR-004: Consultation Integrity
- Once a consultation is completed, the hexagram result cannot be modified
- Users can only edit their personal notes, not the question or toss results
- Coin toss randomization must be cryptographically secure

### BR-005: Account Management
- Email addresses must be unique across the system
- Passwords must meet minimum requirements (8+ characters, 1 uppercase, 1 number)
- Account verification required before first consultation
- Inactive accounts (no login for 2 years) may be flagged for review

### BR-006: Multi-Language Content
- All hexagram interpretations must be available in both English and Russian
- UI text and labels must support both English and Russian completely
- Language preference persists in user profile and applies across all sessions
- Russian translations must use appropriate Cyrillic fonts
- Hexagram Chinese names displayed in traditional characters regardless of selected UI language
- If translation is missing for any hexagram content, system falls back to English with notification
- Date and time formats adjusted according to language selection (EN: MM/DD/YYYY, RU: DD.MM.YYYY)

### BR-007: Cultural Design Authenticity
- UI design must incorporate traditional Chinese visual elements while maintaining modern usability
- Ancient Chinese decorative elements (borders, patterns, fonts) must be culturally accurate and respectful
- Traditional aesthetics should enhance user experience, not impair functionality
- All Chinese characters and symbols must be properly encoded (UTF-8) and display correctly
- Design elements should reflect I Ching's cultural heritage while being accessible to international users

---

## 10. Assumptions and Dependencies

### 10.1 Assumptions
- Users have modern web browsers (Chrome, Firefox, Safari, Edge - last 2 versions)
- Users have stable internet connection
- Content will be in English and Russian languages
- Hexagram interpretations based on Wilhelm/Baynes translation (English) with professional Russian translation
- English hexagram content sourced from https://www.iching-online.com/hexagrams/
- Russian hexagram content sourced from https://64hex.ru/karcher/info.htm
- Copyright and usage rights verified for both content sources
- SQLite sufficient for initial user load (< 10,000 users)
- Web fonts for Chinese characters and Cyrillic properly licensed
- Traditional Chinese design elements are culturally authentic and properly sourced

### 10.2 Dependencies
- .NET 8.0 SDK availability
- Angular CLI and Node.js for frontend development
- **English I Ching Content Source:** https://www.iching-online.com/hexagrams/ (Wilhelm/Baynes translation)
- **Russian I Ching Content Source:** https://64hex.ru/karcher/info.htm (Karcher translation)
- Content scraping/extraction tools or manual data entry for hexagram interpretations
- Copyright clearance and attribution for both content sources
- Web fonts: Google Fonts (Noto Serif SC for Chinese, Noto Serif for Cyrillic)
- Traditional Chinese design assets (borders, patterns, ornaments) with proper licensing
- SSL certificate for HTTPS
- Email service provider for verification/notifications
- Cloud hosting platform (Azure, AWS, or similar)

### 10.3 Constraints
- Budget limitations may restrict initial hosting resources
- Single developer or small team capacity
- Time-to-market goal of 3-4 months for MVP
- SQLite database size limit (practical limit ~1TB)

---

## 11. Success Criteria

### 11.1 Acceptance Criteria
- All high-priority functional requirements implemented
- User can complete full consultation workflow without errors
- System maintains 99% uptime during testing period
- Load testing confirms support for 100 concurrent users
- Security audit identifies no critical vulnerabilities
- Positive feedback from beta user group (> 80% satisfaction)

### 11.2 Key Performance Indicators (KPIs)
- User registration rate
- Consultation completion rate
- Daily/Monthly active users
- Average consultations per user
- User retention rate (30-day, 90-day)
- Page load performance metrics
- API error rate (< 1%)

---

## 12. Risks and Mitigation

| Risk | Impact | Probability | Mitigation Strategy |
|------|--------|-------------|---------------------|
| Content copyright issues | High | Medium | Verify usage rights for iching-online.com and 64hex.ru; seek permission; provide proper attribution; consider fair use doctrine |
| Content source unavailability | High | Low | Download and store all content locally; maintain backup copies; document data extraction date |
| Russian translation quality concerns (Karcher vs Wilhelm) | Medium | Medium | Review translation consistency; engage Russian I Ching expert for validation; consider hybrid approach |
| Chinese font rendering issues | Medium | Low | Use well-supported web fonts (Noto Serif SC); test across browsers; provide font fallbacks |
| Cultural appropriation concerns | Medium | Low | Consult with cultural experts; ensure respectful representation; provide educational context |
| Random number generation not truly random | Medium | Low | Use cryptographically secure RNG; implement testing |
| Database performance degradation | High | Medium | Implement proper indexing; plan migration path to PostgreSQL |
| User adoption lower than expected | High | Medium | Implement analytics; gather feedback; iterate on UX |
| Security vulnerabilities | High | Medium | Security audit; penetration testing; follow OWASP guidelines |
| Browser compatibility issues with ancient fonts/styles | Medium | Medium | Comprehensive cross-browser testing; use polyfills and font fallbacks; progressive enhancement approach |

---

## 13. Implementation Phases

### Phase 1: MVP (Months 1-3)
- User authentication and profile management with language preference
- Core consultation functionality (coin toss, hexagram generation)
- Hexagram interpretation library with English and Russian content
- Multi-language UI implementation (English/Russian toggle)
- Traditional Chinese visual design system (fonts, borders, colors)
- Consultation history (read-only)
- Responsive UI for desktop and mobile with ancient Chinese aesthetics

### Phase 2: Enhancement (Months 4-6)
- Dashboard with analytics
- Advanced search and filtering
- Notes editing on past consultations
- Data export functionality
- Performance optimization

### Phase 3: Growth (Months 7-12)
- User onboarding tutorial
- Email notifications for consultations
- Advanced statistics and insights
- Community features exploration
- Mobile app planning

---

## 14. Glossary

| Term | Definition |
|------|------------|
| Hexagram | Six-line figure representing one of 64 possible states in I Ching |
| Trigram | Three-line figure; two trigrams combine to form a hexagram |
| Changing Line | A line that transforms from yin to yang or vice versa (values 0 or 3) |
| Primary Hexagram | The initial hexagram generated from coin tosses |
| Relating Hexagram | The resulting hexagram after changing lines transform |
| Yang | Active, creative principle (represented by solid line) |
| Yin | Receptive, adaptive principle (represented by broken line) |
| Judgment | Primary interpretation text for a hexagram |
| Image | Symbolic description of hexagram's meaning |
| Wilhelm/Baynes | Standard English translation of I Ching by Richard Wilhelm and Cary Baynes |

---

## 15. Appendices

### Appendix A: Hexagram Calculation Example

**Toss Results:**
1. Three heads → 3 (changing yang)
2. Two heads, one tail → 2 (yin)
3. Two tails, one head → 1 (yang)
4. Three tails → 0 (changing yin)
5. Two heads, one tail → 2 (yin)
6. Two tails, one head → 1 (yang)

**Primary Hexagram:** Bottom to top: 3,2,1,0,2,1  
Binary: 101010 (reading 1 as yang, 0/2 as yin)

**Changing Lines:** Positions 1 and 4

**Relating Hexagram:** Line 1 changes to yin, Line 4 changes to yang  
Binary: 001110

### Appendix B: Reference Documents
- Richard Wilhelm, *I Ching or Book of Changes* (1950)
- **English Hexagram Source:** https://www.iching-online.com/hexagrams/ (Wilhelm/Baynes translation)
- **Russian Hexagram Source:** https://64hex.ru/karcher/info.htm (Karcher translation)
- OWASP Web Application Security Guidelines
- JWT Best Practices (RFC 8725)
- Angular Style Guide (official)
- ASP.NET Core Best Practices

### Appendix C: Content Attribution Requirements

#### English Content
- **Source:** https://www.iching-online.com/hexagrams/
- **Translation:** Wilhelm/Baynes
- **Attribution Format:** "Interpretation based on Wilhelm/Baynes translation, courtesy of iching-online.com"
- **Action Required:** Verify terms of use and obtain permission if necessary
- **Display Location:** Footer of interpretation pages, About section

#### Russian Content
- **Source:** https://64hex.ru/karcher/info.htm
- **Translation:** Karcher (Stephen Karcher)
- **Attribution Format:** "Русский перевод на основе работы Стивена Карчера, источник: 64hex.ru"
- **Action Required:** Contact site administrator for permission; verify copyright status
- **Display Location:** Footer of interpretation pages, About section

#### Important Notes
- Both sources must be properly attributed in the application
- Terms of service for both websites should be reviewed before content extraction
- Consider implementing a "Sources" or "About" page detailing all content origins
- If direct permission cannot be obtained, consult with legal counsel regarding fair use
- Maintain documentation of all correspondence regarding content usage

---

## Document Approval

| Role | Name | Signature | Date |
|------|------|-----------|------|
| Product Owner | [Name] | | |
| Business Analyst | [Name] | | |
| Technical Lead | [Name] | | |
| Stakeholder Representative | [Name] | | |

---

**End of Document**