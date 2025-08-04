using Microsoft.EntityFrameworkCore;
using TaskMonitor.Models;

namespace TaskMonitor.Data; 
public class AppDbContext : DbContext
{
    public DbSet<ProcessHistory> ProcessHistory { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}