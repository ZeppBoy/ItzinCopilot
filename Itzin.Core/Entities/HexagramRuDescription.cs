namespace Itzin.Core.Entities;

public class HexagramRuDescription
{
    public int Id { get; set; }
    public int HexagramId { get; set; }
    public string Short { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ImageRow { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string InnerOuterWorlds { get; set; } = string.Empty;
    public string HiddenOpportunity { get; set; } = string.Empty;
    public string Subsequence { get; set; } = string.Empty;
    public string Definition { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public string Line1 { get; set; } = string.Empty;
    public string Line2 { get; set; } = string.Empty;
    public string Line3 { get; set; } = string.Empty;
    public string Line4 { get; set; } = string.Empty;
    public string Line5 { get; set; } = string.Empty;
    public string Line6 { get; set; } = string.Empty;
    public string LineBonus { get; set; } = string.Empty;

    // Navigation property
    public Hexagram Hexagram { get; set; } = null!;
}

