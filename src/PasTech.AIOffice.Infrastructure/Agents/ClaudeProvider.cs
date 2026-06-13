using Anthropic.SDK;
using Anthropic.SDK.Constants;
using Anthropic.SDK.Messaging;
using PasTech.AIOffice.Application.Interfaces;

namespace PasTech.AIOffice.Infrastructure.Agents;

public class ClaudeProvider(AnthropicClient client) : ILlmProvider
{
    public string Name => "Claude";
    public string ModelId => AnthropicModels.Claude35Sonnet;

    public async Task<LlmResult> CompleteAsync(string system, string user, CancellationToken ct = default)
    {
        try
        {
            var response = await client.Messages.GetClaudeMessageAsync(new MessageParameters
            {
                Model = ModelId,
                MaxTokens = 4096,
                Stream = false,
                Temperature = 0.7m,
                Messages = [new Message(RoleType.User, $"{system}\n\n---\n{user}")]
            }, ct);

            var text = response.Message.ToString() ?? "";
            var tokens = (response.Usage?.InputTokens ?? 0) + (response.Usage?.OutputTokens ?? 0);
            return new LlmResult(text, tokens, true);
        }
        catch (Exception ex)
        {
            return new LlmResult("", 0, false, ex.Message);
        }
    }
}
