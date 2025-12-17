export interface Hexagram {
  id: number;
  number: number;
  chineseName: string;
  pinyin: string;
  englishName: string;
  russianName: string;
  unicode?: string;
  judgment?: string;
  image?: string;
  lines?: string[];
  trigrams?: {
    upper: string;
    lower: string;
  };
  ruDescription?: HexagramRuDescription;
}

export interface HexagramRuDescription {
  short: string;
  name: string;
  imageRow: string;
  description: string;
  innerOuterWorlds: string;
  hiddenOpportunity: string;
  subsequence: string;
  definition: string;
  symbol: string;
  line1: string;
  line2: string;
  line3: string;
  line4: string;
  line5: string;
  line6: string;
  lineBonus: string;
}

export interface HexagramLine {
  position: number;
  type: 'yin' | 'yang';
  interpretation: {
    english: string;
    russian: string;
  };
}

export interface HexagramListItem {
  id: number;
  number: number;
  chineseName: string;
  englishName: string;
  russianName: string;
  unicode?: string;
}
