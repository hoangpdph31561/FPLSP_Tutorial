using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Response;
using FPLSP_Tutorial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles
{
    public class ClientPostProfie : Profile
    {
        public ClientPostProfie()
        {
            CreateMap<PostEntity,ClientPostDetailResponse>().ForMember(dest => dest.UserCreatedName, opt => opt.Ignore());
            CreateMap<PostEntity, ClientPostDTO>();
            CreateMap<PostEntity, ClientPostListResponse>().ForMember(dest => dest.UserCreatedName, opt => opt.Ignore());
                
        }
    }
}
