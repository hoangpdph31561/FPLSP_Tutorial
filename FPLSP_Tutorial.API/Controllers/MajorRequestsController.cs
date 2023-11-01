using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
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

        // GET api/<ExampleController>/5
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ViewMajorRequestWithPaginationRequest request, CancellationToken cancellationToken)
        {
            MajorRequestListWithPaginationViewModel vm = new(_majorRequestReadOnlyRespository, _localizationService);
            vm = new(_majorRequestReadOnlyRespository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        // GET api/<ExampleController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            MajorRequestViewModels vm = new(_majorRequestReadOnlyRespository, _localizationService);

            await vm.HandleAsync(id, cancellationToken);

            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Post(MajorRequestCreateRequest request, CancellationToken cancellationToken)
        {
            MajorRequestCreateViewModel vm = new(_majorRequestReadOnlyRespository, _majorRequestReadWriteRespository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        /// <summary>
        /// Tao demo call api yeu cau cap quyen chu bo mon 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<string> Post(MajorRequestCreateRequest request, CancellationToken cancellationToken)
        //{
        //    MajorRequestCreateViewModel vm = new(_majorRequestReadOnlyRespository, _majorRequestReadWriteRespository, _localizationService, _mapper);

        //    await vm.HandleAsync(request, cancellationToken);

        //    return "Thanh cong";
        //}

        [HttpPut]
        public async Task<IActionResult> Put(MajorRequestUpdateRequest request, CancellationToken cancellationToken)
        {
            MajorRequestUpDateViewModel vm = new(_majorRequestReadWriteRespository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(MajorRequestDeleteRequest request, CancellationToken cancellationToken)
        {
            MajorRequestDeleteViewModel vm = new(_majorRequestReadWriteRespository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }
    }
}
