using System.Globalization;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using SuperMarket_Antojitos.Server.Dtos;
using SuperMarket_Antojitos.Server.Interfaces;

namespace SuperMarket_Antojitos.Server.Services;

public class InvoiceService : IInvoiceService
{
    public byte[] GenerateInvoicePdf(CustomerDTO customer, SaleDTO sale)
    {
        try
        {
            using (var stream = new MemoryStream())
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                document.Add(new Paragraph($"Invoice ID: {sale.SaleId}"));
                document.Add(new Paragraph($"Customer: {customer.FirstName} {customer.LastName}"));
                document.Add(new Paragraph($"Date: {sale.SaleDate:dd-MM-yyyy}"));

                document.Add(new Paragraph("\nSale Details:"));
                var table = new Table(3);
                table.AddHeaderCell("Product");
                table.AddHeaderCell("Quantity");
                table.AddHeaderCell("Total Price");

                foreach (var detail in sale.SaleDetails)
                {
                    table.AddCell(detail.ProductId.ToString());
                    table.AddCell(detail.Quantity.ToString());
                    table.AddCell(detail.TotalPrice.ToString(CultureInfo.CurrentCulture));
                }

                document.Add(table);
                document.Close();

                return stream.ToArray();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}