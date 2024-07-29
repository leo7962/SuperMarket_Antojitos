namespace SuperMarket_Antojitos.Server.Dtos;

public class SaleDetailDTO
{
    public int SaleId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}