using Anthropic.SDK;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PasTech.AIOffice.Application.Interfaces;
using PasTech.AIOffice.Infrastructure.Agents;
using PasTech.AIOffice.Infrastructure.Data;
using PasTech.AIOffice.Infrastructure.ErpClient;

namespace PasTech.AIOffice.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        // Database
        services.AddDbContext<AiOfficeDbContext>(opts =>
            opts.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddScoped<ITaskRepository, TaskRepository>();

        // Claude
        var claudeKey = config["Anthropic:ApiKey"] ?? "";
        services.AddSingleton(new AnthropicClient(claudeKey));
        services.AddSingleton<ClaudeProvider>();

        // Gemini (via plain HttpClient)
        var geminiKey = config["Gemini:ApiKey"] ?? "";
        services.AddHttpClient<GeminiProvider>(c =>
            c.Timeout = TimeSpan.FromSeconds(60));
        services.AddSingleton(sp =>
        {
            var http = sp.GetRequiredService<IHttpClientFactory>().CreateClient();
            return new GeminiProvider(http, geminiKey);
        });

        // Factory
        services.AddSingleton<LlmProviderFactory>();

        // Agents
        services.AddScoped<IAgentService, SalesAgent>();
        services.AddScoped<IAgentService, SurveyDesignAgent>();
        services.AddScoped<IAgentService, QuotationAgent>();
        services.AddScoped<IAgentService, ProjectAgent>();
        services.AddScoped<IAgentService, DocumentAgent>();
        services.AddScoped<IAgentService, SupportAgent>();
        services.AddScoped<IAgentService, MarketingAgent>();

        // ERP client
        services.AddHttpClient<IErpClient, ErpHttpClient>(client =>
        {
            var erpBase = config["ErpApi:BaseUrl"] ?? "http://58.8.94.168:5000/";
            client.BaseAddress = new Uri(erpBase);
            var token = config["ErpApi:Token"];
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        });

        return services;
    }
}
