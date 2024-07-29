using SuperMarket_Antojitos.Server.Dtos;

namespace SuperMarket_Antojitos.Server.Interfaces;

public interface IInvoiceService
{
    byte[] GenerateInvoicePdf(CustomerDTO customerDTO, SaleDTO saleDTO);
}