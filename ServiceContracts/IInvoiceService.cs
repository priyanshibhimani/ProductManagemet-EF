using ProductManagemet.Models;

namespace ProductManagemet.ServiceContracts
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetInvoiceAsync(int partyId,string sortOrder = null, string searchTerm = null);
        Task<IEnumerable<PartyWiseProduct>> GetPartyWiseProductAsync(int partyId);
        Task<IEnumerable<Product>> GetProductAsync();
        Task UpdateInvoiceAsync(Invoice invoice);
        Task<Invoice> GetInvoiceByIdAsync(int id);
        Task<Product> GetProductByIdAsync(Invoice invoice);

        Task<bool> SearchInvoiceAsync(int id);
        Task AddInvoiceAsync(Invoice invoice);
        Task DeleteInvoiceAsync(int id);
        Task GenerateTotal(int partyId);

    }
}
