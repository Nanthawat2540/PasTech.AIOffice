using PasTech.AIOffice.Domain.Entities;
using PasTech.AIOffice.Domain.Enums;

namespace PasTech.AIOffice.Application.Interfaces;

public interface IAgentService
{
    AgentType AgentType { get; }
    Task<AgentResult> RunAsync(OfficeTask task, string input, string provider = "claude", CancellationToken ct = default);
}

public record AgentResult(string Output, int TokensUsed, bool Success, string Provider = "claude", string? Error = null);
