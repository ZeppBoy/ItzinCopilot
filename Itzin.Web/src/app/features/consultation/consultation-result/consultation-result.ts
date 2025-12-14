import { Component, inject, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { ConsultationService } from '../../../core/services/consultation.service';
import { Consultation } from '../../../core/models/consultation.model';

interface HexagramLine {
  type: 'yang' | 'yin';
  isChanging: boolean;
  lineNumber: number;
}

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
  @Output() newConsultationRequested = new EventEmitter<void>();

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

      // If line is changing, flip it
      const originalType = this.isYangLine(value) ? 'yang' : 'yin';
      const type = isChanging ? (originalType === 'yang' ? 'yin' : 'yang') : originalType;

      return {
        type,
        isChanging: false, // Relating hexagram doesn't show changing lines
        lineNumber
      };
    });
  }

  private isYangLine(value: number): boolean {
    // 7 = young yang, 9 = old yang (changing)
    return value === 7 || value === 9;
  }

  private isChangingLine(value: number): boolean {
    // 6 = old yin (changing), 9 = old yang (changing)
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
    // Clear all consultation data including saved consultation
    this.consultationService.clearTossResults();
    // Emit event to parent to reset the flow
    this.newConsultationRequested.emit();
  }

  goToDashboard(): void {
    this.router.navigate(['/dashboard']);
  }
}
