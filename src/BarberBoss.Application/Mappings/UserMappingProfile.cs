using AutoMapper;
using BarberBoss.Communication.DTOs.Request.UserRequests;
using BarberBoss.Communication.DTOs.Response.UserResponses;
using BarberBoss.Domain.Entities;

namespace BarberBoss.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, RequestRegisterUserJson>().ReverseMap();
        CreateMap<User, RequestUpdateUserJson>().ReverseMap();
        CreateMap<User, ResponseUserJson>().ReverseMap();
    }
}