using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class SaleDetail
{
    [Key] public int SaleDetailId { get; set; }

    [Required] public int SaleId { get; set; }

    public Sale Sale { get; set; }

    [Required] public int ProductId { get; set; }

    public Product Product { get; set; }

    [Required][Range(1, int.MaxValue)] public int Quantity { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than or equal to zero")]
    [Precision(18, 2)]
    public decimal TotalPrice { get; set; }
}