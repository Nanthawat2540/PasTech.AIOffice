using System.Net.Http.Json;
using System.Text.Json;
using PasTech.AIOffice.Application.Interfaces;

namespace PasTech.AIOffice.Infrastructure.Agents;

public class GeminiProvider(HttpClient http, string apiKey) : ILlmProvider
{
    public string Name => "Gemini";
    public string ModelId => "gemini-1.5-pro";

    private static readonly string BaseUrl =
        "https://generativelanguage.googleapis.com/v1beta/models";

    public async Task<LlmResult> CompleteAsync(string system, string user, CancellationToken ct = default)
    {
        try
        {
            var url = $"{BaseUrl}/{ModelId}:generateContent?key={apiKey}";

            var body = new
            {
                systemInstruction = new
                {
                    parts = new[] { new { text = system } }
                },
                contents = new[]
                {
                    new { role = "user", parts = new[] { new { text = user } } }
                },
                generationConfig = new
                {
                    maxOutputTokens = 4096,
                    temperature = 0.7
                }
            };

            var response = await http.PostAsJsonAsync(url, body, ct);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: ct);
            var text = json
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString() ?? "";

            var inputTokens  = json.TryGetProperty("usageMetadata", out var meta)
                ? meta.GetProperty("promptTokenCount").GetInt32() : 0;
            var outputTokens = json.TryGetProperty("usageMetadata", out var meta2)
                ? meta2.GetProperty("candidatesTokenCount").GetInt32() : 0;

            return new LlmResult(text, inputTokens + outputTokens, true);
        }
        catch (Exception ex)
        {
            return new LlmResult("", 0, false, $"Gemini error: {ex.Message}");
        }
    }
}
