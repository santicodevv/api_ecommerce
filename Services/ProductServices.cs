using System;
using System.Drawing;
using ApiEcommerce.Models;
using ApiEcommerce.Repository.IRepository;

namespace ApiEcommerce.Services;

public class ProductServices : IProductServices
{
    private readonly IProductRepository _category;
    public ProductServices(IProductRepository productRepository)
    {
        _category = productRepository;
    }

    public async Task<List<Product>> GetProductsAsync(int page, int pageSize)
    {
        return await _category.GetProductsAsync(page, pageSize);
    }

    public async Task<ServiceResult<Product>> CreateProductAsync(Product product)
    {
        if (product is null)
        {
            return new ServiceResult<Product>
            {
                Success = false,
                Message = "Error: El producto no valido"
            };
        }

        if (product.CategoryId == 0)
        {
            return new ServiceResult<Product>
            {
                Success = false,
                Message = "Error: La categoria debe tener un id existente!"
            };
        }

        if (await _category.ProductExistsAsync(product.Name))
        {
            return new ServiceResult<Product>
            {
                Success = false,
                Message = "Error: El producto ya existe"
            };
        }

        var productCreated = await _category.CreateProductAsync(product);
        if (!productCreated)
        {
            return new ServiceResult<Product>
            {
                Success = false,
                Message = "Error: El producto no se pudo crear"
            };
        }

        return new ServiceResult<Product> { Success = true, Message = "Producto creado exitosamente", Data = product};
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _category.GetProductByIdAsync(id);
    }

    public async Task<bool> ProductExistsAsync(int id)
    {
        return await _category.ProductExistsAsync(id);
    }

    public async Task<bool> ProductExistsAsync(string name)
    {
        return await _category.ProductExistsAsync(name);
    }

    public Task<List<Product>> SearchProductAsync(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<Product>> BuyProductAsync(string name, int quantity)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return new ServiceResult<Product>
            {
                Success = false,
                Message = "Error: El nombre del producto debe ser obligatorio",
            };
        }

        if (quantity <= 0)
        {
            return new ServiceResult<Product>
            {
                Success = false,
                Message = "Error: la cantidad de stock debe ser valida",
            };
        }

        var product = await _category.GetProductAsync(name);

        if (product is null)
        {
            return new ServiceResult<Product>
            {
                Success = false,
                Message = "Error: El producto no existe"
            };
        }

        product.Stock -= quantity;
        var updateProduct = await _category.UpdateProductAsync(product);
        if (!updateProduct)
        {
            return new ServiceResult<Product>
            {
                Success = false,
                Message = "Error: No se pudo actualizar el producto en la compra"
            };  
        }
        
        return new ServiceResult<Product>
        {
            Success = true,
            Message = "Producto comprado exitosamente",
            Data = product
        };  
    }
}
