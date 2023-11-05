using AutoMapper;
using Azure.Core;
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
        [HttpGet("getParentPost/{id}")]
        public async Task<IActionResult> GetParentPost(Guid id, CancellationToken cancellationToken)
        {
            PostParentViewModel vm = new(_clientPostReadOnlyRespository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("getChildPost")]
        public async Task<IActionResult> GetChildPosts([FromQuery] PostIdRequestWithPagination request, CancellationToken cancellationToken)
        {
            ChildPostViewModel vm = new(_clientPostReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("getPostDetail/{id}")]
        public async Task<IActionResult> GetPostDetail(Guid id, CancellationToken cancellationToken)
        {
            PostDetailViewModel vm = new(_clientPostReadOnlyRespository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("getPostByMajorId")]
        public async Task<IActionResult> GetPostsByMajorId([FromQuery] ClientPostListRequest request, CancellationToken cancellationToken)
        {
            PostMainViewModel vm = new(_clientPostReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("getMajor")]
        public async Task<IActionResult> GetMajor([FromQuery] ClientPostMajorRequestWithPagination request, CancellationToken cancellationToken)
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
        [HttpGet("getPostTags")]
        public async Task<IActionResult> GetPostTagsByPostId([FromQuery]PostIdRequestWithPagination request, CancellationToken cancellationToken)
        {
            PostTagViewModel vm = new(_clientPostReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("getMajorsByUserId")]
        public async Task<IActionResult> GetMajorsByUserId([FromQuery] GetMajorByUserIdWithPaginationRequest request, CancellationToken cancellationToken)
        {
            MajorByUserIdViewModel vm = new(_clientPostReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
    }
}
