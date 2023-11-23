using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Post;
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Response;
using FPLSP_Tutorial.Domain.Entities;
using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles
{
    public class PostProfie : Profile
    {
        public PostProfie()
        {
            CreateMap<PostEntity, PostDTO>();
            CreateMap<PostEntity, ViewPostByIdResponse>();
            CreateMap<PostEntity, ViewPostWithPaginationResponse>();
            CreateMap<PostCreateRequest, PostEntity>();
            CreateMap<PostUpdateRequest, PostEntity>();
        }
    }
}
