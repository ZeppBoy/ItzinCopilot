namespace Itzin.Api.DTOs;

public class CoinTossResponseDto
{
    public int TossNumber { get; set; }
    public List<CoinDto> Coins { get; set; } = new();
    public int LineValue { get; set; }
    public string LineType { get; set; } = string.Empty;
    public bool IsChanging { get; set; }
}

public class CoinDto
{
    public string Value { get; set; } = string.Empty; // "heads" or "tails"
}
