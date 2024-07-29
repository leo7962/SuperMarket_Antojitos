namespace SuperMarket_Antojitos.Server.Dtos;

public class ProductDTO
{
    public int ProductId { get; set; }
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
}