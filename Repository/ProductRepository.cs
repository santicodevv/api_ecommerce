using ApiEcommerce.Models;
using ApiEcommerce.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApiEcommerce.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _db;
    public ProductRepository(ApplicationDbContext dbContext)
    {
        _db = dbContext;
    }

    public async Task<List<Product>> GetProductsAsync(int page = 1, int pageSize = 10)
    {
        var result = await _db.Products.OrderBy(product => product.Id).Skip( (page - 1 ) * pageSize ).Take(pageSize).ToListAsync();
         
        return await _db.Products.OrderBy(product => product.Id).ToListAsync();
    }

    public async Task<List<Product>> GetProductsForCategoryAsync(int categoryId)
    {
        return await _db.Products.Where(product => product.CategoryId == categoryId).ToListAsync();
    }

    public async Task<List<Product>> SearchProductAsync(string name)
    {
        return await _db.Products.Where(product => product.Name.ToLower() == name.ToLower()).ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _db.Products.FirstOrDefaultAsync(product => product.Id == id);
    }

    public async Task<Product?> GetProductAsync(string name)
    {
        return await _db.Products.FirstOrDefaultAsync(product => product.Name.ToLower().Trim() == name.ToLower().Trim());
    }

    public async Task<bool> ProductExistsAsync(int id)
    {
        return await _db.Products.AnyAsync(product => product.Id == id);
    }

    public async Task<bool> ProductExistsAsync(string name)
    {
        return await _db.Products.AnyAsync(product => product.Name.ToLower().Trim() == name.ToLower().Trim());
    }

    public async Task<bool> CreateProductAsync(Product product)
    {
        product.CreatedAt = DateTime.Now;
        product.UpdatedAt = DateTime.Now;

        await _db.Products.AddAsync(product);
        return await SaveAsync();
    }

    
    public async Task<bool> UpdateProductAsync(Product product)
    {
        product.UpdatedAt = DateTime.Now;
        _db.Products.Update(product);
        return await SaveAsync();
    }
    
    public async Task<bool> DeleteProductAsync(Product product)
    {
        _db.Products.Remove(product);
        return await SaveAsync();
    }


    public async Task<bool> SaveAsync()
    {
        return await _db.SaveChangesAsync() >= 0;
    }

    
}