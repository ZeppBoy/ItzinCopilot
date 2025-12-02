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
    public string? Notes { get; set; }
    public DateTime ConsultationDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public User User { get; set; } = null!;
    public Hexagram PrimaryHexagram { get; set; } = null!;
    public Hexagram? RelatingHexagram { get; set; }
}
