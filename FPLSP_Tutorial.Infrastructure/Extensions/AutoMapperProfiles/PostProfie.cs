using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Post;
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Response;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles
{
    public class PostProfie : Profile
    {
        public PostProfie()
        {
            CreateMap<PostEntity, PostDto>()
                .ForMember(dest => dest.ListTag, opt => opt.MapFrom(src => src.PostTags.Select(pt => pt.Tag)))
                .ForMember(dest => dest.CountPost, opt => opt.MapFrom(src => src.Posts.Select(pt => pt.Post).Count()));
            CreateMap<PostEntity, ViewPostByIdResponse>();
            CreateMap<PostEntity, ViewPostWithPaginationResponse>();
            CreateMap<PostCreateRequest, PostEntity>();
            CreateMap<PostUpdateRequest, PostEntity>();
        }
    }
}
