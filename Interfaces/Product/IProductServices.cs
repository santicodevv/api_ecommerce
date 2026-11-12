using ApiEcommerce.Models;

namespace ApiEcommerce.Services;

public interface IProductServices
{
    Task<ServiceResult<Product>> CreateProductAsync(Product product);
    Task<ServiceResult<Product>> UpdateProductAsync(int id, Product product);
    Task<ServiceResult<Product>> DeleteProductAsync(int id);
    Task<Product?> GetProductByIdAsync(int id);
    Task<List<Product>> GetProductsAsync(int page, int pageSize);
    Task<bool> ProductExistsAsync(int id);
    Task<bool> ProductExistsAsync(string name);
    Task<List<Product>> SearchProductAsync(string name);
    Task<ServiceResult<Product>> BuyProductAsync(string name, int quantity);
    Task<ServiceResult<List<Product>>> GetProductsForCategoryAsync(int categoryId);
    Task<ServiceResult<List<Product>>> GetProductsForNameDescriptionAsync(string name);
}
