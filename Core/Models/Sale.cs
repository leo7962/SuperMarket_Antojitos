using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Sale
{
    public int SaleId { get; set; }

    [Required(ErrorMessage = "The date of sale is mandatory")]
    public DateTime SaleDate { get; set; }

    [Required] public int CustomerId { get; set; }

    public Customer Customer { get; set; }
    public ICollection<SaleDetail> SaleDetails { get; set; }
}