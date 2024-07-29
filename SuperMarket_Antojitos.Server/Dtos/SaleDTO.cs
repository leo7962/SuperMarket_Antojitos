namespace SuperMarket_Antojitos.Server.Dtos;

public class SaleDTO
{
    public int SaleId { get; set; }
    public DateTime SaleDate { get; set; }
    public int CustomerId { get; set; }
    public List<SaleDetailDTO> SaleDetails { get; set; }
}