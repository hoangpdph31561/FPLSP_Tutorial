using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.Domain.Entities;


namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<TagEntity, TagDto>().ReverseMap();
            CreateMap<TagEntity, TagCreateRequest>().ReverseMap();
            CreateMap<TagEntity, TagUpdateRequest>().ReverseMap();
        }
    }
}
