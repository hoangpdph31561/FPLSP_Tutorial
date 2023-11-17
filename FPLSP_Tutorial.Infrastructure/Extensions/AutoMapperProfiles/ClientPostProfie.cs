using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles
{
    public class ClientPostProfie : Profile
    {
        public ClientPostProfie()
        {
            CreateMap<PostEntity, PostDetailDTO>();
            CreateMap<PostEntity, PostMainDTO>()
                .ForMember(
                dest => dest.LstTags, opt => opt.MapFrom(src => src.PostTags.Select(pt => pt.Tag).Distinct()));
            CreateMap<PostEntity, PostBaseDTO>();
            CreateMap<InputMajorRequest, MajorRequestEntity>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.UserId));
            CreateMap<MajorEntity, MajorBaseDTO>()
    .ForMember(dest => dest.NumberOfPosts, opt => opt.MapFrom(src =>
        src.Tags.SelectMany(tag => tag.PostTags.Select(pt => pt.Post))
            .Count(post => !post.Deleted)));

            CreateMap<TagEntity, TagBaseDTO>();
        }

    }
}
