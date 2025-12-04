namespace Itzin.Core.Interfaces;

public interface ICoinTossService
{
    int TossCoin();
    List<int> TossCoins(int count = 6);
    int CalculateTossValue(List<bool> coins);
    List<string> GenerateThreeCoins();
}
