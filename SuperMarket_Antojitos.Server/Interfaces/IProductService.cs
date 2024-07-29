using SuperMarket_Antojitos.Server.Dtos;

namespace SuperMarket_Antojitos.Server.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetProductsAsync();
    Task<ProductDTO> GetProductByIdAsync(int id);
    Task AddProductAsync(ProductDTO productDTO);
    Task UpdateProductAsync(ProductDTO productDTO);
    Task DeleteProductAsync(int id);
}