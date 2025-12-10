import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ConsultationService } from '../../../core/services/consultation.service';
import { QuestionInput } from '../question-input/question-input';
import { CoinToss } from '../coin-toss/coin-toss';
import { ConsultationResult } from '../consultation-result/consultation-result';
import { Consultation } from '../../../core/models/consultation.model';

@Component({
  selector: 'app-consultation-flow',
  standalone: true,
  imports: [CommonModule, QuestionInput, CoinToss, ConsultationResult],
  templateUrl: './consultation-flow.html',
  styleUrl: './consultation-flow.scss',
})
export class ConsultationFlow implements OnInit {
  private consultationService = inject(ConsultationService);
  private router = inject(Router);

  currentStep: 'question' | 'toss' | 'result' = 'question';
  consultation: Consultation | null = null;
  isCreating = false;
  errorMessage = '';

  ngOnInit(): void {
    // Check if there's a saved consultation to restore
    const savedConsultation = this.consultationService.getCurrentConsultation();
    if (savedConsultation) {
      this.consultation = savedConsultation;
      this.currentStep = 'result';
    }
  }

  onQuestionSubmitted(question: string): void {
    this.currentStep = 'toss';
  }

  onAllTossesComplete(): void {
    this.createConsultation();
  }

  createConsultation(): void {
    this.isCreating = true;
    this.errorMessage = '';

    const tossResults = this.consultationService.getTossResults();
    const tossValues = tossResults.map(r => r.lineValue);

    let question: string | undefined;
    this.consultationService.currentQuestion$.subscribe(q => question = q || undefined).unsubscribe();

    this.consultationService.createConsultation({
      question,
      tossResults: tossValues,
      language: 'en'
    }).subscribe({
      next: (consultation) => {
        this.consultation = consultation;
        this.currentStep = 'result';
        this.isCreating = false;
        // Save consultation to service so it can be restored
        this.consultationService.setCurrentConsultation(consultation);
      },
      error: (error) => {
        this.errorMessage = 'Failed to create consultation. Please try again.';
        this.isCreating = false;
        console.error('Error creating consultation:', error);
      }
    });
  }

  goBack(): void {
    if (this.currentStep === 'toss') {
      this.currentStep = 'question';
      this.consultationService.clearTossResults();
    } else if (this.currentStep === 'question') {
      this.router.navigate(['/dashboard']);
    }
  }
}
