using AutoMapper;
using LabLINQ.DTOs;
using LabLINQ.Models;

namespace LabLINQ.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Client, ClientDto>();
        CreateMap<Product, ProductDto>();
        CreateMap<Order, OrderDto>();
    }
}