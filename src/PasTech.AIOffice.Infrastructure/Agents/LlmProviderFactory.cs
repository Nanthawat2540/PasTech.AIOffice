using PasTech.AIOffice.Application.Interfaces;

namespace PasTech.AIOffice.Infrastructure.Agents;

public class LlmProviderFactory(ClaudeProvider claude, GeminiProvider gemini)
{
    public ILlmProvider Get(string provider) => provider.ToLower() switch
    {
        "gemini" => gemini,
        _        => claude   // default = Claude
    };

    public ILlmProvider Claude => claude;
    public ILlmProvider Gemini => gemini;
}
