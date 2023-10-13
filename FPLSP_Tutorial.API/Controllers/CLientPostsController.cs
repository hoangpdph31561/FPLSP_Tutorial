using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ClientPostReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Infrastructure.ViewModels.ClientPost;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FPLSP_Tutorial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLientPostsController : ControllerBase
    {
        private readonly IClientPostReadOnlyRespository _clientPostReadOnlyRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public CLientPostsController(IClientPostReadOnlyRespository clientPostReadOnlyRespository, IMapper mapper, ILocalizationService localizationService)
        {
            _clientPostReadOnlyRespository = clientPostReadOnlyRespository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(Guid id, CancellationToken cancellationToken)
        {
            ClientPostDetailViewModel vm = new(_clientPostReadOnlyRespository,_mapper, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("getByMajorId")]
        public async Task<IActionResult> GetPostByMajorId([FromQuery] ClientPostListRequest request, CancellationToken cancellationToken)
        {
            ClientPostGetByMajorIdViewModel vm = new(_clientPostReadOnlyRespository, _mapper, _localizationService);
            await vm.HandleAsync(request,cancellationToken);
            return Ok(vm);
        }
        [HttpGet("getPostTag")]
        public async Task<IActionResult> GetPostTag([FromQuery] ClientPostRequestIdWithPagination request, CancellationToken cancellationToken)
        {
            ClientPostGetPostTagViewModel vm = new (_clientPostReadOnlyRespository,_mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
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
        public async Task<IActionResult> GetPostSearch([FromQuery] ClientPostSearchRequest request,  CancellationToken cancellationToken)
        {
            ClientPostSearchViewModel vm = new(_clientPostReadOnlyRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
    }
}
