import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
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

  hexagram: Hexagram | null = null;
  isLoading: boolean = true;
  errorMessage: string = '';
  currentLanguage: 'en' | 'ru' = 'en';

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
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
      },
      error: (error) => {
        this.errorMessage = 'Failed to load hexagram. Please try again.';
        this.isLoading = false;
        console.error('Error loading hexagram:', error);
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/hexagrams']);
  }

  getLineSymbol(type: 'yin' | 'yang'): string {
    return type === 'yang' ? '━━━━━━' : '━━  ━━';
  }
}
