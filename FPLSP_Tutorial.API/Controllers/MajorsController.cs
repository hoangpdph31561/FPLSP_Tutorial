using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Major;
using FPLSP_Tutorial.Application.DataTransferObjects.Major.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Infrastructure.ViewModels.Major;
using Microsoft.AspNetCore.Mvc;

namespace FPLSP_Tutorial.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MajorsController : ControllerBase
{
    private readonly ILocalizationService _localizationService;
    public readonly IMajorReadOnlyRepository _majorReadOnlyRepository;
    public readonly IMajorReadWriteRepository _majorReadWriteRepository;
    private readonly IMapper _mapper;

    public MajorsController(IMajorReadOnlyRepository majorReadOnlyRepository,
        IMajorReadWriteRepository majorReadWriteRepository
        , ILocalizationService localizationService, IMapper mapper)

    {
        _majorReadOnlyRepository = majorReadOnlyRepository;
        _majorReadWriteRepository = majorReadWriteRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    [HttpGet("GetListAsync")]
    public async Task<IActionResult> GetListAsync([FromQuery] MajorViewRequest request,
        CancellationToken cancellationToken)
    {
        MajorListViewModel vm = new(_majorReadOnlyRepository, _localizationService);

        await vm.HandleAsync(request, cancellationToken);

        if (vm.Success)
        {
            List<MajorDTO> result = new();
            result = (List<MajorDTO>)vm.Data;
            return Ok(result);
        }

        return BadRequest(vm);
    }

    [HttpGet("GetListWithPaginationAsync")]
    public async Task<IActionResult> GetListWithPagination([FromQuery] MajorViewWithPaginationRequest request,
        CancellationToken cancellationToken)
    {
        MajorListWithPaginationViewModel vm = new(_majorReadOnlyRepository, _localizationService);

        await vm.HandleAsync(request, cancellationToken);

        if (vm.Success)
        {
            PaginationResponse<MajorDTO> result = new();
            result = (PaginationResponse<MajorDTO>)vm.Data;
            return Ok(result);
        }

        return BadRequest(vm);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        MajorViewModel vm = new(_majorReadOnlyRepository, _localizationService);

        await vm.HandleAsync(id, cancellationToken);
        if (vm.Success)
        {
            var result = (MajorDTO)vm.Data;
            return Ok(result);
        }

        return BadRequest(vm);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(MajorCreateRequest request, CancellationToken cancellationToken)
    {
        MajorCreateViewModel vm = new(_majorReadOnlyRepository, _majorReadWriteRepository, _localizationService,
            _mapper);

        await vm.HandleAsync(request, cancellationToken);

        return Ok(vm);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(MajorUpdateRequest request, CancellationToken cancellationToken)
    {
        MajorUpdateViewModel vm = new(_majorReadWriteRepository, _localizationService, _mapper);

        await vm.HandleAsync(request, cancellationToken);

        return Ok(vm);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromQuery] MajorDeleteRequest request,
        CancellationToken cancellationToken)
    {
        MajorDeleteViewModel vm = new(_majorReadWriteRepository, _localizationService, _mapper);

        await vm.HandleAsync(request, cancellationToken);

        return Ok(vm);
    }
}