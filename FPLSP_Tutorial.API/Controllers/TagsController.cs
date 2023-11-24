using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Infrastructure.ViewModels.TagViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FPLSP_Tutorial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagReadOnlyRepository _tagReadOnlyRepository;
        private readonly ITagReadWriteRepository _tagReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public TagsController(ITagReadOnlyRepository tagReadOnlyRepository, ITagReadWriteRepository tagReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _tagReadOnlyRepository = tagReadOnlyRepository;
            _tagReadWriteRepository = tagReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        [HttpGet("GetListWithPagination")]
        public async Task<IActionResult> GetListWithPagination([FromQuery] TagViewWithPaginationRequest request, CancellationToken cancellationToken)
        {
            TagListWithPaginationViewModel vm = new(_tagReadOnlyRepository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);

            if (vm.Success)
            {
                PaginationResponse<TagDTO> result = new();
                result = (PaginationResponse<TagDTO>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            TagViewModel vm = new(_tagReadOnlyRepository, _localizationService);

            await vm.HandleAsync(id, cancellationToken);

            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TagCreateRequest request, CancellationToken cancellationToken)
        {
            TagCreateViewModel vm = new(_tagReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }


        [HttpPut]
        public async Task<IActionResult> Update(TagUpdateRequest request, CancellationToken cancellationToken)
        {
            TagUpdateViewModel vm = new(_tagReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] TagDeleteRequest request, CancellationToken cancellationToken)
        {
            TagDeleteViewModel vm = new(_tagReadWriteRepository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

    }
}
