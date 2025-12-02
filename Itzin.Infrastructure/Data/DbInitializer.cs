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

            // Check if hexagrams already exist
            if (await context.Hexagrams.AnyAsync())
            {
                logger.LogInformation("Database already seeded with hexagrams");
                return;
            }

            // Seed hexagrams
            logger.LogInformation("Seeding all 64 hexagrams...");
            var hexagrams = CompleteHexagramSeed.GetAllHexagrams();
            await context.Hexagrams.AddRangeAsync(hexagrams);
            await context.SaveChangesAsync();
            
            logger.LogInformation("Successfully seeded {Count} hexagrams", hexagrams.Count);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }
}
