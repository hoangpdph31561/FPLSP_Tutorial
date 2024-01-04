using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Infrastructure.ViewModels.MajorRequests;
using Microsoft.AspNetCore.Mvc;

namespace FPLSP_Tutorial.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MajorRequestsController : ControllerBase
{
    private readonly ILocalizationService _localizationService;
    public readonly IMajorRequestReadOnlyRepository _majorRequestReadOnlyRespository;
    public readonly IMajorRequestReadWriteRepository _majorRequestReadWriteRespository;
    private readonly IMapper _mapper;

    public MajorRequestsController(IMajorRequestReadOnlyRepository majorRequestReadOnlyRespository,
        IMajorRequestReadWriteRepository majorRequestReadWriteRespository, IConfiguration configuration,
        ILocalizationService localizationService, IMapper mapper)
    {
        _majorRequestReadOnlyRespository = majorRequestReadOnlyRespository;
        _majorRequestReadWriteRespository = majorRequestReadWriteRespository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    [HttpGet("GetListWithPagination")]
    public async Task<IActionResult> GetListWithPagination([FromQuery] MajorRequestViewWithPaginationRequest request,
        CancellationToken cancellationToken)
    {
        MajorRequestListWithPaginationViewModel vm = new(_majorRequestReadOnlyRespository, _localizationService);

        await vm.HandleAsync(request, cancellationToken);
        if (vm.Success)
        {
            var paginationResponse = new PaginationResponse<MajorRequestDTO>();
            paginationResponse = (PaginationResponse<MajorRequestDTO>)vm.Data;
            return Ok(paginationResponse);
        }

        return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(MajorRequestCreateRequest request, CancellationToken cancellationToken)
    {
        MajorRequestCreateViewModel vm = new(_majorRequestReadOnlyRespository, _majorRequestReadWriteRespository,
            _localizationService, _mapper);

        await vm.HandleAsync(request, cancellationToken);

        return Ok(vm);
    }


    [HttpPut]
    public async Task<IActionResult> UpdateAsync(MajorRequestUpdateRequest request, CancellationToken cancellationToken)
    {
        MajorRequestUpdateViewModel vm = new(_majorRequestReadWriteRespository, _localizationService, _mapper);

        await vm.HandleAsync(request, cancellationToken);

        return Ok(vm);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromQuery] MajorRequestDeleteRequest request,
        CancellationToken cancellationToken)
    {
        MajorRequestDeleteViewModel vm = new(_majorRequestReadWriteRespository, _localizationService, _mapper);

        await vm.HandleAsync(request, cancellationToken);

        return Ok(vm);
    }
}