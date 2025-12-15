import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ConsultationService } from '../../../core/services/consultation.service';
import { Consultation } from '../../../core/models/consultation.model';

interface HexagramLine {
  type: 'yang' | 'yin';
  isChanging: boolean;
  lineNumber: number;
}

@Component({
  selector: 'app-history-detail',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './history-detail.html',
  styleUrl: './history-detail.scss',
})
export class HistoryDetail implements OnInit {
  private consultationService = inject(ConsultationService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  consultation: Consultation | null = null;
  loading = false;
  error: string | null = null;

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.loadConsultation(parseInt(id, 10));
    }
  }

  loadConsultation(id: number): void {
    this.loading = true;
    this.error = null;

    this.consultationService.getConsultationById(id).subscribe({
      next: (consultation) => {
        this.consultation = consultation;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load consultation';
        this.loading = false;
        console.error('Error loading consultation:', err);
      }
    });
  }

  getPrimaryHexagramLines(): HexagramLine[] {
    if (!this.consultation?.tossValues) return [];

    return this.consultation.tossValues.map((value, index) => ({
      type: this.isYangLine(value) ? 'yang' : 'yin',
      isChanging: this.isChangingLine(value),
      lineNumber: index + 1
    }));
  }

  getRelatingHexagramLines(): HexagramLine[] {
    if (!this.consultation?.tossValues || !this.consultation?.changingLines) return [];

    return this.consultation.tossValues.map((value, index) => {
      const lineNumber = index + 1;
      const isChanging = this.consultation!.changingLines!.includes(lineNumber);

      const originalType = this.isYangLine(value) ? 'yang' : 'yin';
      const type = isChanging ? (originalType === 'yang' ? 'yin' : 'yang') : originalType;

      return {
        type,
        isChanging: false,
        lineNumber
      };
    });
  }

  private isYangLine(value: number): boolean {
    return value === 7 || value === 9;
  }

  private isChangingLine(value: number): boolean {
    return value === 6 || value === 9;
  }

  getLineImagePath(line: HexagramLine): string {
    if (line.type === 'yang') {
      return line.isChanging ? '/assets/images/oldYang.png' : '/assets/images/NewYang.png';
    } else {
      return line.isChanging ? '/assets/images/oldYin.png' : '/assets/images/newYin.png';
    }
  }

  viewHexagram(id: number): void {
    // Pass changing lines if we're viewing the primary hexagram
    const queryParams: any = {};
    if (this.consultation && this.consultation.changingLines && this.consultation.changingLines.length > 0) {
      if (id === this.consultation.primaryHexagram.id) {
        queryParams.changingLines = this.consultation.changingLines.join(',');
      }
    }
    
    this.router.navigate(['/hexagrams', id], { queryParams });
  }

  onViewButtonClick(event: Event, id: number): void {
    event.stopPropagation();
    event.preventDefault();
    this.viewHexagram(id);
  }

  goBack(): void {
    this.router.navigate(['/history']);
  }

  formatDate(date: string | Date): string {
    const d = new Date(date);
    return d.toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  }
}
