using AutoMapper;
using BarberBoss.Communication.DTOs.Request.IncomeRequests;
using BarberBoss.Communication.DTOs.Response.IncomeResponses;
using BarberBoss.Domain.Entities;

namespace BarberBoss.Application.Mappings;

public class IncomeMappingProfile : Profile
{
    public IncomeMappingProfile()
    {
        CreateMap<Income, RequestRegisterIncomeJson>().ReverseMap();
        CreateMap<Income, RequestUpdateIncomeJson>().ReverseMap();
        CreateMap<Income, ResponseIncomeJson>().ReverseMap();
    }
}