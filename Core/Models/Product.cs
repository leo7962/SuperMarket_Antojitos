using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Product
{
    [Key] public int ProductId { get; set; }

    [StringLength(20)] public string ProductCode { get; set; }

    [Required(ErrorMessage = "Product name is mandatory")]
    [StringLength(100, ErrorMessage = "The name must be between 1 and 100 characters.")]
    public string ProductName { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than or equal to zero")]
    [Precision(18, 2)]
    public decimal UnitPrice { get; set; }

    [Required][Range(0, int.MaxValue)] public int UnitsInStock { get; set; }

    public ICollection<SaleDetail> SaleDetails { get; set; }
}