export interface Consultation {
  id: number;
  userId: number;
  question?: string;
  primaryHexagramId: number;
  relatingHexagramId?: number;
  changingLines: number[];
  createdAt: Date;
  notes?: string;
  primaryHexagram?: any;
  relatingHexagram?: any;
}

export interface CoinTossResult {
  tossNumber: number;
  coins: CoinResult[];
  lineValue: number;
  lineType: 'yang' | 'yin';
  isChanging: boolean;
}

export interface CoinResult {
  value: 'heads' | 'tails';
}

export interface CreateConsultationRequest {
  question?: string;
  tossResults: number[];
}

export interface ConsultationResult {
  consultation: Consultation;
  primaryHexagram: {
    id: number;
    number: number;
    chineseName: string;
    englishName: string;
  };
  relatingHexagram?: {
    id: number;
    number: number;
    chineseName: string;
    englishName: string;
  };
  changingLines: number[];
}
