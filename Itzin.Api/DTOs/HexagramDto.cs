namespace Itzin.Api.DTOs;

public class HexagramDto
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string ChineseName { get; set; } = string.Empty;
    public string Pinyin { get; set; } = string.Empty;
    public string EnglishName { get; set; } = string.Empty;
    public string RussianName { get; set; } = string.Empty;
    public string Unicode { get; set; } = string.Empty;
    public string Judgment { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public List<string> Lines { get; set; } = new();
}

public class HexagramListDto
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string ChineseName { get; set; } = string.Empty;
    public string Pinyin { get; set; } = string.Empty;
    public string EnglishName { get; set; } = string.Empty;
    public string RussianName { get; set; } = string.Empty;
    public string Unicode { get; set; } = string.Empty;
}
