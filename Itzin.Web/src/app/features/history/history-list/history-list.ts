import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ConsultationService } from '../../../core/services/consultation.service';
import { Consultation } from '../../../core/models/consultation.model';

@Component({
  selector: 'app-history-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './history-list.html',
  styleUrl: './history-list.scss',
})
export class HistoryList implements OnInit {
  private consultationService = inject(ConsultationService);
  
  consultations: Consultation[] = [];
  loading = false;
  error: string | null = null;

  ngOnInit(): void {
    this.loadConsultations();
  }

  loadConsultations(): void {
    this.loading = true;
    this.error = null;
    
    this.consultationService.getUserConsultations().subscribe({
      next: (consultations) => {
        this.consultations = consultations;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load consultations';
        this.loading = false;
        console.error('Error loading consultations:', err);
      }
    });
  }

  formatDate(date: string | Date): string {
    const d = new Date(date);
    return d.toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  }

  deleteConsultation(id: number, event: Event): void {
    event.stopPropagation();
    
    if (!confirm('Are you sure you want to delete this consultation? This action cannot be undone.')) {
      return;
    }

    this.consultationService.deleteConsultation(id).subscribe({
      next: () => {
        this.consultations = this.consultations.filter(c => c.id !== id);
      },
      error: (err) => {
        this.error = 'Failed to delete consultation';
        console.error('Error deleting consultation:', err);
      }
    });
  }
}
