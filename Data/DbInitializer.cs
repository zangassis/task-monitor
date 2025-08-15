using Microsoft.EntityFrameworkCore;
using TaskMonitor.Data;
using TaskMonitor.Models;

namespace FinanceTracker.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext dbContext)
    {
        await dbContext.Database.MigrateAsync();

        if (!await dbContext.ProcessHistory.AnyAsync())
        {
            var now = DateTime.UtcNow;

            var sampleEntries = Enumerable.Range(1, 100)
                .Select(i => new ProcessHistory
                {
                    CreatedAt = now.AddDays(-i),
                    ProcessName = $"Sample Process: #{i}",
                    Status = "Completed",
                    Details = $"Process details: #{i}",
                    CreatedBy = "admin"
                })
                .ToList();

            await dbContext.ProcessHistory.AddRangeAsync(sampleEntries);
            await dbContext.SaveChangesAsync();
        }
    }
}
