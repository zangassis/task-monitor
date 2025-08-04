using Microsoft.EntityFrameworkCore;
using TaskMonitor.Data;
using Quartz;
using TaskMonitor.Jobs;

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
        .WithCronSchedule("0 0 3 * * ?")); // Daily at 03:00 UTC
});

builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);

var app = builder.Build();

app.Run();
