using Itzin.Api.DTOs;
using Itzin.Core.Entities;
using Itzin.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Itzin.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HexagramsController : ControllerBase
{
    private readonly IHexagramService _hexagramService;
    private readonly IHexagramRuDescriptionRepository _ruDescriptionRepository;
    private readonly ILogger<HexagramsController> _logger;

    public HexagramsController(
        IHexagramService hexagramService,
        IHexagramRuDescriptionRepository ruDescriptionRepository,
        ILogger<HexagramsController> logger)
    {
        _hexagramService = hexagramService;
        _ruDescriptionRepository = ruDescriptionRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<HexagramListDto>>> GetAll([FromQuery] string? language = "en")
    {
        var hexagrams = await _hexagramService.GetAllHexagramsAsync();
        
        var dtos = hexagrams.Select(h => new HexagramListDto
        {
            Id = h.Id,
            Number = h.Number,
            ChineseName = h.ChineseName,
            Pinyin = h.Pinyin,
            EnglishName = h.EnglishName,
            RussianName = h.RussianName,
            Unicode = h.Unicode
        }).ToList();

        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HexagramDto>> GetById(int id, [FromQuery] string? language = "en")
    {
        var hexagram = await _hexagramService.GetHexagramByIdAsync(id);
        
        if (hexagram == null)
            return NotFound(new { message = "Hexagram not found" });

        var dto = MapToDto(hexagram, language ?? "en");
        return Ok(dto);
    }

    [HttpGet("number/{number}")]
    public async Task<ActionResult<HexagramDto>> GetByNumber(int number, [FromQuery] string? language = "en")
    {
        var hexagram = await _hexagramService.GetHexagramByNumberAsync(number);
        
        if (hexagram == null)
            return NotFound(new { message = "Hexagram not found" });

        var dto = MapToDto(hexagram, language ?? "en");
        return Ok(dto);
    }

    [HttpGet("ru-descriptions")]
    public async Task<ActionResult> GetRuDescriptions()
    {
        var descriptions = await _ruDescriptionRepository.GetAllAsync();
        return Ok(new { 
            count = descriptions.Count,
            data = descriptions.Select(d => new { 
                d.Id, 
                d.HexagramId, 
                d.Name, 
                d.Symbol,
                ShortPreview = d.Short.Length > 50 ? d.Short.Substring(0, 50) + "..." : d.Short
            })
        });
    }

    [HttpGet("ru-descriptions/{hexagramId}")]
    public async Task<ActionResult> GetRuDescriptionByHexagramId(int hexagramId)
    {
        var description = await _ruDescriptionRepository.GetByHexagramIdAsync(hexagramId);
        
        if (description == null)
            return NotFound(new { message = "Russian description not found for this hexagram" });

        return Ok(description);
    }

    private HexagramDto MapToDto(Hexagram hexagram, string language)
    {
        var isRussian = language.ToLower() == "ru";
        
        var dto = new HexagramDto
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

        // Include Russian description if available and language is Russian
        if (isRussian && hexagram.RuDescription != null)
        {
            dto.RuDescription = new HexagramRuDescriptionDto
            {
                Short = hexagram.RuDescription.Short,
                Name = hexagram.RuDescription.Name,
                ImageRow = hexagram.RuDescription.ImageRow,
                Description = hexagram.RuDescription.Description,
                InnerOuterWorlds = hexagram.RuDescription.InnerOuterWorlds,
                Definition = hexagram.RuDescription.Definition,
                Symbol = hexagram.RuDescription.Symbol,
                Line1 = hexagram.RuDescription.Line1,
                Line2 = hexagram.RuDescription.Line2,
                Line3 = hexagram.RuDescription.Line3,
                Line4 = hexagram.RuDescription.Line4,
                Line5 = hexagram.RuDescription.Line5,
                Line6 = hexagram.RuDescription.Line6,
                LineBonus = hexagram.RuDescription.LineBonus
            };
        }

        return dto;
    }
}
