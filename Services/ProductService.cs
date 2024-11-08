using Microsoft.EntityFrameworkCore;
using ProductManagemet.Context;
using ProductManagemet.Models;
using ProductManagemet.ServiceContracts;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace ProductManagemet.Services
{
    public class ProductService: IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetProductAsync(string sortOrder = null, string searchTerm = null)
        {
            var productsQuery =  _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(p => p.ProductName.Contains(searchTerm));
                //productsQuery = productsQuery.Where(p => p.ProductDescription.Contains(searchTerm));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    productsQuery = productsQuery.OrderByDescending(p => p.ProductName);
                    break;
                case "name_asc":
                    productsQuery = productsQuery.OrderBy(p => p.ProductName);
                    break;
                default:
                    break;
            }

            return await productsQuery.ToListAsync();
          
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

   
        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public  async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task UpdateProductAsync(int id, Product product)
        {

            var existingProduct = await _context.Products.FindAsync(id);
            var productRate = new ProductRate
            {
                ProductId = existingProduct.ProductId,
                NewRate = existingProduct.ProductRate, 
                UpdatedDate = DateTime.Now

            };
            existingProduct.ProductName = product.ProductName;
            existingProduct.ProductDescription = product.ProductDescription;
            existingProduct.ProductRate = product.ProductRate; 
            existingProduct.UpdatedAt = DateTime.Now; 
              _context.ProductRates.Add(productRate);
             await _context.SaveChangesAsync();

        }

        public async Task<bool> SearchProductAsync(int id)
        {
           return  _context.Products.Any(e => e.ProductId == id);
        }
    }
}
