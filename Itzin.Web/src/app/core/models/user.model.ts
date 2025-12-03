export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  preferredLanguage: 'en' | 'ru';
  isEmailVerified: boolean;
  createdAt: Date;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  preferredLanguage: 'en' | 'ru';
}

export interface AuthResponse {
  token: string;
  refreshToken: string;
  user: User;
}

export interface PasswordResetRequest {
  email: string;
}

export interface PasswordResetConfirm {
  token: string;
  newPassword: string;
}
