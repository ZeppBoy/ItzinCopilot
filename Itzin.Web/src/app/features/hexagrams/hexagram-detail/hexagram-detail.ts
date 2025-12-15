import { Component, inject, OnInit } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { HexagramService } from '../../../core/services/hexagram.service';
import { Hexagram } from '../../../core/models/hexagram.model';

@Component({
  selector: 'app-hexagram-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './hexagram-detail.html',
  styleUrl: './hexagram-detail.scss',
})
export class HexagramDetail implements OnInit {
  private hexagramService = inject(HexagramService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private location = inject(Location);

  hexagram: Hexagram | null = null;
  isLoading: boolean = true;
  errorMessage: string = '';
  currentLanguage: 'en' | 'ru' = 'ru'; // Changed to Russian by default
  changingLines: number[] = []; // Track changing lines from consultation

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    
    // Check for changing lines in query params
    const changingLinesParam = this.route.snapshot.queryParams['changingLines'];
    if (changingLinesParam) {
      this.changingLines = changingLinesParam.split(',').map((n: string) => parseInt(n, 10));
    }
    
    if (id) {
      this.loadHexagram(+id);
    } else {
      this.errorMessage = 'Invalid hexagram ID';
      this.isLoading = false;
    }
  }

  loadHexagram(id: number): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.hexagramService.getById(id, this.currentLanguage).subscribe({
      next: (hexagram) => {
        this.hexagram = hexagram;
        this.isLoading = false;
        console.log('Loaded hexagram with RuDescription:', hexagram);
      },
      error: (error) => {
        this.errorMessage = 'Failed to load hexagram. Please try again.';
        this.isLoading = false;
        console.error('Error loading hexagram:', error);
      }
    });
  }

  isLineChanging(lineNumber: number): boolean {
    return this.changingLines.includes(lineNumber);
  }

  getLineClass(lineNumber: number): string {
    return this.isLineChanging(lineNumber) ? 'line-changing' : 'line-static';
  }

  toggleLanguage(): void {
    this.currentLanguage = this.currentLanguage === 'en' ? 'ru' : 'en';
    if (this.hexagram) {
      this.loadHexagram(this.hexagram.id);
    }
  }

  goBack(): void {
    this.location.back();
  }

  getLineSymbol(type: 'yin' | 'yang'): string {
    return type === 'yang' ? '━━━━━━' : '━━  ━━';
  }
}
