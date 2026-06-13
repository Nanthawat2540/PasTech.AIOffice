using PasTech.AIOffice.Domain.Entities;
using PasTech.AIOffice.Domain.Enums;

namespace PasTech.AIOffice.Application.Interfaces;

public interface ITaskRepository
{
    Task<List<OfficeTask>> GetAllAsync();
    Task<OfficeTask?> GetByIdAsync(int id);
    Task<OfficeTask> CreateAsync(OfficeTask task);
    Task UpdateAsync(OfficeTask task);
    Task AddLogAsync(AgentLog log);
    Task<string> NextTaskNumberAsync();
}
