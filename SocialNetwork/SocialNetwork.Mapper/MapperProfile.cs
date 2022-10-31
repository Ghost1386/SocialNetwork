using AutoMapper;
using SocialNetwork.Common.DTOs.UserDTOs;
using SocialNetwork.Models.Model;

namespace SocialNetwork.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CreateUserDTO, User>();
        CreateMap<DeleteUserDTO, User>();
        CreateMap<EditUserDTO, User>();
    }
}