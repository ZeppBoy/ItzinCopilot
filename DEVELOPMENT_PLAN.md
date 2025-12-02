# Itzin - I Ching Divination System: Development Plan

## Document Control

| Version | Date | Author | Status |
|---------|------|--------|--------|
| 1.0 | November 2025 | Development Team | Draft |

---

## 1. Executive Summary

This development plan outlines the technical implementation strategy for **Itzin**, an I Ching divination web application. The plan is structured in three phases over 12 months, starting with an MVP focusing on core functionality, followed by enhancement and growth phases.

### Technology Stack
- **Frontend:** Angular 17+ (TypeScript)
- **Backend:** ASP.NET Core 8.0 (C#)
- **Database:** SQLite with Entity Framework Core
- **Authentication:** JWT-based with refresh tokens
- **Hosting:** Cloud-based (Azure/AWS)

---

## 2. Architecture Overview

### 2.1 System Architecture
```
┌─────────────────┐         ┌─────────────────┐         ┌─────────────────┐
│  Angular SPA    │ ◄─────► │  ASP.NET Core   │ ◄─────► │     SQLite      │
│  (Frontend)     │  REST   │     Web API     │  EF Core│    Database     │
└─────────────────┘   API   └─────────────────┘         └─────────────────┘
```

### 2.2 Core Modules
1. **Authentication Module** - User registration, login, JWT management
2. **Consultation Module** - Coin toss, hexagram generation
3. **Hexagram Library Module** - 64 hexagrams with interpretations
4. **History Module** - Consultation tracking and journaling
5. **Localization Module** - English/Russian language support
6. **UI/UX Module** - Traditional Chinese aesthetics

---

## 3. Phase 1: MVP Development (Months 1-3)

### Sprint 1: Project Setup & Authentication (Weeks 1-2)

#### Backend Tasks
- [ ] **Task 1.1:** Initialize ASP.NET Core Web API project
  - Configure solution structure (API, Core, Infrastructure layers)
  - Setup dependency injection container
  - Configure logging (Serilog)
  - **Estimate:** 2 days

- [ ] **Task 1.2:** Database setup with Entity Framework Core
  - Design database schema (Users, Consultations, Hexagrams, ConsultationHistory)
  - Create DbContext and connection configuration
  - Setup migrations
  - **Estimate:** 3 days

- [ ] **Task 1.3:** Implement User entity and repository
  - User model with password hashing (BCrypt)
  - Email verification token mechanism
  - User repository with CRUD operations
  - **Estimate:** 2 days

- [ ] **Task 1.4:** JWT Authentication implementation
  - JWT token generation and validation
  - Refresh token mechanism
  - Token expiration handling
  - Secure cookie configuration
  - **Estimate:** 3 days

- [ ] **Task 1.5:** User registration endpoint
  - Email validation and uniqueness check
  - Password strength validation
  - Email verification email sending (SendGrid/SMTP)
  - **Estimate:** 2 days

- [ ] **Task 1.6:** User login endpoint
  - Credentials validation
  - JWT token issuance
  - Login attempt tracking
  - **Estimate:** 2 days

- [ ] **Task 1.7:** Password reset functionality
  - Reset token generation
  - Password reset email
  - Token validation and password update
  - **Estimate:** 2 days

#### Frontend Tasks
- [ ] **Task 1.8:** Initialize Angular project
  - Angular CLI setup with routing
  - Configure environment files
  - Setup base folder structure (modules, components, services)
  - **Estimate:** 1 day

- [ ] **Task 1.9:** Authentication module setup
  - AuthService with HTTP interceptors
  - Token storage (localStorage/sessionStorage)
  - Auth guard for protected routes
  - **Estimate:** 2 days

- [ ] **Task 1.10:** Registration component
  - Form with validation (email, password strength)
  - Error handling and user feedback
  - Email verification notification
  - **Estimate:** 2 days

- [ ] **Task 1.11:** Login component
  - Login form with validation
  - Remember me functionality
  - Redirect after successful login
  - **Estimate:** 2 days

- [ ] **Task 1.12:** Password reset flow
  - Forgot password component
  - Reset password component (with token)
  - Success/error notifications
  - **Estimate:** 2 days

#### Sprint 1 Total: **23 days** (~4.5 weeks)

---

### Sprint 2: Hexagram Library & Data Seeding (Weeks 3-4)

#### Backend Tasks
- [ ] **Task 2.1:** Hexagram entity model
  - Hexagram table (id, number, chinese_name, pinyin, english_name, russian_name)
  - Trigram reference tables
  - Line interpretations structure
  - **Estimate:** 2 days

- [ ] **Task 2.2:** Hexagram repository
  - CRUD operations
  - Search by number, name
  - Query optimization with EF Core includes
  - **Estimate:** 2 days

- [ ] **Task 2.3:** Content scraping/preparation scripts
  - Web scraper for iching-online.com (English)
  - Web scraper for 64hex.ru (Russian)
  - Data validation and cleaning
  - **Estimate:** 4 days

- [ ] **Task 2.4:** Database seeding
  - Create seed data from scraped content
  - Migration with all 64 hexagrams
  - Verify data integrity
  - **Estimate:** 3 days

- [ ] **Task 2.5:** Hexagram API endpoints
  - GET /api/hexagrams (list all)
  - GET /api/hexagrams/{id} (get single with full interpretation)
  - GET /api/hexagrams/search?query={term}
  - **Estimate:** 2 days

#### Frontend Tasks
- [ ] **Task 2.6:** Hexagram service
  - API integration for hexagram retrieval
  - Caching mechanism for hexagram data
  - **Estimate:** 1 day

- [ ] **Task 2.7:** Hexagram list component
  - Display all 64 hexagrams in grid
  - Thumbnail with hexagram symbol and name
  - Navigation to detail view
  - **Estimate:** 2 days

- [ ] **Task 2.8:** Hexagram detail component
  - Full interpretation display
  - Line interpretations
  - Traditional Chinese styling
  - **Estimate:** 3 days

#### Sprint 2 Total: **19 days** (~4 weeks)

---

### Sprint 3: Coin Toss & Hexagram Generation (Weeks 5-7)

#### Backend Tasks
- [ ] **Task 3.1:** Random number generation service
  - Cryptographically secure random (RNGCryptoServiceProvider)
  - Coin toss simulation (3 coins per toss)
  - Value calculation (0-3 based on heads/tails)
  - **Estimate:** 2 days

- [ ] **Task 3.2:** Hexagram calculation engine
  - Build hexagram from 6 tosses (bottom to top)
  - Binary conversion for hexagram identification
  - Changing lines detection
  - Relating hexagram generation
  - **Estimate:** 4 days

- [ ] **Task 3.3:** Consultation entity and repository
  - Consultation model (user_id, question, created_at, toss_results)
  - Primary/relating hexagram references
  - Changing lines JSON field
  - **Estimate:** 2 days

- [ ] **Task 3.4:** Consultation API endpoints
  - POST /api/consultations/toss (single toss)
  - POST /api/consultations/complete (finalize consultation)
  - GET /api/consultations/{id} (retrieve consultation)
  - **Estimate:** 3 days

#### Frontend Tasks
- [ ] **Task 3.5:** Consultation service
  - API integration for toss operations
  - State management for ongoing consultation
  - **Estimate:** 2 days

- [ ] **Task 3.6:** Coin toss component
  - Visual coin animation (CSS/SVG)
  - Toss button with progress indicator (1-6)
  - Display toss results (heads/tails)
  - **Estimate:** 4 days

- [ ] **Task 3.7:** Question input component
  - Form to enter consultation question
  - Character limit and validation
  - Optional field
  - **Estimate:** 1 day

- [ ] **Task 3.8:** Hexagram result component
  - Display primary hexagram with symbol
  - Show changing lines highlighted
  - Display relating hexagram (if applicable)
  - Link to full interpretation
  - **Estimate:** 3 days

- [ ] **Task 3.9:** Consultation flow orchestration
  - Step-by-step wizard (question → tosses → result)
  - Progress tracking
  - Auto-save functionality
  - **Estimate:** 3 days

#### Sprint 3 Total: **24 days** (~5 weeks)

---

### Sprint 4: History & Localization (Weeks 8-10)

#### Backend Tasks
- [ ] **Task 4.1:** Consultation history endpoints
  - GET /api/consultations/history (paginated list)
  - GET /api/consultations/history/{id} (single consultation)
  - Query filtering (date range, hexagram)
  - **Estimate:** 2 days

- [ ] **Task 4.2:** Notes functionality
  - Add notes field to Consultation entity
  - PATCH /api/consultations/{id}/notes endpoint
  - Validation and sanitization
  - **Estimate:** 2 days

- [ ] **Task 4.3:** Localization setup (backend)
  - Resource files for API messages
  - Culture middleware configuration
  - Accept-Language header support
  - **Estimate:** 2 days

#### Frontend Tasks
- [ ] **Task 4.4:** Internationalization (i18n) setup
  - Angular i18n or ngx-translate integration
  - Translation files (en.json, ru.json)
  - Language switcher component
  - **Estimate:** 3 days

- [ ] **Task 4.5:** Translate all UI text
  - Navigation, forms, buttons
  - Error messages
  - Help text and tooltips
  - **Estimate:** 2 days

- [ ] **Task 4.6:** History list component
  - Paginated consultation history
  - Date formatting
  - Filter controls (date, hexagram)
  - **Estimate:** 3 days

- [ ] **Task 4.7:** History detail component
  - Display past consultation details
  - Show question, hexagram, interpretation
  - Notes display
  - **Estimate:** 2 days

- [ ] **Task 4.8:** User profile component
  - Display user information
  - Language preference selection
  - Timezone setting
  - **Estimate:** 2 days

#### Sprint 4 Total: **18 days** (~3.5 weeks)

---

### Sprint 5: UI/UX & Traditional Design (Weeks 11-12)

#### Frontend Tasks
- [ ] **Task 5.1:** Design system setup
  - Color palette (red, gold, black, white)
  - Traditional Chinese fonts (Google Fonts: Noto Serif SC)
  - CSS variables and theme configuration
  - **Estimate:** 2 days

- [ ] **Task 5.2:** Traditional UI components
  - Decorative borders and frames
  - Paper texture backgrounds
  - Chinese seal/stamp elements
  - **Estimate:** 3 days

- [ ] **Task 5.3:** Hexagram symbol rendering
  - Unicode trigram symbols (☰☷☵☶☴☳☲☱)
  - SVG custom hexagram display
  - Animated line changes
  - **Estimate:** 3 days

- [ ] **Task 5.4:** Responsive layout implementation
  - Mobile-first design approach
  - Breakpoints for tablet/desktop
  - Touch-friendly controls
  - **Estimate:** 3 days

- [ ] **Task 5.5:** Navigation and layout
  - Header with logo and language switcher
  - Sidebar/menu navigation
  - Footer with attribution
  - **Estimate:** 2 days

- [ ] **Task 5.6:** Loading states and animations
  - Spinner with Chinese motif
  - Skeleton loaders
  - Transition animations
  - **Estimate:** 2 days

- [ ] **Task 5.7:** Error handling UI
  - 404 page with traditional styling
  - Error messages component
  - Toast notifications
  - **Estimate:** 2 days

#### Backend Tasks
- [ ] **Task 5.8:** API documentation
  - Swagger/OpenAPI setup
  - Endpoint descriptions
  - Example requests/responses
  - **Estimate:** 2 days

#### Sprint 5 Total: **19 days** (~4 weeks)

---

### Phase 1 Testing & Deployment (Week 13)

#### Testing Tasks
- [ ] **Task 6.1:** Unit tests (Backend)
  - Services: RandomGenerator, HexagramCalculator
  - Repositories: User, Hexagram, Consultation
  - Coverage target: >80%
  - **Estimate:** 3 days

- [ ] **Task 6.2:** Unit tests (Frontend)
  - Components: Login, Registration, CoinToss
  - Services: Auth, Consultation, Hexagram
  - Coverage target: >70%
  - **Estimate:** 3 days

- [ ] **Task 6.3:** Integration tests
  - API endpoint testing with test database
  - Authentication flow testing
  - Consultation flow testing
  - **Estimate:** 2 days

- [ ] **Task 6.4:** E2E tests
  - Cypress/Playwright setup
  - Critical user journeys (registration → consultation → history)
  - **Estimate:** 2 days

- [ ] **Task 6.5:** Manual QA testing
  - Cross-browser testing (Chrome, Firefox, Safari, Edge)
  - Mobile device testing (iOS, Android)
  - Accessibility testing (WCAG 2.1)
  - **Estimate:** 3 days

#### Deployment Tasks
- [ ] **Task 6.6:** Environment setup
  - SQLite database file setup and backup strategy
  - Cloud hosting configuration (Azure App Service/AWS)
  - SSL certificate setup
  - **Estimate:** 1 day

- [ ] **Task 6.7:** CI/CD pipeline
  - GitHub Actions/Azure DevOps setup
  - Automated build and test
  - Deployment automation
  - **Estimate:** 2 days

- [ ] **Task 6.8:** Production deployment
  - Database migration execution
  - Application deployment
  - Smoke testing
  - **Estimate:** 1 day

#### Phase 1 Total Testing & Deployment: **18 days** (~3.5 weeks)

---

## **Phase 1 Total Duration: 13 weeks (~3 months)**

---

## 4. Phase 2: Enhancement (Months 4-6)

### Sprint 6: Dashboard & Analytics (Weeks 14-16)

#### Backend Tasks
- [ ] **Task 7.1:** Analytics service
  - Consultation statistics (total, by month, by hexagram)
  - Most consulted hexagrams
  - Consultation frequency tracking
  - **Estimate:** 3 days

- [ ] **Task 7.2:** Dashboard API endpoints
  - GET /api/dashboard/stats (overall statistics)
  - GET /api/dashboard/recent (recent consultations)
  - GET /api/dashboard/trends (time-based data)
  - **Estimate:** 2 days

#### Frontend Tasks
- [ ] **Task 7.3:** Dashboard component
  - Statistics cards (total consultations, this month, etc.)
  - Charts (consultation frequency, hexagram distribution)
  - Recent consultations list
  - **Estimate:** 4 days

- [ ] **Task 7.4:** Chart library integration
  - Chart.js or D3.js setup
  - Line chart for consultation trends
  - Pie chart for hexagram distribution
  - **Estimate:** 3 days

- [ ] **Task 7.5:** Dashboard navigation
  - Make dashboard the home page after login
  - Quick access to new consultation
  - **Estimate:** 1 day

#### Sprint 6 Total: **13 days** (~2.5 weeks)

---

### Sprint 7: Advanced Search & Filtering (Weeks 17-19)

#### Backend Tasks
- [ ] **Task 8.1:** Advanced search implementation
  - Full-text search on hexagram interpretations (SQLite FTS5)
  - Search by keywords, themes
  - LIKE-based search optimization
  - **Estimate:** 3 days

- [ ] **Task 8.2:** Filter enhancements
  - Filter by hexagram family/trigram
  - Filter by changing lines pattern
  - Date range improvements
  - **Estimate:** 3 days

#### Frontend Tasks
- [ ] **Task 8.3:** Advanced search component
  - Search bar with autocomplete
  - Filter panel with multiple criteria
  - Search results display
  - **Estimate:** 3 days

- [ ] **Task 8.4:** Search UX improvements
  - Search history
  - Saved searches
  - Result highlighting
  - **Estimate:** 2 days

- [ ] **Task 8.5:** Hexagram comparison feature
  - Side-by-side hexagram comparison
  - Highlight differences
  - **Estimate:** 3 days

#### Sprint 7 Total: **15 days** (~3 weeks)

---

### Sprint 8: Notes & Data Export (Weeks 20-22)

#### Backend Tasks
- [ ] **Task 9.1:** Rich text notes support
  - Update notes to support markdown/HTML
  - Sanitization for XSS prevention
  - **Estimate:** 2 days

- [ ] **Task 9.2:** Export functionality
  - PDF export service (consultation + interpretation)
  - JSON export for backup
  - CSV export for analytics
  - **Estimate:** 4 days

- [ ] **Task 9.3:** Export API endpoints
  - POST /api/export/consultation/{id}/pdf
  - GET /api/export/history (JSON/CSV)
  - Async processing for large exports
  - **Estimate:** 2 days

#### Frontend Tasks
- [ ] **Task 9.4:** Rich text editor integration
  - TinyMCE or Quill.js integration
  - Notes editing in history detail
  - Auto-save functionality
  - **Estimate:** 3 days

- [ ] **Task 9.5:** Export UI
  - Export buttons in history and consultation views
  - Format selection (PDF, JSON, CSV)
  - Download progress indicator
  - **Estimate:** 2 days

- [ ] **Task 9.6:** Tags system
  - Tag creation and management
  - Tag consultation assignments
  - Filter by tags
  - **Estimate:** 3 days

#### Sprint 8 Total: **16 days** (~3 weeks)

---

### Sprint 9: Performance Optimization (Weeks 23-24)

#### Backend Tasks
- [ ] **Task 10.1:** Database optimization
  - Add missing indexes
  - Query performance analysis
  - SQLite pragma optimization (WAL mode, cache size)
  - **Estimate:** 2 days

- [ ] **Task 10.2:** Caching implementation
  - In-memory cache implementation (IMemoryCache)
  - Optional: Redis cache for distributed scenarios
  - Cache hexagram data
  - Cache dashboard statistics
  - **Estimate:** 2 days

- [ ] **Task 10.3:** API response optimization
  - Pagination improvements
  - Response compression (gzip)
  - Lazy loading for interpretation sections
  - **Estimate:** 2 days

#### Frontend Tasks
- [ ] **Task 10.4:** Bundle size optimization
  - Lazy loading modules
  - Tree shaking unused code
  - Image optimization
  - **Estimate:** 2 days

- [ ] **Task 10.5:** Rendering performance
  - Virtual scrolling for long lists
  - Change detection optimization
  - Memoization for expensive calculations
  - **Estimate:** 2 days

- [ ] **Task 10.6:** PWA implementation
  - Service worker setup
  - Offline functionality
  - App manifest
  - **Estimate:** 3 days

#### Sprint 9 Total: **14 days** (~2.8 weeks)

---

### Phase 2 Testing & Bug Fixes (Week 25-26)

- [ ] **Regression testing** (all Phase 1 features)
- [ ] **Performance testing** (load testing, stress testing)
- [ ] **Security audit** (penetration testing, vulnerability scanning)
- [ ] **User acceptance testing** (UAT with beta users)
- [ ] **Bug fixes and polish**

#### Phase 2 Total: **16 days** (~3 weeks)

---

## **Phase 2 Total Duration: 12 weeks (~3 months)**

---

## 5. Phase 3: Growth (Months 7-12)

### Sprint 10: User Onboarding (Weeks 27-29)

#### Frontend Tasks
- [ ] **Task 11.1:** Onboarding tutorial
  - Interactive walkthrough for first-time users
  - Tooltips and guided tour
  - Skip/dismiss functionality
  - **Estimate:** 4 days

- [ ] **Task 11.2:** Help system
  - FAQ page
  - Contextual help buttons
  - Video tutorials (optional)
  - **Estimate:** 3 days

- [ ] **Task 11.3:** About page
  - I Ching introduction
  - How to use the system
  - Content attribution
  - **Estimate:** 2 days

#### Sprint 10 Total: **9 days** (~2 weeks)

---

### Sprint 11: Email Notifications (Weeks 30-32)

#### Backend Tasks
- [ ] **Task 12.1:** Email template system
  - HTML email templates
  - Personalization tokens
  - Multi-language support
  - **Estimate:** 3 days

- [ ] **Task 12.2:** Notification preferences
  - User preference entity (email_notifications, frequency)
  - Preference management endpoints
  - **Estimate:** 2 days

- [ ] **Task 12.3:** Scheduled notifications
  - Daily/weekly consultation reminders
  - Background job scheduling (Hangfire)
  - Unsubscribe mechanism
  - **Estimate:** 4 days

#### Frontend Tasks
- [ ] **Task 12.4:** Notification preferences UI
  - Settings page for email preferences
  - Frequency selection (daily, weekly, none)
  - **Estimate:** 2 days

#### Sprint 11 Total: **11 days** (~2 weeks)

---

### Sprint 12: Advanced Statistics (Weeks 33-36)

#### Backend Tasks
- [ ] **Task 13.1:** Advanced analytics engine
  - Pattern recognition (recurring hexagrams)
  - Temporal analysis (hexagram changes over time)
  - Consultation insights generation
  - **Estimate:** 5 days

- [ ] **Task 13.2:** Insights API
  - GET /api/insights/patterns
  - GET /api/insights/recommendations
  - Machine learning integration (optional)
  - **Estimate:** 4 days

#### Frontend Tasks
- [ ] **Task 13.3:** Insights dashboard
  - Personal insights display
  - Pattern visualization
  - Recommendations section
  - **Estimate:** 4 days

- [ ] **Task 13.4:** Advanced charts
  - Hexagram evolution timeline
  - Changing lines heatmap
  - Interactive data exploration
  - **Estimate:** 3 days

#### Sprint 12 Total: **16 days** (~3 weeks)

---

### Sprint 13: Community Features Exploration (Weeks 37-40)

#### Research & Planning
- [ ] **Task 14.1:** Community features research
  - User forums feasibility study
  - Social features design (sharing consultations)
  - Moderation requirements
  - **Estimate:** 3 days

- [ ] **Task 14.2:** Feature prototyping
  - Basic commenting system POC
  - Public/private consultation sharing
  - Community guidelines draft
  - **Estimate:** 5 days

#### Implementation (if approved)
- [ ] **Task 14.3:** Community infrastructure
  - Comments entity and endpoints
  - Sharing mechanism
  - Privacy controls
  - **Estimate:** 5 days

- [ ] **Task 14.4:** Community UI
  - Public consultations feed
  - Comment threads
  - Report/moderation tools
  - **Estimate:** 4 days

#### Sprint 13 Total: **17 days** (~3.5 weeks)

---

### Sprint 14: Mobile App Planning (Weeks 41-44)

#### Planning Tasks
- [ ] **Task 15.1:** Mobile requirements analysis
  - Native vs hybrid vs PWA decision
  - Platform prioritization (iOS vs Android)
  - Feature parity assessment
  - **Estimate:** 3 days

- [ ] **Task 15.2:** Mobile architecture design
  - Technology selection (React Native, Flutter, Xamarin)
  - API adjustments for mobile
  - Offline-first strategy
  - **Estimate:** 4 days

- [ ] **Task 15.3:** Mobile UI/UX design
  - Wireframes for key screens
  - Navigation patterns
  - Traditional design adaptation for mobile
  - **Estimate:** 5 days

- [ ] **Task 15.4:** Mobile development roadmap
  - Sprint planning for mobile
  - Resource allocation
  - Timeline estimation
  - **Estimate:** 2 days

#### Sprint 14 Total: **14 days** (~3 weeks)

---

### Phase 3 Finalization (Weeks 45-48)

#### Ongoing Tasks
- [ ] **Continuous monitoring and optimization**
- [ ] **User feedback collection and analysis**
- [ ] **Bug fixes and minor enhancements**
- [ ] **Security updates and patches**
- [ ] **Performance monitoring and tuning**
- [ ] **Content updates (hexagram interpretations)**
- [ ] **Marketing and user acquisition initiatives**

#### Phase 3 Total: **24 weeks** (~6 months)

---

## 6. Technical Debt Management

### Ongoing Throughout All Phases
- **Code reviews:** Mandatory for all pull requests
- **Refactoring sprints:** 1 week every 2 months
- **Dependency updates:** Monthly security and feature updates
- **Documentation updates:** Concurrent with feature development
- **Performance audits:** Quarterly

---

## 7. Risk Management

| Risk | Mitigation Strategy | Owner |
|------|---------------------|-------|
| Content scraping blocked | Manual content entry backup; API access negotiation | Backend Lead |
| Authentication vulnerabilities | OWASP guidelines; security audit in Phase 2 | Security Specialist |
| Performance issues at scale | Load testing; caching strategy; CDN | DevOps Engineer |
| Browser compatibility | Cross-browser testing; polyfills; progressive enhancement | Frontend Lead |
| Scope creep | Strict prioritization; phase gates | Project Manager |
| Resource availability | Cross-training; documentation; knowledge sharing | Tech Lead |

---

## 8. Team Structure

### Core Team
- **1x Tech Lead / Senior Full Stack Developer** (you)
- **1x Backend Developer** (C#/.NET)
- **1x Frontend Developer** (Angular/TypeScript)
- **1x UI/UX Designer** (Traditional Chinese design expertise)
- **1x QA Engineer** (Automation & Manual Testing)
- **1x DevOps Engineer** (Part-time, shared)
- **1x Product Owner** (Business/Stakeholder liaison)

### Extended Team (Phase 3)
- **1x Data Analyst** (Analytics & Insights)
- **1x Content Specialist** (I Ching expertise)
- **1x Mobile Developer** (Phase 3 mobile planning)

---

## 9. Development Standards

### Code Quality
- **Backend:** Follow SOLID principles, clean architecture
- **Frontend:** Angular style guide, reactive patterns (RxJS)
- **Testing:** TDD approach, minimum 80% coverage
- **Documentation:** XML comments (C#), JSDoc (TypeScript)

### Version Control
- **Git workflow:** GitFlow (main, develop, feature, release, hotfix branches)
- **Commit messages:** Conventional Commits format
- **Pull requests:** Require 1 approval, all checks passing

### Security
- **Authentication:** JWT with short expiration, refresh tokens
- **Authorization:** Role-based access control (RBAC)
- **Data protection:** Encryption at rest and in transit
- **Input validation:** Server-side validation, parameterized queries
- **OWASP Top 10:** Address all vulnerabilities

---

## 10. Success Metrics

### Technical KPIs
- **API response time:** <200ms for 95th percentile
- **Frontend load time:** <3s for initial load
- **Test coverage:** Backend >80%, Frontend >70%
- **Bug rate:** <5 bugs per 1000 lines of code
- **Uptime:** 99.9% availability

### Business KPIs (Phase 1)
- **User registrations:** 1000+ in first 3 months
- **Daily active users:** 100+ by end of Phase 1
- **Consultations per user:** Average 5+ per month
- **User retention:** 40%+ after 30 days

---

## 11. Deliverables by Phase

### Phase 1 (MVP)
- ✅ Functional web application (frontend + backend)
- ✅ User authentication and profile management
- ✅ Core consultation functionality (coin toss, hexagram generation)
- ✅ Complete hexagram library (64 hexagrams, EN/RU)
- ✅ Consultation history (read-only)
- ✅ Multi-language UI (English/Russian)
- ✅ Traditional Chinese visual design
- ✅ Responsive design (desktop, tablet, mobile)
- ✅ Deployed to production environment
- ✅ API documentation (Swagger)
- ✅ User documentation (help pages)

### Phase 2 (Enhancement)
- ✅ Dashboard with analytics and insights
- ✅ Advanced search and filtering
- ✅ Notes editing on consultations
- ✅ Data export (PDF, JSON, CSV)
- ✅ Performance optimizations
- ✅ PWA capabilities
- ✅ Caching layer (Redis)

### Phase 3 (Growth)
- ✅ User onboarding tutorial
- ✅ Email notification system
- ✅ Advanced statistics and pattern recognition
- ✅ Community features (if approved)
- ✅ Mobile app plan and roadmap
- ✅ Marketing materials and landing page

---

## 12. Timeline Summary

| Phase | Duration | Key Deliverables |
|-------|----------|------------------|
| **Phase 1: MVP** | 13 weeks (3 months) | Core functionality, authentication, hexagram library, basic history |
| **Phase 2: Enhancement** | 12 weeks (3 months) | Dashboard, advanced features, performance optimization |
| **Phase 3: Growth** | 24 weeks (6 months) | Onboarding, notifications, advanced analytics, mobile planning |
| **Total** | **49 weeks (~12 months)** | Full-featured I Ching divination platform |

---

## 13. Next Steps

### Immediate Actions (Week 1)
1. **Environment setup:** Install development tools, IDEs, database
2. **Repository creation:** Initialize Git repository, setup CI/CD pipelines
3. **Team kickoff meeting:** Review plan, assign initial tasks, establish communication channels
4. **Design review:** Finalize UI/UX mockups with designer
5. **Content licensing:** Contact iching-online.com and 64hex.ru for permission

### Weekly Rituals
- **Monday:** Sprint planning, task assignment
- **Daily:** 15-minute standup (what done, what next, blockers)
- **Wednesday:** Mid-sprint check-in
- **Friday:** Demo and retrospective

### Monthly Reviews
- **Progress review:** Against timeline and deliverables
- **Budget review:** Resource allocation and spending
- **Risk assessment:** Identify new risks, update mitigation strategies
- **Stakeholder demo:** Present completed features

---

## 14. Appendices

### Appendix A: Technology Justification

#### Backend: ASP.NET Core 8.0
- **Pros:** High performance, mature ecosystem, excellent tooling, strong typing
- **Cons:** Learning curve for non-.NET developers
- **Alternatives considered:** Node.js (less structured), Python/Django (slower)

#### Frontend: Angular 17+
- **Pros:** Full framework, TypeScript, enterprise-ready, strong architecture
- **Cons:** Steeper learning curve than React
- **Alternatives considered:** React (more flexible but less opinionated), Vue (smaller ecosystem)

#### Database: SQLite
- **Pros:** Zero configuration, serverless, embedded, file-based, excellent for small-to-medium workloads, no separate database server needed
- **Cons:** Limited concurrent writes, not ideal for high-traffic scenarios (but sufficient for MVP and moderate use)
- **Alternatives considered:** PostgreSQL (overkill for initial requirements), SQL Server (cost and complexity), MongoDB (less relational fit)
- **Migration path:** Can migrate to PostgreSQL/SQL Server in future if traffic demands

### Appendix B: Key Dependencies

#### Backend
- ASP.NET Core 8.0
- Entity Framework Core 8.0
- Microsoft.EntityFrameworkCore.Sqlite (SQLite provider)
- BCrypt.Net (password hashing)
- System.IdentityModel.Tokens.Jwt (JWT)
- Serilog (logging)
- Swashbuckle (Swagger/OpenAPI)
- Hangfire (background jobs - Phase 3)
- StackExchange.Redis (caching - Phase 2, optional)

#### Frontend
- Angular 17+
- RxJS 7+
- Angular Material or PrimeNG (UI components)
- ngx-translate (i18n)
- Chart.js or D3.js (visualizations - Phase 2)
- Quill.js or TinyMCE (rich text editor - Phase 2)

---

## Document Approval

| Role | Name | Signature | Date |
|------|------|-----------|------|
| Tech Lead | [Your Name] | | |
| Product Owner | [Name] | | |
| Project Manager | [Name] | | |
| Stakeholder | [Name] | | |

---

**End of Development Plan**

---

## Change Log

| Date | Version | Change Description | Author |
|------|---------|-------------------|--------|
| Nov 2025 | 1.0 | Initial development plan creation | Development Team |

