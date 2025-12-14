namespace Itzin.Core.Entities;

public class Consultation
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Question { get; set; } = string.Empty;
    public string? QuestionLanguage { get; set; }
    public string TossResults { get; set; } = string.Empty;
    public int PrimaryHexagramId { get; set; }
    public int? RelatingHexagramId { get; set; }
    public string? ChangingLines { get; set; }
    
    // Advanced Consultation Properties
    public bool IsAdvanced { get; set; } = false;
    public int? AntiHexagramId { get; set; }
    public int? ChangingHexagramId { get; set; }
    public string? AdditionalChangingHexagrams { get; set; } // Comma-separated list of hexagram IDs
    
    public string? Notes { get; set; }
    public DateTime ConsultationDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public User User { get; set; } = null!;
    public Hexagram PrimaryHexagram { get; set; } = null!;
    public Hexagram? RelatingHexagram { get; set; }
    public Hexagram? AntiHexagram { get; set; }
    public Hexagram? ChangingHexagram { get; set; }
}
