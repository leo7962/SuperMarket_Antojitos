using Microsoft.AspNetCore.Mvc;
using SuperMarket_Antojitos.Server.Interfaces;

namespace SuperMarket_Antojitos.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoicesController : ControllerBase
{
    private readonly ICustomerService customerService;
    private readonly IInvoiceService invoiceService;
    private readonly ISaleService saleService;

    public InvoicesController(IInvoiceService invoiceService, ISaleService saleService,
        ICustomerService customerService)
    {
        this.invoiceService = invoiceService;
        this.saleService = saleService;
        this.customerService = customerService;
    }

    [HttpGet("export/pdf/{saleId:int}")]
    public async Task<IActionResult> ExportInvoicePDF(int saleId)
    {
        var sale = await saleService.GetSaleByIdAsync(saleId);
        var customer = await customerService.GetCustomerByIdAsync(sale.CustomerId);

        var fileContents = invoiceService.GenerateInvoicePdf(customer, sale);

        return File(fileContents, "application/pdf", $"Invoice_{customer.FirstName}.PDF");
    }
}