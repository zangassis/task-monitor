using Microsoft.EntityFrameworkCore;
using TaskMonitor.Data;
using Quartz;
using TaskMonitor.Jobs;
using FinanceTracker.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("CleanupHistoryJob");

    q.AddJob<CleanupHistoryJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("CleanupHistoryTrigger")
        .WithCronSchedule("0 3 * * *")); // Daily at 03:00 UTC);
});

builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbInitializer.SeedAsync(dbContext);
}

app.Run();
