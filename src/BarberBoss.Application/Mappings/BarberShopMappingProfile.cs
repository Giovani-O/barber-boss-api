using AutoMapper;
using BarberBoss.Communication.DTOs.Request.BarberShopRequests;
using BarberBoss.Communication.DTOs.Response.BarberShopResponses;
using BarberBoss.Domain.Entities;

namespace BarberBoss.Application.Mappings;

public class BarberShopMappingProfile : Profile
{
    public BarberShopMappingProfile()
    {
        CreateMap<BarberShop, RequestRegisterBarberShopJson>().ReverseMap();
        CreateMap<BarberShop, RequestUpdateBarberShopJson>().ReverseMap();
        CreateMap<BarberShop, ResponseBarberShopJson>().ReverseMap();
    }
}