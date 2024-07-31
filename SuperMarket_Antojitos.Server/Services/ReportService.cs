using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SuperMarket_Antojitos.Server.Data;
using SuperMarket_Antojitos.Server.Interfaces;

namespace SuperMarket_Antojitos.Server.Services;

public class ReportService : IReportService
{
    private readonly DataContext context;

    public ReportService(DataContext context)
    {
        this.context = context;
    }

    public async Task<byte[]> GenerateSalesReportExcel(DateTime startDate, DateTime endDate)
    {
        try
        {
            var sales = await context.Sales
                .Include(s => s.Customer)
                .Include(s => s.SaleDetails)
                    .ThenInclude(sd => sd.Product)
                .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                .ToListAsync();

            if (!sales.Any())
            {
                throw new ApplicationException("No sales data found for the specified date range.");
            }

            using (var package = new ExcelPackage())
            {
                var worsheet = package.Workbook.Worksheets.Add("Sales Report");

                worsheet.Cells[1, 1].Value = "Sale ID";
                worsheet.Cells[1, 2].Value = "Customer";
                worsheet.Cells[1, 3].Value = "Date";
                worsheet.Cells[1, 4].Value = "Product";
                worsheet.Cells[1, 5].Value = "Quantity";
                worsheet.Cells[1, 6].Value = "Total Price";

                var row = 2;

                foreach (var sale in sales)
                    foreach (var detail in sale.SaleDetails)
                    {
                        worsheet.Cells[row, 1].Value = sale.SaleId;
                        worsheet.Cells[row, 2].Value = $"{sale.Customer.FirstName} {sale.Customer.LastName}";
                        worsheet.Cells[row, 3].Value = sale.SaleDate.ToString("dd-MM-yyyy");
                        worsheet.Cells[row, 4].Value = detail.Product.ProductName;
                        worsheet.Cells[row, 5].Value = detail.Quantity;
                        worsheet.Cells[row, 6].Value = detail.TotalPrice;
                        row++;
                    }

                return await package.GetAsByteArrayAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating Excel report: {ex.Message}");
            throw new ApplicationException("An error occurred while generating the Excel report.", ex);
        }
    }

    public async Task<byte[]> GenerateSalesReportPdf(DateTime startDate, DateTime endDate)
    {
        try
        {
            var sales = await context.Sales
                .Include(s => s.Customer)
                .Include(s => s.SaleDetails)
                    .ThenInclude(sd => sd.Product)
                .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                .ToListAsync();

            if (!sales.Any())
            {
                throw new ApplicationException("No sales data found for the specified date range.");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                document.Add(new Paragraph("Sales Report"));
                document.Add(new Paragraph($"From: {startDate:dd-MM-yyyy} To: {endDate:dd-MM-yyyy}\n"));

                var table = new Table(6);
                table.AddHeaderCell("Sale ID");
                table.AddHeaderCell("Customer");
                table.AddHeaderCell("Date");
                table.AddHeaderCell("Product");
                table.AddHeaderCell("Quantity");
                table.AddHeaderCell("Total Price");

                foreach (var sale in sales)
                {
                    foreach (var detail in sale.SaleDetails)
                    {
                        table.AddCell(sale.SaleId.ToString());
                        table.AddCell($"{sale.Customer.FirstName} {sale.Customer.LastName}");
                        table.AddCell(sale.SaleDate.ToString("dd-MM-yyyy"));
                        table.AddCell(detail.Product.ProductName);
                        table.AddCell(detail.Quantity.ToString());
                        table.AddCell(detail.TotalPrice.ToString());
                    }
                }

                document.Add(table);
                document.Close();

                return stream.ToArray();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating PDF report: {ex.Message}");
            throw new ApplicationException("An error occurred while generating the PDF report.", ex);
        }
    }
}