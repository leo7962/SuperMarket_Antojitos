using AutoMapper;
using Core.Models;
using SuperMarket_Antojitos.Server.Dtos;

namespace SuperMarket_Antojitos.Server.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<Sale, SaleDTO>().ReverseMap();
        CreateMap<SaleDetail, SaleDetailDTO>().ReverseMap();
    }
}