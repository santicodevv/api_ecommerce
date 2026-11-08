using System.ComponentModel.DataAnnotations;

public class PaginationParams
{
    [Range(1, int.MaxValue, ErrorMessage = "Error: La pagina no puede ser mayor a 0")]
    public int Page { get; set; } = 1;
    [Range(1, 100, ErrorMessage = "Error: El tamaño de la pagina no puede ser mayor a 100")]
    public int PageSize { get; set; } = 10;
}