using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ClientPostReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ClientPostReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Infrastructure.ViewModels.ClientPost;
using Microsoft.AspNetCore.Mvc;

namespace FPLSP_Tutorial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLientPostsController : ControllerBase
    {
        private readonly IClientPostReadOnlyRespository _clientPostReadOnlyRespository;
        private readonly IClientPostReadWriteRespository _clientPostReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public CLientPostsController(IClientPostReadOnlyRespository clientPostReadOnlyRespository, IClientPostReadWriteRespository clientPostReadWriteRespository, IMapper mapper, ILocalizationService localizationService)
        {
            _clientPostReadOnlyRespository = clientPostReadOnlyRespository;
            _mapper = mapper;
            _localizationService = localizationService;
            _clientPostReadWriteRespository = clientPostReadWriteRespository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(Guid id, CancellationToken cancellationToken)
        {
            ClientPostDetailViewModel vm = new(_clientPostReadOnlyRespository, _mapper, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("getByMajorId")]
        public async Task<IActionResult> GetPostByMajorId([FromQuery] ClientPostListRequest request, CancellationToken cancellationToken)
        {
            ClientPostGetByMajorIdViewModel vm = new(_clientPostReadOnlyRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("getPostTag/{id}")]
        public async Task<IActionResult> GetPostTag(Guid id, CancellationToken cancellationToken)
        {
            ClientPostGetPostTagViewModel vm = new(_clientPostReadOnlyRespository, _mapper, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("getParentChildPost")]
        public async Task<IActionResult> GetParentChildPost([FromQuery] ClientPostRequestIdWithPagination request, CancellationToken cancellationToken)
        {
            ClientPostParentChildViewModel vm = new(_clientPostReadOnlyRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("getPostSearch")]
        public async Task<IActionResult> GetPostSearch([FromQuery] ClientPostSearchRequest request, CancellationToken cancellationToken)
        {
            ClientPostSearchViewModel vm = new(_clientPostReadOnlyRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("getMajor")]
        public async Task<IActionResult> GetMajor([FromQuery] ClientPost_GetMajorRequestWithPagination request, CancellationToken cancellationToken)
        {
            ClientPost_ListMajorViewModel vm = new(_clientPostReadOnlyRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMajorRequest(InputMajorRequest request, CancellationToken cancellationToken)
        {
            ClienPostMajorRequestCreateViewModel vm = new(_clientPostReadOnlyRespository, _clientPostReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
    }
}
