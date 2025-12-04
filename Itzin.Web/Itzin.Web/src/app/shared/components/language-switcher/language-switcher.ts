import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { TranslateModule } from '@ngx-translate/core';
import { LanguageService, Language } from '../../../core/services/language.service';

@Component({
  selector: 'app-language-switcher',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatMenuModule,
    MatIconModule,
    TranslateModule
  ],
  template: `
    <button mat-icon-button [matMenuTriggerFor]="languageMenu" class="language-button">
      <mat-icon>language</mat-icon>
    </button>
    <mat-menu #languageMenu="matMenu">
      <button 
        mat-menu-item 
        *ngFor="let lang of languages"
        (click)="changeLanguage(lang)"
        [class.active]="lang === currentLanguage">
        <mat-icon *ngIf="lang === currentLanguage">check</mat-icon>
        <span>{{ 'languages.' + lang | translate }}</span>
      </button>
    </mat-menu>
  `,
  styles: [`
    .language-button {
      color: var(--color-text-primary, #333);
    }

    .active {
      background-color: rgba(0, 0, 0, 0.04);
      font-weight: 500;
    }

    mat-icon {
      margin-right: 8px;
    }
  `]
})
export class LanguageSwitcherComponent {
  private languageService = inject(LanguageService);
  
  languages: Language[] = this.languageService.getAvailableLanguages();
  currentLanguage: Language = this.languageService.getCurrentLanguage();

  constructor() {
    this.languageService.currentLanguage$.subscribe(lang => {
      this.currentLanguage = lang;
    });
  }

  changeLanguage(lang: Language): void {
    this.languageService.setLanguage(lang);
  }
}
