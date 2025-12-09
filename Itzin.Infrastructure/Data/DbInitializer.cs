using Itzin.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Itzin.Infrastructure.Data;

public class DbInitializer
{
    public static async Task InitializeAsync(ItzinDbContext context, ILogger logger)
    {
        try
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Seed hexagrams if not already seeded
            if (!await context.Hexagrams.AnyAsync())
            {
                logger.LogInformation("Seeding all 64 hexagrams...");
                var hexagrams = CompleteHexagramSeed.GetAllHexagrams();
                await context.Hexagrams.AddRangeAsync(hexagrams);
                await context.SaveChangesAsync();
                
                logger.LogInformation("Successfully seeded {Count} hexagrams", hexagrams.Count);
            }
            else
            {
                logger.LogInformation("Database already seeded with hexagrams");
            }

            // Seed Russian descriptions if not already seeded
            if (!await context.HexagramRuDescriptions.AnyAsync())
            {
                logger.LogInformation("Seeding hexagram Russian descriptions...");
                var ruDescriptions = HexagramRuDescriptionSeedData.GetAllDescriptions();
                await context.HexagramRuDescriptions.AddRangeAsync(ruDescriptions);
                await context.SaveChangesAsync();
                
                logger.LogInformation("Successfully seeded {Count} Russian descriptions", ruDescriptions.Count);
            }
            else
            {
                logger.LogInformation("Database already seeded with Russian descriptions");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }
}
