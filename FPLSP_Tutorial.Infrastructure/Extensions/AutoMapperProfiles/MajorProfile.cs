using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Major;
using FPLSP_Tutorial.Application.DataTransferObjects.Major.Request;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles
{
    public class MajorProfile : Profile
    {
        public MajorProfile()
        {
            CreateMap<MajorEntity, MajorDTOs>();
            CreateMap<MajorCreateRequest, MajorDTOs>();
            CreateMap<MajorUpdateRequest, MajorDTOs>();
            CreateMap<MajorDeleteRequest, MajorDTOs>();
            CreateMap<MajorCreateRequest, MajorDTOs>();
        }
    }
}
