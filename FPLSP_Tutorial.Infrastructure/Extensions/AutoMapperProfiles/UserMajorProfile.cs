using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser;
using FPLSP_Tutorial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles
{
    public class UserMajorProfile : Profile
    {
        public UserMajorProfile()
        {
            CreateMap<UserMajorEntity, MajorUserDto>().ForMember(des => des.TenChuyenNganh, src => src.MapFrom(src => src.Major.Name))
                                               .ForMember(des => des.RoleCodes, src => src.MapFrom(src => src.User.RoleCodes))
                                               .ForMember(des => des.email,src => src.MapFrom(src => src.User.Email));
            CreateMap<DeleteMajorUserRequest, UserMajorEntity>();
            CreateMap<UpdateMajorUserRequest, UserMajorEntity>();
            CreateMap<CreateUserMajorRequest, UserMajorEntity>();
        }
     
    }
}
