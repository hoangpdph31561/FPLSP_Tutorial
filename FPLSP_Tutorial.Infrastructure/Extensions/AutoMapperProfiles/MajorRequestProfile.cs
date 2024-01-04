using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles;

public class MajorRequestProfile : Profile
{
    public MajorRequestProfile()
    {
        CreateMap<MajorRequestEntity, MajorRequestDTO>()
            .ForMember(des => des.MajorName, from => from
                .MapFrom(mr => mr.Major.Name))
            .ForMember(des => des.MajorCode, from => from
                .MapFrom(mr => mr.Major.Code));
        CreateMap<MajorRequestCreateRequest, MajorRequestEntity>();
        CreateMap<MajorRequestDeleteRequest, MajorRequestEntity>();
        CreateMap<MajorRequestUpdateRequest, MajorRequestEntity>();
    }
}