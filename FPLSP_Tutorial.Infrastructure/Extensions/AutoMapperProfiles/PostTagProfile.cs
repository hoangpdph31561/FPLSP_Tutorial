using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.PostTag;
using FPLSP_Tutorial.Application.DataTransferObjects.PostTag.Request;
using FPLSP_Tutorial.Domain.Entities;

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
