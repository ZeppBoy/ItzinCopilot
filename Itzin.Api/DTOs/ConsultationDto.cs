using System.ComponentModel.DataAnnotations;

namespace Itzin.Api.DTOs;

public class CreateConsultationDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(500)]
    public string Question { get; set; } = string.Empty;

    [MaxLength(10)]
    public string Language { get; set; } = "en";
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
}

public class ConsultationListDto
{
    public int Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public DateTime ConsultationDate { get; set; }
    public HexagramListDto PrimaryHexagram { get; set; } = null!;
    public bool HasChangingLines { get; set; }
}

public class UpdateConsultationNotesDto
{
    [MaxLength(2000)]
    public string? Notes { get; set; }
}
