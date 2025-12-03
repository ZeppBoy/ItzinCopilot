import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { HexagramService } from '../../../core/services/hexagram.service';
import { HexagramListItem } from '../../../core/models/hexagram.model';

@Component({
  selector: 'app-hexagram-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './hexagram-list.html',
  styleUrl: './hexagram-list.scss',
})
export class HexagramList implements OnInit {
  private hexagramService = inject(HexagramService);
  private router = inject(Router);

  hexagrams: HexagramListItem[] = [];
  isLoading: boolean = true;
  errorMessage: string = '';
  currentLanguage: 'en' | 'ru' = 'en';

  ngOnInit(): void {
    this.loadHexagrams();
  }

  loadHexagrams(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.hexagramService.getAll(this.currentLanguage).subscribe({
      next: (hexagrams) => {
        this.hexagrams = hexagrams;
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = 'Failed to load hexagrams. Please try again.';
        this.isLoading = false;
        console.error('Error loading hexagrams:', error);
      }
    });
  }

  viewHexagram(id: number): void {
    this.router.navigate(['/hexagrams', id]);
  }

  goBack(): void {
    this.router.navigate(['/dashboard']);
  }

  getHexagramSymbol(number: number): string {
    // Map hexagram numbers to Unicode symbols
    // For now, return a placeholder - we'll enhance this later
    return '☰☷';
  }
}
