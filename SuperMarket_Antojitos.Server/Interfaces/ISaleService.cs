using SuperMarket_Antojitos.Server.Dtos;

namespace SuperMarket_Antojitos.Server.Interfaces;

public interface ISaleService
{
    Task<IEnumerable<SaleDTO>> GetSalesAsync();
    Task<SaleDTO> GetSaleByIdAsync(int id);
    Task<SaleDTO> GetSaleByDetailAsync(int id);
    Task<SaleDTO> CreateAddSaleAsync(SaleDTO saleDto);
    Task UpdateSaleAsync(SaleDTO saleDto);
    Task DeleteSaleAsync(int id);
}