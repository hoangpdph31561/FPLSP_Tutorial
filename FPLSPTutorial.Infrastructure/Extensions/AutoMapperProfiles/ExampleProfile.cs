using AutoMapper;
using FPLSPTutorial.Application.DataTransferObjects.Example;
using FPLSPTutorial.Domain.Entities;

namespace FPLSPTutorial.Infrastructure.Extensions.AutoMapperProfiles
{
    public class ExampleProfile : Profile
    {
        public ExampleProfile()
        {
            CreateMap<ExampleEntity, ExampleDto>();

        }
    }
}
