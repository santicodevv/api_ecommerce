using ApiEcommerce.Models;

namespace ApiEcommerce.Repository.IRepository;

public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync(int page, int pageSize);
    Task<List<Product>> GetProductsForCategoryAsync(int categoryId);
    Task<List<Product>> SearchProductAsync(string name);
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product?> GetProductAsync(string name);
    Task<bool> ProductExistsAsync(int id);
    Task<bool> ProductExistsAsync(string name);
    Task<bool> CreateProductAsync(Product product);
    Task<bool> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(Product product);
    Task<bool> SaveAsync();
}