using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.UserMajor;
using FPLSP_Tutorial.Application.DataTransferObjects.UserMajor.Request;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles;

public class UserMajorProfile : Profile
{
    public UserMajorProfile()
    {
        CreateMap<UserMajorEntity, UserMajorDTO>()
            .ForMember(des => des.MajorCode, from => from
                .MapFrom(um => um.Major.Code))
            .ForMember(des => des.MajorName, from => from
                .MapFrom(um => um.Major.Name));
        CreateMap<UserMajorDeleteRequest, UserMajorEntity>();
        CreateMap<UserMajorUpdateRequest, UserMajorEntity>();
        CreateMap<UserMajorCreateRequest, UserMajorEntity>();
    }
}