namespace PasTech.AIOffice.Application.Interfaces;

public interface ILlmProvider
{
    string Name { get; }       // "Claude" | "Gemini"
    string ModelId { get; }    // "claude-sonnet-4-6" | "gemini-1.5-pro"
    Task<LlmResult> CompleteAsync(string system, string user, CancellationToken ct = default);
}

public record LlmResult(string Text, int Tokens, bool Success, string? Error = null);
