import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { 
  User, 
  LoginRequest, 
  RegisterRequest, 
  AuthResponse,
  PasswordResetRequest,
  PasswordResetConfirm
} from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();

  private readonly TOKEN_KEY = 'auth_token';
  private readonly REFRESH_TOKEN_KEY = 'refresh_token';

  constructor() {
    this.loadUserFromStorage();
  }

  private loadUserFromStorage(): void {
    const token = this.getToken();
    if (token) {
      // Decode JWT to get user info
      const payload = this.decodeToken(token);
      if (payload && payload.exp * 1000 > Date.now()) {
        this.currentUserSubject.next(this.extractUserFromToken(payload));
      } else {
        this.clearTokens();
      }
    }
  }

  register(request: RegisterRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${environment.apiUrl}/auth/register`, request)
      .pipe(tap(response => this.handleAuthResponse(response)));
  }

  login(request: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${environment.apiUrl}/auth/login`, request)
      .pipe(tap(response => this.handleAuthResponse(response)));
  }

  logout(): void {
    this.clearTokens();
    this.currentUserSubject.next(null);
  }

  requestPasswordReset(request: PasswordResetRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiUrl}/auth/request-password-reset`, request);
  }

  resetPassword(request: PasswordResetConfirm): Observable<void> {
    return this.http.post<void>(`${environment.apiUrl}/auth/reset-password`, request);
  }

  verifyEmail(email: string, token: string): Observable<void> {
    return this.http.post<void>(`${environment.apiUrl}/auth/verify-email`, { email, token });
  }

  refreshToken(): Observable<AuthResponse> {
    const refreshToken = this.getRefreshToken();
    return this.http.post<AuthResponse>(`${environment.apiUrl}/auth/refresh`, { refreshToken })
      .pipe(tap(response => this.handleAuthResponse(response)));
  }

  private handleAuthResponse(response: AuthResponse): void {
    localStorage.setItem(this.TOKEN_KEY, response.token);
    localStorage.setItem(this.REFRESH_TOKEN_KEY, response.refreshToken);
    this.currentUserSubject.next(response.user);
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  getRefreshToken(): string | null {
    return localStorage.getItem(this.REFRESH_TOKEN_KEY);
  }

  private clearTokens(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.removeItem(this.REFRESH_TOKEN_KEY);
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    if (!token) return false;
    
    const payload = this.decodeToken(token);
    return payload && payload.exp * 1000 > Date.now();
  }

  private decodeToken(token: string): any {
    try {
      const payload = token.split('.')[1];
      return JSON.parse(atob(payload));
    } catch {
      return null;
    }
  }

  private extractUserFromToken(payload: any): User {
    return {
      id: payload.sub || payload.userId,
      email: payload.email,
      firstName: payload.firstName || '',
      lastName: payload.lastName || '',
      preferredLanguage: payload.preferredLanguage || 'en',
      isEmailVerified: payload.isEmailVerified === 'true' || payload.isEmailVerified === true,
      createdAt: new Date(payload.createdAt)
    };
  }

  getCurrentUser(): User | null {
    return this.currentUserSubject.value;
  }
}
