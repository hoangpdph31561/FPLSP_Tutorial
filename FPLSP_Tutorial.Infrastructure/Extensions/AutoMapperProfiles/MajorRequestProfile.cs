using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest;
using FPLSP_Tutorial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles
{
    public class MajorRequestProfile : Profile
    {
        public MajorRequestProfile()
        {
            CreateMap<MajorRequestEntity, MajorRequestDto>().ForMember(des => des.MajorName, opt => opt.MapFrom(src => src.Major.Name))
                                                           .ForMember(des => des.Email, opt => opt.MapFrom(src => src.Major.UserMajors.Select(x => x.User.Email).FirstOrDefault())); 
            CreateMap<MajorRequestCreateRequest, MajorRequestEntity>();
            CreateMap<MajorRequestDeleteRequest, MajorRequestEntity>();
            CreateMap<MajorRequestUpdateRequest, MajorRequestEntity>();
        }
    }
}
