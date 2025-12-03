import { Component, inject, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConsultationService } from '../../../core/services/consultation.service';
import { CoinTossResult } from '../../../core/models/consultation.model';

@Component({
  selector: 'app-coin-toss',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './coin-toss.html',
  styleUrl: './coin-toss.scss',
})
export class CoinToss {
  private consultationService = inject(ConsultationService);

  @Output() allTossesComplete = new EventEmitter<void>();

  tossResults: CoinTossResult[] = [];
  currentToss: CoinTossResult | null = null;
  isTossing = false;
  showResult = false;

  get totalTosses(): number {
    return 6;
  }

  get remainingTosses(): number {
    return this.totalTosses - this.tossResults.length;
  }

  get progress(): number {
    return (this.tossResults.length / this.totalTosses) * 100;
  }

  performToss(): void {
    if (this.isTossing || this.tossResults.length >= this.totalTosses) {
      return;
    }

    this.isTossing = true;
    this.showResult = false;

    // Animate for 1.5 seconds
    setTimeout(() => {
      const result = this.consultationService.tossCoins();
      this.currentToss = result;
      this.consultationService.addTossResult(result);
      this.tossResults.push(result);
      
      this.isTossing = false;
      this.showResult = true;

      // Auto-proceed to next toss or complete
      if (this.tossResults.length >= this.totalTosses) {
        setTimeout(() => {
          this.allTossesComplete.emit();
        }, 2000);
      }
    }, 1500);
  }

  getLineSymbol(result: CoinTossResult): string {
    if (result.isChanging) {
      return result.lineType === 'yang' ? '━━━━━━ ×' : '━━  ━━ ○';
    }
    return result.lineType === 'yang' ? '━━━━━━' : '━━  ━━';
  }

  getLineDescription(result: CoinTossResult): string {
    if (result.isChanging) {
      return result.lineType === 'yang' ? 'Old Yang (Changing)' : 'Old Yin (Changing)';
    }
    return result.lineType === 'yang' ? 'Young Yang' : 'Young Yin';
  }
}
