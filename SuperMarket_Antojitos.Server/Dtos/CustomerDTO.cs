namespace SuperMarket_Antojitos.Server.Dtos;

public class CustomerDTO
{
    public int CustomerId { get; set; }
    public int DNI { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}