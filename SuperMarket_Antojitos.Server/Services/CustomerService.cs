using AutoMapper;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using SuperMarket_Antojitos.Server.Data;
using SuperMarket_Antojitos.Server.Dtos;
using SuperMarket_Antojitos.Server.Interfaces;

namespace SuperMarket_Antojitos.Server.Services;

public class CustomerService : ICustomerService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CustomerService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AddCustomerAsync(CustomerDTO customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer != null) _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    public async Task<CustomerDTO> GetCustomerByDniAsyc(int dni)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.DNI == dni);
        return _mapper.Map<CustomerDTO>(customer);
    }

    public async Task<CustomerDTO> GetCustomerByIdAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        return _mapper.Map<CustomerDTO>(customer);
    }

    public async Task<IEnumerable<CustomerDTO>> GetCustomersAsync()
    {
        var customers = await _context.Customers.ToListAsync();
        return _mapper.Map<IEnumerable<CustomerDTO>>(customers);
    }

    public async Task UpdateCustomerAsync(CustomerDTO customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }
}