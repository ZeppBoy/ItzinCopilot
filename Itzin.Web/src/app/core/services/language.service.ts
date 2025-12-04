import { Injectable, inject } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { BehaviorSubject, Observable } from 'rxjs';

export type Language = 'en' | 'ru';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {
  private translate = inject(TranslateService);
  private currentLanguageSubject = new BehaviorSubject<Language>('en');
  public currentLanguage$ = this.currentLanguageSubject.asObservable();

  private readonly LANGUAGE_KEY = 'itzin_language';
  private readonly availableLanguages: Language[] = ['en', 'ru'];

  constructor() {
    this.initializeLanguage();
  }

  private initializeLanguage(): void {
    const savedLanguage = localStorage.getItem(this.LANGUAGE_KEY) as Language;
    const browserLanguage = this.getBrowserLanguage();
    
    const initialLanguage = savedLanguage || browserLanguage || 'en';
    
    this.translate.addLangs(this.availableLanguages);
    this.translate.setDefaultLang('en');
    this.setLanguage(initialLanguage);
  }

  private getBrowserLanguage(): Language | null {
    const browserLang = this.translate.getBrowserLang();
    if (browserLang && this.availableLanguages.includes(browserLang as Language)) {
      return browserLang as Language;
    }
    return null;
  }

  public setLanguage(lang: Language): void {
    if (!this.availableLanguages.includes(lang)) {
      lang = 'en';
    }
    
    this.translate.use(lang);
    this.currentLanguageSubject.next(lang);
    localStorage.setItem(this.LANGUAGE_KEY, lang);
    
    // Update HTML lang attribute for accessibility
    document.documentElement.lang = lang;
  }

  public getCurrentLanguage(): Language {
    return this.currentLanguageSubject.value;
  }

  public getAvailableLanguages(): Language[] {
    return [...this.availableLanguages];
  }

  public translate$(key: string, params?: object): Observable<string> {
    return this.translate.stream(key, params);
  }

  public instantTranslate(key: string, params?: object): string {
    return this.translate.instant(key, params);
  }
}
