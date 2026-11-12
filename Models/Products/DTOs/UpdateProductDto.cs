using System;
using System.ComponentModel.DataAnnotations;

namespace ApiEcommerce.Models.Dtos;

public class ProductProductDTO
{
    [Required]
    [MaxLength(150, ErrorMessage = "El nombre debe ser menor a 150 caracteres")]
    [MinLength(3, ErrorMessage = "El nombre debe ser mayor a 3 caracteres")]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImgUrl { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int CategoryId { get; set; }
}
