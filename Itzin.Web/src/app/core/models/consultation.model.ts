export interface Consultation {
  id: number;
  question?: string;
  consultationDate: Date;
  primaryHexagram: HexagramInfo;
  relatingHexagram?: HexagramInfo;
  changingLines?: number[];
  tossValues: number[];
  notes?: string;
}

export interface HexagramInfo {
  id: number;
  number: number;
  chineseName: string;
  pinyin: string;
  englishName: string;
  russianName: string;
  unicode: string;
  judgment?: string;
  image?: string;
  lines?: string[];
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
  language?: string;
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
