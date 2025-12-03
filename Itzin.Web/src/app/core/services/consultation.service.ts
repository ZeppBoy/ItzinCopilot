import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Consultation, CreateConsultationRequest, CoinTossResult, CoinResult } from '../models/consultation.model';

@Injectable({
  providedIn: 'root'
})
export class ConsultationService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/consultations`;

  private tossResultsSubject = new BehaviorSubject<CoinTossResult[]>([]);
  public tossResults$ = this.tossResultsSubject.asObservable();

  private currentQuestionSubject = new BehaviorSubject<string>('');
  public currentQuestion$ = this.currentQuestionSubject.asObservable();

  setQuestion(question: string): void {
    this.currentQuestionSubject.next(question);
  }

  addTossResult(result: CoinTossResult): void {
    const current = this.tossResultsSubject.value;
    this.tossResultsSubject.next([...current, result]);
  }

  clearTossResults(): void {
    this.tossResultsSubject.next([]);
    this.currentQuestionSubject.next('');
  }

  getTossResults(): CoinTossResult[] {
    return this.tossResultsSubject.value;
  }

  createConsultation(request: CreateConsultationRequest): Observable<Consultation> {
    return this.http.post<Consultation>(this.apiUrl, request);
  }

  getConsultationById(id: number): Observable<Consultation> {
    return this.http.get<Consultation>(`${this.apiUrl}/${id}`);
  }

  getUserConsultations(): Observable<Consultation[]> {
    return this.http.get<Consultation[]>(this.apiUrl);
  }

  updateConsultationNotes(id: number, notes: string): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${id}/notes`, { notes });
  }

  // Simulate coin toss (3 coins)
  tossCoins(): CoinTossResult {
    const coins: CoinResult[] = Array(3).fill(0).map(() => ({
      value: (Math.random() > 0.5 ? 'heads' : 'tails') as 'heads' | 'tails'
    }));
    
    // Calculate line value: heads=3, tails=2
    const lineValue = coins.reduce((sum, coin) => 
      sum + (coin.value === 'heads' ? 3 : 2), 0
    );
    
    // Determine line type and if it's changing
    // 6 = old yin (changing), 7 = young yang, 8 = young yin, 9 = old yang (changing)
    let lineType: 'yang' | 'yin';
    let isChanging: boolean;
    
    if (lineValue === 6) {
      lineType = 'yin';
      isChanging = true;
    } else if (lineValue === 7) {
      lineType = 'yang';
      isChanging = false;
    } else if (lineValue === 8) {
      lineType = 'yin';
      isChanging = false;
    } else { // lineValue === 9
      lineType = 'yang';
      isChanging = true;
    }

    const tossNumber = this.tossResultsSubject.value.length + 1;

    return {
      tossNumber,
      coins,
      lineValue,
      lineType,
      isChanging
    };
  }
}
