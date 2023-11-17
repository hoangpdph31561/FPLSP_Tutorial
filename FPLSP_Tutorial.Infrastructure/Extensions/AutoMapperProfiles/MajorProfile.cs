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
            CreateMap<MajorEntity, MajorDTO>();
            CreateMap<MajorCreateRequest, MajorEntity>();
            CreateMap<MajorUpdateRequest, MajorEntity>();
            CreateMap<MajorDeleteRequest, MajorEntity>();

            CreateMap<MajorEntity, MajorDTO>()
                .ForMember(dest => dest.NumberOfLecturer, opt => opt.MapFrom(src => src.UserMajors.Count()));
            CreateMap<MajorEntity, MajorDTO>()
                .ForMember(dest => dest.NumberOfRequest, opt => opt.MapFrom(src => src.MajorRequests.Count()));
        }
    }
}
