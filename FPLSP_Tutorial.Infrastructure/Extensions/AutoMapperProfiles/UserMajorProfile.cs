using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles
{
    public class UserMajorProfile : Profile
    {
        public UserMajorProfile()
        {
            CreateMap<UserMajorEntity, UserMajorDTO>();
            CreateMap<UserMajorDeleteRequest, UserMajorEntity>();
            CreateMap<UserMajorUpdateRequest, UserMajorEntity>();
            CreateMap<UserMajorCreateRequest, UserMajorEntity>();
        }

    }
}
