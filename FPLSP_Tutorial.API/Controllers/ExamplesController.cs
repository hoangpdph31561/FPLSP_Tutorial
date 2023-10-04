using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Example.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Infrastructure.ViewModels.News;
using Microsoft.AspNetCore.Mvc;

namespace FPLSP_Tutorial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamplesController : ControllerBase
    {
        public readonly IExampleReadOnlyRepository _exampleReadOnlyRepository;
        public readonly IExampleReadWriteRepository _exampleReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public ExamplesController(IExampleReadOnlyRepository exampleReadOnlyRepository, IExampleReadWriteRepository exampleReadWriteRepository, IConfiguration configuration, ILocalizationService localizationService, IMapper mapper)
        {
            _exampleReadOnlyRepository = exampleReadOnlyRepository;
            _exampleReadWriteRepository = exampleReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        // GET api/<ExampleController>/5
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ViewExampleWithPaginationRequest request, CancellationToken cancellationToken)
        {
            ExampleListWithPaginationViewModel vm = new(_exampleReadOnlyRepository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        // GET api/<ExampleController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            ExampleViewModel vm = new(_exampleReadOnlyRepository, _localizationService);

            await vm.HandleAsync(id, cancellationToken);

            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ExampleCreateRequest request, CancellationToken cancellationToken)
        {
            ExampleCreateViewModel vm = new(_exampleReadOnlyRepository, _exampleReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ExampleUpdateRequest request, CancellationToken cancellationToken)
        {
            ExampleUpdateViewModel vm = new(_exampleReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(ExampleDeleteRequest request, CancellationToken cancellationToken)
        {
            ExampleDeleteViewModel vm = new(_exampleReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }
    }
}