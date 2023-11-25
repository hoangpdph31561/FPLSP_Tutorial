using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.PostTag.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Infrastructure.ViewModels.PostTag;
using Microsoft.AspNetCore.Mvc;

namespace FPLSP_Tutorial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostTagsController : ControllerBase
    {
        private readonly IPostTagReadWriteRepository _postTagReadWriteRespository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public PostTagsController(IPostTagReadWriteRepository postTagReadWriteRespository, ILocalizationService localizationService, IMapper mapper)
        {
            _postTagReadWriteRespository = postTagReadWriteRespository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(PostTagCreateRequest request, CancellationToken cancellationToken)
        {
            PostTagCreateViewModel vm = new(_postTagReadWriteRespository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpPost("AddRange")]
        public async Task<IActionResult> AddRangeAsync(List<PostTagCreateRequest> request, CancellationToken cancellationToken)
        {
            PostTagCreateRangeViewModel vm = new(_postTagReadWriteRespository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(PostTagUpdateRequest request, CancellationToken cancellationToken)
        {
            PostTagUpdateViewModel vm = new(_postTagReadWriteRespository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(PostTagDeleteRequest request, CancellationToken cancellationToken)
        {
            PostTagDeleteViewModel vm = new(_postTagReadWriteRespository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }
    }
}
