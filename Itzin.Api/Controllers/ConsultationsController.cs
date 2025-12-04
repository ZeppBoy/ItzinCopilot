using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Itzin.Api.DTOs;
using Itzin.Core.Entities;
using Itzin.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Itzin.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ConsultationsController : ControllerBase
{
    private readonly IConsultationService _consultationService;
    private readonly ILogger<ConsultationsController> _logger;

    public ConsultationsController(
        IConsultationService consultationService,
        ILogger<ConsultationsController> logger)
    {
        _consultationService = consultationService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<ConsultationResponseDto>> Create([FromBody] CreateConsultationDto request)
    {
        var userId = GetUserId();
        var language = request.Language ?? "en";

        var consultation = await _consultationService.CreateConsultationWithTossesAsync(
            userId, 
            request.Question ?? string.Empty, 
            request.TossResults, 
            language);

        _logger.LogInformation("Consultation created for user {UserId}", userId);

        var dto = MapToResponseDto(consultation, language);
        return CreatedAtAction(nameof(GetById), new { id = consultation.Id }, dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ConsultationResponseDto>> GetById(int id, [FromQuery] string? language = "en")
    {
        var userId = GetUserId();
        var consultation = await _consultationService.GetConsultationByIdAsync(id, userId);

        if (consultation == null)
            return NotFound(new { message = "Consultation not found" });

        var dto = MapToResponseDto(consultation, language ?? "en");
        return Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult<List<ConsultationListDto>>> GetMy(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 50,
        [FromQuery] string? language = "en")
    {
        var userId = GetUserId();
        var consultations = await _consultationService.GetUserConsultationsAsync(userId, skip, take);

        var dtos = consultations.Select(c => MapToListDto(c, language ?? "en")).ToList();
        return Ok(dtos);
    }

    [HttpPatch("{id}/notes")]
    public async Task<ActionResult> UpdateNotes(int id, [FromBody] UpdateConsultationNotesDto request)
    {
        var userId = GetUserId();

        try
        {
            await _consultationService.UpdateConsultationNotesAsync(id, userId, request.Notes ?? string.Empty);
            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return NotFound(new { message = "Consultation not found" });
        }
    }

    [HttpPost("toss")]
    public ActionResult<CoinTossResponseDto> TossCoins([FromQuery] int tossNumber = 1)
    {
        var random = new Random();
        var coins = new List<CoinDto>();

        for (int i = 0; i < 3; i++)
        {
            coins.Add(new CoinDto
            {
                Value = random.Next(2) == 0 ? "heads" : "tails"
            });
        }

        // Calculate line value: heads=3, tails=2
        var lineValue = coins.Sum(c => c.Value == "heads" ? 3 : 2);

        // Determine line type and if it's changing
        // 6 = old yin (changing), 7 = young yang, 8 = young yin, 9 = old yang (changing)
        string lineType;
        bool isChanging;

        switch (lineValue)
        {
            case 6:
                lineType = "yin";
                isChanging = true;
                break;
            case 7:
                lineType = "yang";
                isChanging = false;
                break;
            case 8:
                lineType = "yin";
                isChanging = false;
                break;
            case 9:
                lineType = "yang";
                isChanging = true;
                break;
            default:
                lineType = "yang";
                isChanging = false;
                break;
        }

        return Ok(new CoinTossResponseDto
        {
            TossNumber = tossNumber,
            Coins = coins,
            LineValue = lineValue,
            LineType = lineType,
            IsChanging = isChanging
        });
    }

    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value 
                         ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? User.FindFirst("sub")?.Value;
        
        if (userIdClaim == null)
            throw new UnauthorizedAccessException("User ID not found in token");

        return int.Parse(userIdClaim);
    }

    private ConsultationResponseDto MapToResponseDto(Consultation consultation, string language)
    {
        var isRussian = language.ToLower() == "ru";
        var tossValues = consultation.TossResults.Split(',').Select(int.Parse).ToList();
        var changingLines = consultation.ChangingLines?.Split(',').Select(int.Parse).ToList();

        return new ConsultationResponseDto
        {
            Id = consultation.Id,
            Question = consultation.Question,
            ConsultationDate = consultation.ConsultationDate,
            PrimaryHexagram = MapHexagramToDto(consultation.PrimaryHexagram, isRussian),
            RelatingHexagram = consultation.RelatingHexagram != null 
                ? MapHexagramToDto(consultation.RelatingHexagram, isRussian) 
                : null,
            ChangingLines = changingLines,
            TossValues = tossValues,
            Notes = consultation.Notes
        };
    }

    private ConsultationListDto MapToListDto(Consultation consultation, string language)
    {
        return new ConsultationListDto
        {
            Id = consultation.Id,
            Question = consultation.Question,
            ConsultationDate = consultation.ConsultationDate,
            PrimaryHexagram = new HexagramListDto
            {
                Id = consultation.PrimaryHexagram.Id,
                Number = consultation.PrimaryHexagram.Number,
                ChineseName = consultation.PrimaryHexagram.ChineseName,
                Pinyin = consultation.PrimaryHexagram.Pinyin,
                EnglishName = consultation.PrimaryHexagram.EnglishName,
                RussianName = consultation.PrimaryHexagram.RussianName,
                Unicode = consultation.PrimaryHexagram.Unicode
            },
            HasChangingLines = !string.IsNullOrEmpty(consultation.ChangingLines)
        };
    }

    private HexagramDto MapHexagramToDto(Hexagram hexagram, bool isRussian)
    {
        return new HexagramDto
        {
            Id = hexagram.Id,
            Number = hexagram.Number,
            ChineseName = hexagram.ChineseName,
            Pinyin = hexagram.Pinyin,
            EnglishName = hexagram.EnglishName,
            RussianName = hexagram.RussianName,
            Unicode = hexagram.Unicode,
            Judgment = isRussian ? hexagram.JudgmentRu : hexagram.JudgmentEn,
            Image = isRussian ? hexagram.ImageRu : hexagram.ImageEn,
            Lines = new List<string>
            {
                isRussian ? hexagram.Line1Ru : hexagram.Line1En,
                isRussian ? hexagram.Line2Ru : hexagram.Line2En,
                isRussian ? hexagram.Line3Ru : hexagram.Line3En,
                isRussian ? hexagram.Line4Ru : hexagram.Line4En,
                isRussian ? hexagram.Line5Ru : hexagram.Line5En,
                isRussian ? hexagram.Line6Ru : hexagram.Line6En
            }
        };
    }
}
