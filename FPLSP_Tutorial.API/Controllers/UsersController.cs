using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.User;
using FPLSP_Tutorial.Application.DataTransferObjects.User.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Infrastructure.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace FPLSP_Tutorial.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;
    public readonly IUserReadOnlyRepository _userReadOnlyRepository;
    public readonly IUserReadWriteRepository _userReadWriteRepository;

    public UsersController(IUserReadOnlyRepository userReadOnlyRepository,
        IUserReadWriteRepository userReadWriteRepository
        , ILocalizationService localizationService, IMapper mapper)

    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _userReadWriteRepository = userReadWriteRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    [HttpGet("GetListWithPagination")]
    public async Task<IActionResult> GetListWithPaginationAsync([FromQuery] UserViewWithPaginationRequest request,
        CancellationToken cancellationToken)
    {
        UserListWithPaginationViewModel vm = new(_userReadOnlyRepository, _localizationService);

        await vm.HandleAsync(request, cancellationToken);
        if (vm.Success)
        {
            var paginationResponse = new PaginationResponse<UserDTO>();
            paginationResponse = (PaginationResponse<UserDTO>)vm.Data;
            return Ok(paginationResponse);
        }

        return Ok(vm);
    }

    [HttpGet("GetByEmailAsync")]
    public async Task<IActionResult> GetByEmailAsync([FromQuery] string email, CancellationToken cToken)
    {
        UserViewByEmailViewModel vm = new(_localizationService, _userReadOnlyRepository);
        await vm.HandleAsync(email, cToken);
        return Ok(vm.Data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        UserViewModel vm = new(_userReadOnlyRepository, _localizationService);

        await vm.HandleAsync(id, cancellationToken);

        return Ok(vm);
    }


    [HttpPost]
    public async Task<IActionResult> AddAsync(UserCreateRequest request, CancellationToken cancellationToken)
    {
        UserCreateViewModel vm = new(_userReadOnlyRepository, _userReadWriteRepository, _localizationService, _mapper);

        await vm.HandleAsync(request, cancellationToken);

        return Ok(vm);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UserUpdateRequest request, CancellationToken cancellationToken)
    {
        UserUpdateViewModel vm = new(_userReadWriteRepository, _localizationService, _mapper);

        await vm.HandleAsync(request, cancellationToken);

        return Ok(vm);
    }
}