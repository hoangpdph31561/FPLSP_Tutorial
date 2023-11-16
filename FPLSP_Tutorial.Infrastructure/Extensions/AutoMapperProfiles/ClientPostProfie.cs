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
            CreateMap<PostEntity, PostMainDTO>().ForMember(
                dest => dest.ContentShortened, opt => opt.MapFrom(src => StringHandleExtensions.GetStringWithEllipsis(src.Content, 300)));
            CreateMap<PostEntity, PostBaseDTO>();
            CreateMap<InputMajorRequest, MajorRequestEntity>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.UserId));
            CreateMap<MajorEntity, MajorBaseDTO>();
            CreateMap<TagEntity, TagBaseDTO>();
        }
    }
}
