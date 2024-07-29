using AutoMapper;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using SuperMarket_Antojitos.Server.Data;
using SuperMarket_Antojitos.Server.Dtos;
using SuperMarket_Antojitos.Server.Interfaces;

namespace SuperMarket_Antojitos.Server.Services;

public class ProductService : IProductService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ProductService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AddProductAsync(ProductDTO productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null) _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<ProductDTO> GetProductByIdAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        return _mapper.Map<ProductDTO>(product);
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
    {
        var products = await _context.Products.ToListAsync();
        return _mapper.Map<IEnumerable<ProductDTO>>(products);
    }

    public async Task UpdateProductAsync(ProductDTO productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}