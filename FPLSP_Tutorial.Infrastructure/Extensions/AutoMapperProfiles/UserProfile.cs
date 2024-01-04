using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.User;
using FPLSP_Tutorial.Application.DataTransferObjects.User.Request;
using FPLSP_Tutorial.Domain.Entities;
using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, UserDTO>()
            .ForMember(des => des.ListJoinedMajors, from => from
                .MapFrom(u => u.UserMajors
                    .Where(um => um.Status != EntityStatus.Deleted && !um.Deleted)));
        CreateMap<UserCreateRequest, UserEntity>();
        CreateMap<UserUpdateRequest, UserEntity>();
    }
}