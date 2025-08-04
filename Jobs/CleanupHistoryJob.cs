using Microsoft.EntityFrameworkCore;
using Quartz;
using TaskMonitor.Data;
using TaskMonitor.Models;

namespace TaskMonitor.Jobs;
public class CleanupHistoryJob : IJob
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<CleanupHistoryJob> _logger;

    public CleanupHistoryJob(AppDbContext dbContext, ILogger<CleanupHistoryJob> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-30);

        var oldEntries = await _dbContext.ProcessHistory
            .Where(h => h.CreatedAt < cutoffDate)
            .ToListAsync();

        if (oldEntries.Count == 0)
        {
            _logger.LogInformation("No old history entries found for cleanup.");
            return;
        }

        await RemoveHistory(oldEntries);

        var notification = new Notification
        {
            Message = $"{oldEntries.Count} history entries cleaned up at {DateTime.UtcNow:O}",
            CreatedAt = DateTime.UtcNow
        };

        await SaveNotification(notification);
    }

    private async Task SaveNotification(Notification notification)
    {
        _dbContext.Notifications.Add(notification);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Cleanup notification created.");
    }

    private async Task RemoveHistory(List<ProcessHistory> oldEntries)
    {
        _dbContext.ProcessHistory.RemoveRange(oldEntries);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Deleted {Count} old history entries.", oldEntries.Count);
    }
}
