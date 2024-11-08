using ProductManagemet.Models;

namespace ProductManagemet.ServiceContracts
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductAsync(string sortOrder = null, string searchTerm = null);
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(int id, Product product);
        Task DeleteProductAsync(int id);
        Task<bool> SearchProductAsync(int id);

    }
}
