using Microsoft.EntityFrameworkCore;
using PasTech.AIOffice.Domain.Entities;

namespace PasTech.AIOffice.Infrastructure.Data;

public class AiOfficeDbContext(DbContextOptions<AiOfficeDbContext> options) : DbContext(options)
{
    public DbSet<OfficeTask> Tasks { get; set; }
    public DbSet<AgentLog> AgentLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<OfficeTask>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.TaskNumber).HasMaxLength(20);
            e.Property(x => x.CustomerName).HasMaxLength(200);
            e.Property(x => x.CustomerEmail).HasMaxLength(200);
            e.Property(x => x.QuotationAmount).HasPrecision(18, 2);
            e.HasMany(x => x.Logs).WithOne(x => x.OfficeTask).HasForeignKey(x => x.OfficeTaskId);
        });

        b.Entity<AgentLog>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Output).HasMaxLength(8000);
            e.Property(x => x.Input).HasMaxLength(4000);
        });
    }
}
