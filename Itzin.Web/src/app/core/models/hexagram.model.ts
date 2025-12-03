export interface Hexagram {
  id: number;
  number: number;
  chineseName: string;
  pinyin: string;
  englishName: string;
  russianName: string;
  trigrams: {
    upper: string;
    lower: string;
  };
  judgment: {
    chinese?: string;
    english: string;
    russian: string;
  };
  image: {
    chinese?: string;
    english: string;
    russian: string;
  };
  lines: HexagramLine[];
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
}
