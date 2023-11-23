using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Infrastructure.ViewModels.ClientPost;
using Microsoft.AspNetCore.Mvc;

namespace FPLSP_Tutorial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientPostsController : ControllerBase
    {
        private readonly IClientPostReadOnlyRespository _clientPostReadOnlyRespository;
        private readonly IClientPostReadWriteRespository _clientPostReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public ClientPostsController(IClientPostReadOnlyRespository clientPostReadOnlyRespository, IClientPostReadWriteRespository clientPostReadWriteRespository, IMapper mapper, ILocalizationService localizationService)
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
            if (vm.Success && vm.Data != null)
            {
                PostMainDTO result = (PostMainDTO)vm.Data;
                return Ok(result);
            }
            return Ok(vm);
        }
        [HttpGet("getChildPost")]
        public async Task<IActionResult> GetChildPosts([FromQuery] PostIdRequestWithPagination request, CancellationToken cancellationToken)
        {
            ChildPostViewModel vm = new(_clientPostReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                PaginationResponse<PostMainDTO> result = (PaginationResponse<PostMainDTO>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpGet("getPostDetail/{id}")]
        public async Task<IActionResult> GetPostDetail(Guid id, CancellationToken cancellationToken)
        {
            PostDetailViewModel vm = new(_clientPostReadOnlyRespository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            PostDetailDTO result = new();
            if (vm.Success)
            {
                result = (PostDetailDTO)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpGet("getPostByMajorId")]
        public async Task<IActionResult> GetPostsByMajorId([FromQuery] ClientPostListRequest request, CancellationToken cancellationToken)
        {
            PostMainViewModel vm = new(_clientPostReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            PaginationResponse<PostMainDTO> response = new();
            if (vm.Success)
            {
                response = (PaginationResponse<PostMainDTO>)vm.Data;
                return Ok(response);
            }
            return BadRequest(vm);
        }
        [HttpGet("getMajor")]
        public async Task<IActionResult> GetMajor([FromQuery] ClientPostMajorRequestWithPagination request, CancellationToken cancellationToken)
        {
            ClientPost_ListMajorViewModel vm = new(_clientPostReadOnlyRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            //APIResponse apiResponse = new();
            PaginationResponse<MajorBaseDTO> response = new();
            if (vm.Success)
            {
                response = (PaginationResponse<MajorBaseDTO>)vm.Data;
                return Ok(response);

            }
            return BadRequest(vm);

        }
        [HttpPost]
        public async Task<IActionResult> CreateMajorRequest(InputMajorRequest request, CancellationToken cancellationToken)
        {
            ClienPostMajorRequestCreateViewModel vm = new(_clientPostReadOnlyRespository, _clientPostReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("getPostTags")]
        public async Task<IActionResult> GetPostTagsByPostId([FromQuery] PostIdRequestWithPagination request, CancellationToken cancellationToken)
        {
            PostTagViewModel vm = new(_clientPostReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            PaginationResponse<TagBaseDTO> result = new();
            if (vm.Success)
            {
                result = (PaginationResponse<TagBaseDTO>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpGet("getMajorsByUserId")]
        public async Task<IActionResult> GetMajorsByUserId([FromQuery] GetMajorByUserIdWithPaginationRequest request, CancellationToken cancellationToken)
        {
            MajorByUserIdViewModel vm = new(_clientPostReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("getAllPostBySearch")]
        public async Task<IActionResult> GetPostBySearchAsync([FromQuery] ClientPostSearchWithPaginationRequest request, CancellationToken cancellationToken)
        {
            GetPostSearchWithPaginationViewModel vm = new(_clientPostReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                PaginationResponse<PostMainDTO> result = (PaginationResponse<PostMainDTO>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpGet("getMajor/{id}")]
        public async Task<IActionResult> GetMajorById(Guid id, CancellationToken cancellationToken)
        {
            ClientPostMajorViewModel vm = new(_clientPostReadOnlyRespository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            if (vm.Success)
            {
                MajorBaseDTO result = new();
                result = (MajorBaseDTO)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpGet("getAllListTags")]
        public async Task<IActionResult> GetAllListTags(CancellationToken cancellationToken)
        {
            ListTagsViewModel vm = new(_clientPostReadOnlyRespository, _localizationService);
            await vm.HandleAsync(1, cancellationToken);
            if (vm.Success)
            {
                List<TagBaseDTO> lstTags = (List<TagBaseDTO>)vm.Data;
                return Ok(lstTags);
            }
            return BadRequest(vm);
        }
    }
}
