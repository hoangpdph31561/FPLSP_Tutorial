using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.User.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.User;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserDTO>();
            CreateMap<UserCreateRequest, UserDTO>();
            CreateMap<UserUpdateRequest, UserDTO>();
            CreateMap<UserDeleteRequest, UserDTO>();
            CreateMap<UserCreateRequest, UserDTO>();
        }
    }
}
