using PasTech.AIOffice.Domain.Enums;

namespace PasTech.AIOffice.Domain.Entities;

public class AgentLog
{
    public int Id { get; set; }
    public int OfficeTaskId { get; set; }
    public OfficeTask OfficeTask { get; set; } = null!;
    public AgentType Agent { get; set; }
    public string Action { get; set; } = "";
    public string Input { get; set; } = "";
    public string Output { get; set; } = "";
    public int TokensUsed { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
