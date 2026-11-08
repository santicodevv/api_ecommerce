using System;
using System.ComponentModel.DataAnnotations;

namespace ApiEcommerce.Models.Dtos;

public class CreateProductDTO
{
    [Required]
    [MaxLength(150, ErrorMessage = "El nombre debe ser menor a 150 caracteres")]
    [MinLength(3, ErrorMessage = "El nombre debe ser mayor a 3 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(150, ErrorMessage = "La descripcion debe ser menor a 150 caracteres")]
    [MinLength(3, ErrorMessage = "La descripcion debe ser mayor a 3 caracteres")]
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImgUrl { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public int Stock { get; set; }
    public DateTime? UpdatedAt { get; set; } = null;
    public int CategoryId { get; set; }
}
