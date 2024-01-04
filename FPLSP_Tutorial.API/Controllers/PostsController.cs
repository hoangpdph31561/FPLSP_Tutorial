using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Post;
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Infrastructure.ViewModels.Posts;
using Microsoft.AspNetCore.Mvc;

namespace FPLSP_Tutorial.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;
    private readonly IPostReadOnlyRespository _postReadOnlyRespository;
    private readonly IPostReadWriteRepository _postReadWriteRespository;

    public PostsController(IPostReadOnlyRespository postReadOnlyRespository,
        IPostReadWriteRepository postReadWriteRespository, ILocalizationService localizationService, IMapper mapper)
    {
        _localizationService = localizationService;
        _mapper = mapper;
        _postReadOnlyRespository = postReadOnlyRespository;
        _postReadWriteRespository = postReadWriteRespository;
    }

    [HttpGet("GetListAsync")]
    public async Task<IActionResult> GetList([FromQuery] PostViewRequest request, CancellationToken cancellationToken)
    {
        PostListViewModel vm = new(_postReadOnlyRespository, _localizationService);
        await vm.HandleAsync(request, cancellationToken);
        return Ok(vm.Data);
    }

    [HttpGet("GetListWithPaginationAsync")]
    public async Task<IActionResult> GetListWithPaginationAsync([FromQuery] PostViewWithPaginationRequest request,
        CancellationToken cancellationToken)
    {
        PostListWithPaginationViewModel vm = new(_postReadOnlyRespository, _localizationService);
        await vm.HandleAsync(request, cancellationToken);
        if (vm.Success)
        {
            var result = (PaginationResponse<PostDTO>)vm.Data;
            return Ok(result);
        }

        return BadRequest(vm);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(Guid id, CancellationToken cancellationToken)
    {
        PostViewModel vm = new(_postReadOnlyRespository, _localizationService);
        await vm.HandleAsync(id, cancellationToken);
        if (vm.Success)
        {
            var result = (PostDTO)vm.Data;
            return Ok(result);
        }

        return BadRequest(vm);
    }


    [HttpPost]
    public async Task<IActionResult> AddAsync(PostCreateRequest request, CancellationToken cancellationToken)
    {
        PostCreateViewModel vm = new(_postReadOnlyRespository, _postReadWriteRespository, _localizationService,
            _mapper);
        await vm.HandleAsync(request, cancellationToken);
        if (vm.Success)
        {
            var result = (PostDTO)vm.Data;
            return Ok(result);
        }

        return BadRequest(vm);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(PostUpdateRequest request, CancellationToken cancellationToken)
    {
        PostUpdateViewModel vm = new(_postReadWriteRespository, _localizationService, _mapper);
        await vm.HandleAsync(request, cancellationToken);
        return Ok(vm);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromQuery] PostDeleteRequest request,
        CancellationToken cancellationToken)
    {
        PostDeleteViewModel vm = new(_postReadWriteRespository, _localizationService);
        await vm.HandleAsync(request, cancellationToken);
        return Ok(vm);
    }
}