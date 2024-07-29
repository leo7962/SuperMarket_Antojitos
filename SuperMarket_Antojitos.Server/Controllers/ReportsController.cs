using Microsoft.AspNetCore.Mvc;
using SuperMarket_Antojitos.Server.Interfaces;

namespace SuperMarket_Antojitos.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly IReportService reportService;

    public ReportsController(IReportService reportService)
    {
        this.reportService = reportService;
    }

    [HttpGet("export/excel")]
    public async Task<IActionResult> ExportSalesReportExcel([FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        var fileContents = await reportService.GenerateSalesReportExcel(startDate, endDate);
        return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "SalesReport.xlsx}");
    }

    [HttpGet("export/pdf")]
    public async Task<IActionResult> ExportSalesReportPdf([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var fileContents = await reportService.GenerateSalesReportPdf(startDate, endDate);
        return File(fileContents, "application/pdf", "SalesReport.pdf");
    }
}