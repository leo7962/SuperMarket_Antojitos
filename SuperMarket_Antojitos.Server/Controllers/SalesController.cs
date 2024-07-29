using Microsoft.AspNetCore.Mvc;
using SuperMarket_Antojitos.Server.Dtos;
using SuperMarket_Antojitos.Server.Interfaces;

namespace SuperMarket_Antojitos.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;

    public SalesController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SaleDTO>>> GetSales()
    {
        var sales = await _saleService.GetSalesAsync();
        return Ok(sales);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SaleDTO>> GetSale(int id)
    {
        var sale = await _saleService.GetSaleByIdAsync(id);
        if (sale == null) return NotFound();

        return Ok(sale);
    }

    [HttpPost]
    public async Task<ActionResult<SaleDTO>> PostSale([FromBody] SaleDTO saleDto)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _saleService.CreateAddSaleAsync(saleDto);
            return CreatedAtAction("GetSale", new { id = saleDto.SaleId }, saleDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutSale(int id, [FromBody] SaleDTO saleDto)
    {
        if (id != saleDto.SaleId) return BadRequest();

        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _saleService.UpdateSaleAsync(saleDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSale(int id)
    {
        var sale = await _saleService.GetSaleByIdAsync(id);
        if (sale == null) return NotFound();

        await _saleService.DeleteSaleAsync(id);
        return NoContent();
    }
}