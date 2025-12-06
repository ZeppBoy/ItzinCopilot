import { Component, inject, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { ConsultationService } from '../../../core/services/consultation.service';
import { Consultation } from '../../../core/models/consultation.model';

@Component({
  selector: 'app-consultation-result',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './consultation-result.html',
  styleUrl: './consultation-result.scss',
})
export class ConsultationResult {
  private consultationService = inject(ConsultationService);
  private router = inject(Router);

  @Input() consultation: Consultation | null = null;

  viewHexagram(id: number): void {
    console.log('viewHexagram called with id:', id);
    this.router.navigate(['/hexagrams', id]);
  }

  onViewButtonClick(event: Event, id: number): void {
    console.log('Button clicked! Event:', event, 'ID:', id);
    event.stopPropagation();
    event.preventDefault();
    this.viewHexagram(id);
  }

  newConsultation(): void {
    this.consultationService.clearTossResults();
    this.router.navigate(['/consultation']);
  }

  goToDashboard(): void {
    this.router.navigate(['/dashboard']);
  }
}
