using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Major;
using FPLSP_Tutorial.Application.DataTransferObjects.Major.Request;
using FPLSP_Tutorial.Domain.Entities;
using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles;

public class MajorProfile : Profile
{
    public MajorProfile()
    {
        CreateMap<MajorEntity, MajorDTO>()
            .ForMember(des => des.NumberOfLecturer, from => from
                .MapFrom(m => m.UserMajors
                    .Where(m => m.Status != EntityStatus.Deleted && !m.Deleted)
                    .Select(um => um.User).Where(u => u.Status != EntityStatus.Deleted)
                    .Distinct().Count()))
            .ForMember(des => des.NumberOfLecturerRequest, from => from
                .MapFrom(m => m.MajorRequests
                    .Where(mr => mr.Status != EntityStatus.Deleted && !mr.Deleted)
                    .Distinct().Count()))
            .ForMember(des => des.ListTag, from => from
                .MapFrom(m => m.Tags
                    .Where(t => t.Status != EntityStatus.Deleted && !t.Deleted)))
            .ForMember(des => des.NumberOfPost, from => from
                .MapFrom(m => m.Tags
                    .Where(t => t.Status != EntityStatus.Deleted && !t.Deleted)
                    .SelectMany(t => t.PostTags)
                    .Where(pt => pt.Status != EntityStatus.Deleted && !pt.Deleted).Count()));

        CreateMap<MajorCreateRequest, MajorEntity>();
        CreateMap<MajorUpdateRequest, MajorEntity>();
        CreateMap<MajorDeleteRequest, MajorEntity>();
    }
}