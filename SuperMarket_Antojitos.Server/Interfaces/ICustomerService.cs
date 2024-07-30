using SuperMarket_Antojitos.Server.Dtos;

namespace SuperMarket_Antojitos.Server.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDTO>> GetCustomersAsync();
    Task<CustomerDTO> GetCustomerByIdAsync(int id);
    Task<CustomerDTO> GetCustomerByDniAsyc(int dni);
    Task AddCustomerAsync(CustomerDTO customerDto);
    Task UpdateCustomerAsync(CustomerDTO customerDto);
    Task DeleteCustomerAsync(int id);
}