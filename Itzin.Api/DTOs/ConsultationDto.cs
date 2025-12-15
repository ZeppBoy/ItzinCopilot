using System.ComponentModel.DataAnnotations;

namespace Itzin.Api.DTOs;

public class CreateConsultationDto
{
    [MaxLength(500)]
    public string? Question { get; set; }

    [MaxLength(10)]
    public string Language { get; set; } = "en";

    [Required]
    [MinLength(6)]
    [MaxLength(6)]
    public List<int> TossResults { get; set; } = new();

    public bool IsAdvanced { get; set; } = false;
}

public class ConsultationResponseDto
{
    public int Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public DateTime ConsultationDate { get; set; }
    public HexagramDto PrimaryHexagram { get; set; } = null!;
    public HexagramDto? RelatingHexagram { get; set; }
    public List<int>? ChangingLines { get; set; }
    public List<int> TossValues { get; set; } = new();
    public string? Notes { get; set; }

    // Advanced Consultation Properties
    public bool IsAdvanced { get; set; }
    public HexagramDto? AntiHexagram { get; set; }
    public HexagramDto? ChangingHexagram { get; set; }
    public List<int>? AdditionalChangingHexagrams { get; set; }
    public List<HexagramDto>? AdditionalChangingHexagramsInfo { get; set; }
}

public class ConsultationListDto
{
    public int Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public DateTime ConsultationDate { get; set; }
    public HexagramListDto PrimaryHexagram { get; set; } = null!;
    public bool HasChangingLines { get; set; }
    public bool IsAdvanced { get; set; }
}

public class UpdateConsultationNotesDto
{
    [MaxLength(2000)]
    public string? Notes { get; set; }
}
