using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Customer
{
    [Key] public int CustomerId { get; set; }

    [Required][StringLength(15)] public int DNI { get; set; }

    [Required(ErrorMessage = "The firstname is mandatory")]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "The lastname is mandatory")]
    [StringLength(50)]
    public string LastName { get; set; }

    [StringLength(100)] public string Address { get; set; }

    [Phone] public string PhoneNumber { get; set; }

    [EmailAddress(ErrorMessage = "Enter a valid email address")]
    public string Email { get; set; }

    public ICollection<Sale> Sales { get; set; }
}