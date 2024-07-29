using Microsoft.AspNetCore.Mvc;
using SuperMarket_Antojitos.Server.Dtos;
using SuperMarket_Antojitos.Server.Interfaces;

namespace SuperMarket_Antojitos.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
    {
        var products = await _productService.GetProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDTO>> GetProduct(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> PostCustomer([FromBody] ProductDTO productDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _productService.AddProductAsync(productDto);
        return CreatedAtAction("GetProduct", new { id = productDto.ProductId }, productDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutProduct(int id, [FromBody] ProductDTO productDto)
    {
        if (id != productDto.ProductId) return BadRequest();

        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _productService.UpdateProductAsync(productDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();

        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}