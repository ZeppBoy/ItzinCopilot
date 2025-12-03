import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Hexagram, HexagramListItem } from '../models/hexagram.model';

@Injectable({
  providedIn: 'root'
})
export class HexagramService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/hexagrams`;

  getAll(language: 'en' | 'ru' = 'en'): Observable<HexagramListItem[]> {
    const params = new HttpParams().set('language', language);
    return this.http.get<HexagramListItem[]>(this.apiUrl, { params });
  }

  getById(id: number, language: 'en' | 'ru' = 'en'): Observable<Hexagram> {
    const params = new HttpParams().set('language', language);
    return this.http.get<Hexagram>(`${this.apiUrl}/${id}`, { params });
  }

  getByNumber(number: number, language: 'en' | 'ru' = 'en'): Observable<Hexagram> {
    const params = new HttpParams().set('language', language);
    return this.http.get<Hexagram>(`${this.apiUrl}/number/${number}`, { params });
  }
}
