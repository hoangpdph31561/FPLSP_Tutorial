using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Infrastructure.ViewModels.TagViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FPLSP_Tutorial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagReadOnlyRepository _tagReadOnlyRepository;
        private readonly ITagReadWriteRepository _tagReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public TagController(ITagReadOnlyRepository tagReadOnlyRepository, ITagReadWriteRepository tagReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _tagReadOnlyRepository = tagReadOnlyRepository;
            _tagReadWriteRepository = tagReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
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

        [HttpGet("getTagsByMajorId")]
        public async Task<IActionResult> GetListTag([FromQuery]Guid? id, CancellationToken cancellationToken)
        {
            ListTagViewModel vm = new(_tagReadOnlyRepository, _localizationService);

            await vm.HandleAsync(id, cancellationToken);

            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Put(TagUpdateRequest request, CancellationToken cancellationToken)
        {
            TagUpdateViewModel vm = new(_tagReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(TagDeleteRequest request, CancellationToken cancellationToken)
        {
            TagDeleteViewModel vm = new(_tagReadWriteRepository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

    }
}
