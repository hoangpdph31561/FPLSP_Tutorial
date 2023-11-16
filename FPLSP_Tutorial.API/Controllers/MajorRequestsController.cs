using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Infrastructure.ViewModels.MajorRequests;
using Microsoft.AspNetCore.Mvc;

namespace FPLSP_Tutorial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajorRequestsController : ControllerBase
    {
        public readonly IMajorRequestReadOnlyRespository _majorRequestReadOnlyRespository;
        public readonly IMajorRequestReadWriteRespository _majorRequestReadWriteRespository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public MajorRequestsController(IMajorRequestReadOnlyRespository majorRequestReadOnlyRespository, IMajorRequestReadWriteRespository majorRequestReadWriteRespository, IConfiguration configuration, ILocalizationService localizationService, IMapper mapper)
        {
            _majorRequestReadOnlyRespository = majorRequestReadOnlyRespository;
            _majorRequestReadWriteRespository = majorRequestReadWriteRespository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ViewMajorRequestWithPaginationRequest request, CancellationToken cancellationToken)
        {
            MajorRequestListWithPaginationViewModel vm = new(_majorRequestReadOnlyRespository, _localizationService);
            vm = new(_majorRequestReadOnlyRespository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }
        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        //{
        //    MajorRequestViewModels vm = new(_majorRequestReadOnlyRespository, _localizationService);

        //    await vm.HandleAsync(id, cancellationToken);

        //    return Ok(vm);
        //}
        [HttpGet("majorRequestNotDeleted")] // lấy ra cá majorRequest chưa bị xóa
        public async Task<IActionResult> GetMajorRequest([FromQuery] ViewMajorRequestWithPaginationRequest request, CancellationToken cancellationToken)
        {
            MajorRequestListWithPaginationNotDeletedViewModels vm = new(_majorRequestReadOnlyRespository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                PaginationResponse<MajorRequestDto> paginationResponse = new PaginationResponse<MajorRequestDto>();
                paginationResponse = (PaginationResponse<MajorRequestDto>)vm.Data;
                return Ok(paginationResponse);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] MajorRequestCreateRequest request, CancellationToken cancellationToken)
        {
            MajorRequestCreateViewModel vm = new(_majorRequestReadOnlyRespository, _majorRequestReadWriteRespository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromQuery] MajorRequestUpdateRequest request, CancellationToken cancellationToken)
        {
            MajorRequestUpDateViewModel vm = new(_majorRequestReadWriteRespository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] MajorRequestDeleteRequest request, CancellationToken cancellationToken)
        {
            MajorRequestDeleteViewModel vm = new(_majorRequestReadWriteRespository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }
    }
}
