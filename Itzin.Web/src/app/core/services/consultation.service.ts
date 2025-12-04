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

  // Call API to generate coin toss
  tossCoins(): Observable<CoinTossResult> {
    const tossNumber = this.tossResultsSubject.value.length + 1;
    return this.http.post<CoinTossResult>(`${this.apiUrl}/toss?tossNumber=${tossNumber}`, {});
  }
}
