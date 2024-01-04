using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.UserMajor;
using FPLSP_Tutorial.Application.DataTransferObjects.UserMajor.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Infrastructure.ViewModels.UserMajors;
using Microsoft.AspNetCore.Mvc;

namespace FPLSP_Tutorial.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserMajorsController : ControllerBase
{
    private readonly ILocalizationService _localizationService;
    private readonly IUserMajorReadWriteRepository _majorUserReadWriteRespository;
    private readonly IMapper _mapper;
    private readonly IUserMajorReadOnlyRepository _userMajorReadOnlyRespository;

    public UserMajorsController(IUserMajorReadWriteRepository majorUserReadWriteRespository,
        IUserMajorReadOnlyRepository userMajorReadOnlyRespository, ILocalizationService localizationService,
        IMapper mapper)
    {
        _localizationService = localizationService;
        _mapper = mapper;
        _majorUserReadWriteRespository = majorUserReadWriteRespository;
        _userMajorReadOnlyRespository = userMajorReadOnlyRespository;
    }

    [HttpGet("GetListAsync")]
    public async Task<IActionResult> GetListAsync([FromQuery] UserMajorViewRequest request,
        CancellationToken cancellationToken)
    {
        UserMajorListViewModel vm = new(_userMajorReadOnlyRespository, _localizationService);

        await vm.HandleAsync(request, cancellationToken);
        if (vm.Success)
        {
            var paginationResponse = new PaginationResponse<UserMajorDTO>();
            paginationResponse = (PaginationResponse<UserMajorDTO>)vm.Data;
            return Ok(paginationResponse);
        }

        return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] UserMajorCreateRequest request,
        CancellationToken cancellationToken)
    {
        UserMajorCreateViewModel vm = new(_majorUserReadWriteRespository, _localizationService, _mapper);
        await vm.HandleAsync(request, cancellationToken);
        return Ok(vm);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UserMajorUpdateRequest request, CancellationToken cancellationToken)
    {
        UserMajorUpdateViewModel vm = new(_majorUserReadWriteRespository, _localizationService, _mapper);

        await vm.HandleAsync(request, cancellationToken);

        return Ok(vm);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromQuery] UserMajorDeleteRequest request,
        CancellationToken cancellationToken)
    {
        UserMajorDeleteViewModel vm = new(_mapper, _majorUserReadWriteRespository, _localizationService);
        await vm.HandleAsync(request, cancellationToken);
        return Ok(vm);
    }
}