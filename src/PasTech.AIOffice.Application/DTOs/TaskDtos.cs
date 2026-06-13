using PasTech.AIOffice.Domain.Enums;

namespace PasTech.AIOffice.Application.DTOs;

public record CreateTaskRequest(
    string CustomerName,
    string CustomerEmail,
    string CustomerPhone,
    string Location,
    string Description,
    JobType JobType
);

public record RunAgentRequest(int TaskId, AgentType Agent, string Input);

public record TaskSummaryDto(
    int Id,
    string TaskNumber,
    string CustomerName,
    JobType JobType,
    OfficeTaskStatus Status,
    decimal? QuotationAmount,
    DateTime CreatedAt
);

public record AgentLogDto(
    AgentType Agent,
    string Action,
    string Output,
    DateTime CreatedAt
);
