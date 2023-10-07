using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag;
using FPLSP_Tutorial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.PostTag;
using FPLSP_Tutorial.Application.DataTransferObjects.PostTag.Request;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles
{
    public class PostTagProfile : Profile
    {
        public PostTagProfile()
        {
            CreateMap<PostTagEntity, PostTagDto>().ReverseMap();
            CreateMap<PostTagEntity, PostTagCreateRequest>().ReverseMap();
            CreateMap<PostTagEntity, PostTagUpdateRequest>().ReverseMap();
        }
    }
}
