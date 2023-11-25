using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Post;
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Response;
using FPLSP_Tutorial.Domain.Entities;
using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostEntity, PostDTO>()
                .ForMember(des => des.ListTag, from => from
                    .MapFrom(p => p.PostTags
                        .Where(pt => pt.Status != EntityStatus.Deleted && !pt.Deleted)
                            .Select(pt => pt.Tag).Where(t => t.Status != EntityStatus.Deleted && !t.Deleted)))
                .ForMember(des => des.NumberOfChildPost, from => from
                    .MapFrom(p => p.Posts
                        .Where(pc => pc.Status != EntityStatus.Deleted && !pc.Deleted).Count()));
            CreateMap<PostEntity, ViewPostByIdResponse>();
            CreateMap<PostEntity, ViewPostWithPaginationResponse>();
            CreateMap<PostCreateRequest, PostEntity>();
            CreateMap<PostUpdateRequest, PostEntity>();
        }
    }
}
