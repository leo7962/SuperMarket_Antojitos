namespace SuperMarket_Antojitos.Server.Interfaces;

public interface IReportService
{
    Task<byte[]> GenerateSalesReportExcel(DateTime startDate, DateTime endDate);
    Task<byte[]> GenerateSalesReportPdf(DateTime startDate, DateTime endDate);
}