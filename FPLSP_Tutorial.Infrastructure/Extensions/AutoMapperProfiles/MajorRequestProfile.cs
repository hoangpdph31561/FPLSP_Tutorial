using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest;
using FPLSP_Tutorial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles
{
    public class MajorRequestProfile : Profile
    {
        public MajorRequestProfile()
        {
            CreateMap<MajorRequestEntity, MajorRequestDto>().ForMember(des => des.tenChuyenNganh, src => src.MapFrom(src => src.Major.Name));
            CreateMap<MajorRequestCreateRequest, MajorRequestEntity>();
            CreateMap<MajorRequestDeleteRequest, MajorRequestEntity>();
            CreateMap<MajorRequestUpdateRequest, MajorRequestEntity>();
        }
    }
}
