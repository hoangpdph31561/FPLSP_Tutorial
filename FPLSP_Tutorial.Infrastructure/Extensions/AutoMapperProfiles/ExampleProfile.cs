using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Example;
using FPLSP_Tutorial.Application.DataTransferObjects.Example.Request;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.Extensions.AutoMapperProfiles
{
    public class ExampleProfile : Profile
    {
        public ExampleProfile()
        {
            CreateMap<ExampleEntity, ExampleDto>();
            CreateMap<ExampleCreateRequest, ExampleDto>();
        }
    }
}
