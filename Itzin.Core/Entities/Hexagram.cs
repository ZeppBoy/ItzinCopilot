namespace Itzin.Core.Entities;

public class Hexagram
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string ChineseName { get; set; } = string.Empty;
    public string Pinyin { get; set; } = string.Empty;
    public string EnglishName { get; set; } = string.Empty;
    public string RussianName { get; set; } = string.Empty;
    public string Binary { get; set; } = string.Empty;
    public string Unicode { get; set; } = string.Empty;
    public int UpperTrigram { get; set; }
    public int LowerTrigram { get; set; }
    public string JudgmentEn { get; set; } = string.Empty;
    public string JudgmentRu { get; set; } = string.Empty;
    public string ImageEn { get; set; } = string.Empty;
    public string ImageRu { get; set; } = string.Empty;
    public string Line1En { get; set; } = string.Empty;
    public string Line1Ru { get; set; } = string.Empty;
    public string Line2En { get; set; } = string.Empty;
    public string Line2Ru { get; set; } = string.Empty;
    public string Line3En { get; set; } = string.Empty;
    public string Line3Ru { get; set; } = string.Empty;
    public string Line4En { get; set; } = string.Empty;
    public string Line4Ru { get; set; } = string.Empty;
    public string Line5En { get; set; } = string.Empty;
    public string Line5Ru { get; set; } = string.Empty;
    public string Line6En { get; set; } = string.Empty;
    public string Line6Ru { get; set; } = string.Empty;
    
    public ICollection<Consultation> PrimaryConsultations { get; set; } = new List<Consultation>();
    public ICollection<Consultation> RelatingConsultations { get; set; } = new List<Consultation>();
    
    // Navigation property for Russian descriptions
    public HexagramRuDescription? RuDescription { get; set; }
}
