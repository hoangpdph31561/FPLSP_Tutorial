using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.PostTag.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Infrastructure.ViewModels.PostTagViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FPLSP_Tutorial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostTagController : ControllerBase
    {
        private readonly IPostTagReadWriteRespository _postTagReadWriteRespository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public PostTagController(IPostTagReadWriteRespository postTagReadWriteRespository, ILocalizationService localizationService, IMapper mapper)
        {
            _postTagReadWriteRespository = postTagReadWriteRespository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostTagCreateRequest request, CancellationToken cancellationToken)
        {
            PostTagCreateViewModel vm = new(_postTagReadWriteRespository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Put(PostTagUpdateRequest request, CancellationToken cancellationToken)
        {
            PostTagUpdateViewModel vm = new(_postTagReadWriteRespository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(PostTagDeleteRequest request, CancellationToken cancellationToken)
        {
            PostTagDeleteViewModel vm = new(_postTagReadWriteRespository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }
    }
}
