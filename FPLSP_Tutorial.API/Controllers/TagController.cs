using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Infrastructure.Database.AppDbContext;
using FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadOnly;
using FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadWrite;
using FPLSP_Tutorial.Infrastructure.ViewModels.News;
using FPLSP_Tutorial.Infrastructure.ViewModels.TagViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPLSP_Tutorial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly AppReadOnlyDbContext _dbContext;
        private readonly ITagReadOnlyRepository _tagReadOnlyRepository;
        private readonly ITagReadWriteRepository _tagReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public TagController(ITagReadOnlyRepository tagReadOnlyRepository, ITagReadWriteRepository tagReadWriteRepository, ILocalizationService localizationService, IMapper mapper, AppReadOnlyDbContext dbContext)
        {
            _tagReadOnlyRepository = tagReadOnlyRepository;
            _tagReadWriteRepository = tagReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TagCreateRequest request, CancellationToken cancellationToken)
        {
            TagCreateViewModel vm = new(_tagReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            TagViewModel vm = new(_tagReadOnlyRepository, _localizationService);

            await vm.HandleAsync(id, cancellationToken);

            return Ok(vm);
        }

    }
}
