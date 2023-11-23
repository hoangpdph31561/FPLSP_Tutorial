using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles
{
    public class MajorRequestProfile : Profile
    {
        public MajorRequestProfile()
        {
            CreateMap<MajorRequestEntity, MajorRequestDTO>();
            CreateMap<MajorRequestCreateRequest, MajorRequestEntity>();
            CreateMap<MajorRequestDeleteRequest, MajorRequestEntity>();
            CreateMap<MajorRequestUpdateRequest, MajorRequestEntity>();
        }
    }
}
