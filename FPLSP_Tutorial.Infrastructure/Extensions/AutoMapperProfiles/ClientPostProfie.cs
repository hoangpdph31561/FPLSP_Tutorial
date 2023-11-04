using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Response;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles
{
    public class ClientPostProfie : Profile
    {
        public ClientPostProfie()
        {
            CreateMap<PostEntity, ClientPostDetailResponse>().ForMember(dest => dest.UserCreatedName, opt => opt.Ignore());
            CreateMap<PostEntity, ClientPostDTO>();
            CreateMap<PostEntity, ClientPostListResponse>()
                .ForMember(dest => dest.UserCreatedName, opt => opt.Ignore());
            CreateMap<PostEntity, ClientPostParentChildResponse>()
                .ForMember(dest => dest.ParentPost, opt => opt.MapFrom(src => src.PostId == null ? new ClientPostDTO
                {
                    Id = src.Id,
                    Title = src.Title,
                } : new ClientPostDTO
                {
                    Id = src.Post.Id,
                    Title = src.Post.Title,
                }))
                .ForMember(dest => dest.ChildPosts, opt => opt.MapFrom(src => src.PostId == null ? src.Posts.Select(child =>
                new ClientPostDTO
                {
                    Id = child.Id,
                    Title = child.Title,
                }) : src.Post.Posts.Where(child => child.Id != src.Id).Select(child => new ClientPostDTO
                {
                    Id = child.Id,
                    Title = child.Title,
                })));

            CreateMap<PostEntity, ClientTagResponse>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.PostTags.Select(pt => new TagBaseDTO
                {
                    Id = pt.TagId,
                    Name = pt.Tag.Name
                })));
            CreateMap<InputMajorRequest, MajorRequestEntity>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.UserId));
            CreateMap<MajorEntity, ClientPost_MajorDTO>();

        }
    }
}
