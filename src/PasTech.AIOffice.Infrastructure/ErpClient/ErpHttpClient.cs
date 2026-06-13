using System.Net.Http.Json;
using PasTech.AIOffice.Application.Interfaces;

namespace PasTech.AIOffice.Infrastructure.ErpClient;

public class ErpHttpClient(HttpClient http) : IErpClient
{
    public async Task<int?> FindOrCreateCustomerAsync(string name, string email, string phone)
    {
        try
        {
            var response = await http.PostAsJsonAsync("api/v1/customers", new
            {
                name,
                email,
                phone,
                customer_type = "Corporate",
                credit_limit = 0m,
                credit_days = 30
            });
            if (!response.IsSuccessStatusCode) return null;
            var result = await response.Content.ReadFromJsonAsync<ErpResponse<ErpIdDto>>();
            return result?.Data?.Id;
        }
        catch { return null; }
    }

    public async Task<int?> CreateQuotationAsync(int customerId, string title, decimal amount, string items)
    {
        try
        {
            var response = await http.PostAsJsonAsync("api/v1/quotations", new
            {
                customer_id = customerId,
                title,
                total_amount = amount,
                notes = items,
                valid_days = 30
            });
            if (!response.IsSuccessStatusCode) return null;
            var result = await response.Content.ReadFromJsonAsync<ErpResponse<ErpIdDto>>();
            return result?.Data?.Id;
        }
        catch { return null; }
    }

    public async Task<int?> CreateProjectAsync(int customerId, string title, DateTime startDate)
    {
        try
        {
            var response = await http.PostAsJsonAsync("api/v1/projects", new
            {
                customer_id = customerId,
                name = title,
                start_date = startDate,
                status = "Planning"
            });
            if (!response.IsSuccessStatusCode) return null;
            var result = await response.Content.ReadFromJsonAsync<ErpResponse<ErpIdDto>>();
            return result?.Data?.Id;
        }
        catch { return null; }
    }

    private record ErpResponse<T>(T? Data, bool Success);
    private record ErpIdDto(int Id);
}
