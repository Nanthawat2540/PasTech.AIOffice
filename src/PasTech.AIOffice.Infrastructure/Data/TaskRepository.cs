using Microsoft.EntityFrameworkCore;
using PasTech.AIOffice.Application.Interfaces;
using PasTech.AIOffice.Domain.Entities;

namespace PasTech.AIOffice.Infrastructure.Data;

public class TaskRepository(AiOfficeDbContext db) : ITaskRepository
{
    public Task<List<OfficeTask>> GetAllAsync() =>
        db.Tasks.Include(t => t.Logs).OrderByDescending(t => t.CreatedAt).ToListAsync();

    public Task<OfficeTask?> GetByIdAsync(int id) =>
        db.Tasks.Include(t => t.Logs).FirstOrDefaultAsync(t => t.Id == id);

    public async Task<OfficeTask> CreateAsync(OfficeTask task)
    {
        db.Tasks.Add(task);
        await db.SaveChangesAsync();
        return task;
    }

    public async Task UpdateAsync(OfficeTask task)
    {
        task.UpdatedAt = DateTime.UtcNow;
        db.Tasks.Update(task);
        await db.SaveChangesAsync();
    }

    public async Task AddLogAsync(AgentLog log)
    {
        db.AgentLogs.Add(log);
        await db.SaveChangesAsync();
    }

    public async Task<string> NextTaskNumberAsync()
    {
        var count = await db.Tasks.CountAsync();
        return $"TASK-{(count + 1):D3}";
    }
}
