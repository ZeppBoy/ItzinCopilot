import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { 
    path: 'login', 
    loadComponent: () => import('./features/auth/components/login/login.component')
      .then(m => m.LoginComponent)
  },
  { 
    path: 'register', 
    loadComponent: () => import('./features/auth/components/register/register.component')
      .then(m => m.RegisterComponent)
  },
  { 
    path: 'forgot-password', 
    loadComponent: () => import('./features/auth/components/password-reset/forgot-password/forgot-password')
      .then(m => m.ForgotPassword)
  },
  { 
    path: 'reset-password', 
    loadComponent: () => import('./features/auth/components/password-reset/reset-password/reset-password')
      .then(m => m.ResetPassword)
  },
  { 
    path: 'dashboard', 
    loadComponent: () => import('./features/dashboard/dashboard.component')
      .then(m => m.DashboardComponent),
    canActivate: [authGuard]
  },
  { 
    path: 'hexagrams', 
    loadComponent: () => import('./features/hexagrams/hexagram-list/hexagram-list')
      .then(m => m.HexagramList),
    canActivate: [authGuard]
  },
  { 
    path: 'hexagrams/:id', 
    loadComponent: () => import('./features/hexagrams/hexagram-detail/hexagram-detail')
      .then(m => m.HexagramDetail),
    canActivate: [authGuard]
  },
  { 
    path: 'consultation', 
    loadComponent: () => import('./features/consultation/consultation-flow/consultation-flow')
      .then(m => m.ConsultationFlow),
    canActivate: [authGuard]
  },
  { 
    path: 'history', 
    loadComponent: () => import('./features/history/history-list/history-list')
      .then(m => m.HistoryList),
    canActivate: [authGuard]
  },
  { 
    path: 'history/:id', 
    loadComponent: () => import('./features/history/history-detail/history-detail')
      .then(m => m.HistoryDetail),
    canActivate: [authGuard]
  },
  { path: '**', redirectTo: '/login' }
];
