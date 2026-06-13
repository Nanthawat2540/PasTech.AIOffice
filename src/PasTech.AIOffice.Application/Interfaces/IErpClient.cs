namespace PasTech.AIOffice.Application.Interfaces;

public interface IErpClient
{
    Task<int?> FindOrCreateCustomerAsync(string name, string email, string phone);
    Task<int?> CreateQuotationAsync(int customerId, string title, decimal amount, string items);
    Task<int?> CreateProjectAsync(int customerId, string title, DateTime startDate);
}
