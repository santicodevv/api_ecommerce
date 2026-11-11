using System;
using System.Drawing;
using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using ApiEcommerce.Repository.IRepository;

namespace ApiEcommerce.Services;

public class ProductServices : IProductServices
{
    private readonly IProductRepository _product;
    private readonly ICategoryRepository _category;

    public ProductServices(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _product = productRepository;
        _category = categoryRepository;
    }

    public async Task<List<Product>> GetProductsAsync(int page, int pageSize)
    {
        return await _product.GetProductsAsync(page, pageSize);
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

        if (await _product.ProductExistsAsync(product.Name))
        {
            return new ServiceResult<Product>
            {
                Success = false,
                Message = "Error: El producto ya existe"
            };
        }

        if (!await _category.CategoryExistsAsync(product.CategoryId))
        {
            return new ServiceResult<Product>
            {
                Success = false,
                Message = $"Error: la categoria {product.CategoryId} no existe."
            };
        }

        var productCreated = await _product.CreateProductAsync(product);
        if (!productCreated)
        {
            return new ServiceResult<Product>
            {
                Success = false,
                Message = $"Error: Algo salio mal al guardar el registro {product.Name}"
            };
        }

        return new ServiceResult<Product> { Success = true, Message = "Producto creado exitosamente", Data = product};
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _product.GetProductByIdAsync(id);
    }

    public async Task<bool> ProductExistsAsync(int id)
    {
        return await _product.ProductExistsAsync(id);
    }

    public async Task<bool> ProductExistsAsync(string name)
    {
        return await _product.ProductExistsAsync(name);
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

        var product = await _product.GetProductAsync(name);

        if (product is null)
        {
            return new ServiceResult<Product>
            {
                Success = false,
                Message = "Error: El producto no existe"
            };
        }

        product.Stock -= quantity;
        var updateProduct = await _product.UpdateProductAsync(product);
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


    public async Task<ServiceResult<List<Product>>> GetProductsForCategoryAsync(int categoryId)
    {
        if (!await _category.CategoryExistsAsync(categoryId))
        {
           return new ServiceResult<List<Product>>
            {
                Success = false,
                Message = $"Error: La categoria {categoryId} no existe"
            };   
        }

        var products = await _product.GetProductsForCategoryAsync(categoryId);

        return new ServiceResult<List<Product>>
        {
            Success = true,
            Data = products
        }; ;
    }
}
