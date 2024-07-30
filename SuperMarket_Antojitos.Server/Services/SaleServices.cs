using AutoMapper;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using SuperMarket_Antojitos.Server.Data;
using SuperMarket_Antojitos.Server.Dtos;
using SuperMarket_Antojitos.Server.Interfaces;

namespace SuperMarket_Antojitos.Server.Services;

public class SaleServices : ISaleService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public SaleServices(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SaleDTO>> GetSalesAsync()
    {
        var sales = await _context.Sales.ToListAsync();
        return _mapper.Map<IEnumerable<SaleDTO>>(sales);
    }

    public async Task<SaleDTO> GetSaleByIdAsync(int id)
    {
        var sale = await _context.Sales.FindAsync(id);
        return _mapper.Map<SaleDTO>(sale);
    }

    public async Task<SaleDTO> CreateAddSaleAsync(SaleDTO saleDto)
    {
        try
        {
            var sale = new Sale
            {
                SaleDate = DateTime.Now,
                CustomerId = saleDto.CustomerId
            };

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            foreach (var detailDto in saleDto.SaleDetails)
            {
                var product = await _context.Products.FindAsync(detailDto.ProductId);
                if (product == null) throw new Exception("Product not found");
                if (product.UnitsInStock < detailDto.Quantity)
                    throw new Exception(
                        $"Insufficient stock for product {product.ProductName}. Available: {product.UnitsInStock}, requested: {detailDto.Quantity}.");

                var saleDetail = new SaleDetail
                {
                    SaleId = sale.SaleId,
                    ProductId = detailDto.ProductId,
                    Quantity = detailDto.Quantity,
                    TotalPrice = detailDto.Quantity * product.UnitPrice
                };

                _context.SaleDetails.Add(saleDetail);

                product.UnitsInStock -= detailDto.Quantity;
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<SaleDTO>(sale);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateSaleAsync(SaleDTO saleDto)
    {
        var sale = _mapper.Map<Sale>(saleDto);
        _context.Sales.Update(sale);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSaleAsync(int id)
    {
        var sale = await _context.Sales.FindAsync(id);
        if (sale != null) _context.Sales.Remove(sale);
        await _context.SaveChangesAsync();
    }

    public async Task<SaleDTO> GetSaleByDetailAsync(int id)
    {
        var sale = await _context.Sales
        .Include(s => s.SaleDetails)
        .FirstOrDefaultAsync(s => s.SaleId == id);

        if (sale == null)
        {
            throw new Exception($"Sale with ID {id} not found.");
        }

        return _mapper.Map<SaleDTO>(sale);
    }
}