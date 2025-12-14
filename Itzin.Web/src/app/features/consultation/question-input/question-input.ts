import { Component, inject, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ConsultationService } from '../../../core/services/consultation.service';

@Component({
  selector: 'app-question-input',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './question-input.html',
  styleUrl: './question-input.scss',
})
export class QuestionInput {
  private fb = inject(FormBuilder);
  private consultationService = inject(ConsultationService);

  @Output() questionSubmitted = new EventEmitter<string>();

  questionForm: FormGroup;
  maxLength = 500;

  constructor() {
    this.questionForm = this.fb.group({
      question: [''],
      isAdvanced: [false]
    });
  }

  onSubmit(): void {
    const question = this.questionForm.value.question?.trim() || '';
    const isAdvanced = this.questionForm.value.isAdvanced || false;
    
    this.consultationService.setQuestion(question);
    this.consultationService.setIsAdvanced(isAdvanced);
    
    this.questionSubmitted.emit(question);
  }

  skip(): void {
    this.consultationService.setQuestion('');
    this.consultationService.setIsAdvanced(false);
    this.questionSubmitted.emit('');
  }

  get remainingChars(): number {
    const length = this.questionForm.value.question?.length || 0;
    return this.maxLength - length;
  }
}
