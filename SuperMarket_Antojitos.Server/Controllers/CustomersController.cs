using Microsoft.AspNetCore.Mvc;
using SuperMarket_Antojitos.Server.Dtos;
using SuperMarket_Antojitos.Server.Interfaces;

namespace SuperMarket_Antojitos.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetGustomers()
    {
        var customers = await _customerService.GetCustomersAsync();
        return Ok(customers);
    }

    [HttpGet("{id:int}", Name ="GetUserByID")]
    public async Task<ActionResult<CustomerDTO>> Getcustomer(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null) return NotFound();

        return Ok(customer);
    }

    [HttpGet("byDni/{id:int}", Name = "GetUserByDNI")]
    public async Task<ActionResult<CustomerDTO>> GetCustomerByDNI(int dni)
    {
        var customer = await _customerService.GetCustomerByDniAsyc(dni);
        if (customer == null) return NotFound();
        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDTO>> PostCustomer([FromBody] CustomerDTO customerDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _customerService.AddCustomerAsync(customerDto);
        return CreatedAtAction("GetCustomer", new { id = customerDto.CustomerId }, customerDto);
    }    

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutCustomer(int id, [FromBody] CustomerDTO customerDto)
    {
        if (id != customerDto.CustomerId) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _customerService.UpdateCustomerAsync(customerDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null) return NotFound();

        await _customerService.DeleteCustomerAsync(id);
        return NoContent();
    }
}