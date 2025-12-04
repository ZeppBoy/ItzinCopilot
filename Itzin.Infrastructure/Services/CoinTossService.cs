using System.Security.Cryptography;
using Itzin.Core.Interfaces;

namespace Itzin.Infrastructure.Services;

public class CoinTossService : ICoinTossService
{
    public int TossCoin()
    {
        // Generate cryptographically secure random value
        var randomBytes = new byte[4];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        var randomValue = BitConverter.ToUInt32(randomBytes, 0);
        
        // Return 0 (tails) or 1 (heads)
        return (int)(randomValue % 2);
    }
    public async Task<int> GetTossCoinResultAsync()
    {
        var t =  await Task.Run(() => {
            Thread.Sleep(1000);
            return RandomNumberGenerator.GetInt32(0, 2);
        });

        return t;
    }

    public List<int> TossCoins(int count = 6)
    {
        var tosses = new List<int>();
        for (int i = 0; i < count; i++)
        {
            tosses.Add(CalculateSingleToss());
        }
        return tosses;
    }

    public int CalculateTossValue(List<bool> coins)
    {
        // Convert booleans to toss value
        // true = heads, false = tails
        int heads = coins.Count(c => c);
        
        return heads switch
        {
            3 => 9, // Three heads = 9 (old yang, changing)
            2 => 8, // Two heads = 8 (young yin, static)
            1 => 7, // One head = 7 (young yang, static)
            0 => 6, // Zero heads = 6 (old yin, changing)
            _ => throw new ArgumentException("Invalid coin toss count")
        };
    }

    private int CalculateSingleToss()
    {
        // Toss three coins
        var coin1 = TossCoin();
        var coin2 = TossCoin();
        var coin3 = TossCoin();
        
        int heads = coin1 + coin2 + coin3;
        
        // Calculate toss value based on traditional I Ching rules:
        // 3 heads = 9 (old yang, changing to yin)
        // 2 heads = 8 (young yin, static)
        // 1 head = 7 (young yang, static)
        // 0 heads = 6 (old yin, changing to yang)
        return heads switch
        {
            3 => 9,
            2 => 8,
            1 => 7,
            0 => 6,
            _ => throw new InvalidOperationException("Invalid toss result")
        };
    }

    public List<string> GenerateThreeCoins()
    {
        var coins = new List<string>();
        for (int i = 0; i < 3; i++)
        {
            //coins.Add(TossCoin() == 0 ? "tails" : "heads");
            coins.Add(Task.Run(async () => await GetTossCoinResultAsync())?.Result == 0 ? "tails" : "heads");
          

           // var t = Task.Run(async () => await GetTossCoinResultAsync()).Result;
        }
        return coins;
    }
    
    
}
