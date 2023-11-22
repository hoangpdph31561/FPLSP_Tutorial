using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.User;
using FPLSP_Tutorial.Application.DataTransferObjects.User.Request;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserDTO>()
                .ForMember(
                    dest => dest.ListJoinedMajors, 
                    opt => opt.MapFrom(
                        src => src.UserMajors
                            .Where(um => um.Status != Domain.Enums.EntityStatus.Deleted)
                            .Select(m => m.Major)
                            .Where(m => m.Status != Domain.Enums.EntityStatus.Deleted)));

            CreateMap<UserCreateRequest, UserEntity>();
            CreateMap<UserUpdateRequest, UserEntity>();

        }
    }
}
