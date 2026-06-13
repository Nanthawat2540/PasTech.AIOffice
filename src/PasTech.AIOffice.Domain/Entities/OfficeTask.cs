using PasTech.AIOffice.Domain.Enums;

namespace PasTech.AIOffice.Domain.Entities;

public class OfficeTask
{
    public int Id { get; set; }
    public string TaskNumber { get; set; } = "";       // TASK-001
    public string CustomerName { get; set; } = "";
    public string CustomerEmail { get; set; } = "";
    public string CustomerPhone { get; set; } = "";
    public string Location { get; set; } = "";
    public string Description { get; set; } = "";      // ข้อความ inquiry เดิม
    public JobType JobType { get; set; }
    public OfficeTaskStatus Status { get; set; } = OfficeTaskStatus.Inquiry;
    public decimal? QuotationAmount { get; set; }
    public string? QuotationNumber { get; set; }
    public int? ErpCustomerId { get; set; }            // FK ไป ERP
    public int? ErpQuotationId { get; set; }
    public int? ErpProjectId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public List<AgentLog> Logs { get; set; } = [];
}
