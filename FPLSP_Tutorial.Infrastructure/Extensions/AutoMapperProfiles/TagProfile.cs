using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<TagEntity, TagDTO>();
        CreateMap<TagUpdateRequest, TagEntity>();
        CreateMap<TagCreateRequest, TagEntity>();
        CreateMap<TagDTO, TagEntity>();
    }
}