using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.User.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Infrastructure.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace FPLSP_Tutorial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IUserReadOnlyRepository _userReadOnlyRepository;
        public readonly IUserReadWriteRepository _userReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        public UsersController(IUserReadOnlyRepository userReadOnlyRepository, IUserReadWriteRepository userReadWriteRepository
        , ILocalizationService localizationService, IMapper mapper)

        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _userReadWriteRepository = userReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }


        [HttpGet("GetUserByEmailAsync")]
        public async Task<IActionResult> GetUserByEmailAsync([FromQuery] string email, CancellationToken cToken)
        {
            UserGetByEmailViewModel vm = new(_localizationService, _userReadOnlyRepository);
            await vm.HandleAsync(email, cToken);
            return Ok(vm.Data);
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ViewUserWithPaginationRequest request, CancellationToken cancellationToken)
        {
            UserListWithPaginationViewModel vm = new(_userReadOnlyRepository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            UserViewModel vm = new(_userReadOnlyRepository, _localizationService);

            await vm.HandleAsync(id, cancellationToken);

            return Ok(vm);
        }



        [HttpPost]
        public async Task<IActionResult> Post(UserCreateRequest request, CancellationToken cancellationToken)
        {
            UserCreateViewModel vm = new(_userReadOnlyRepository, _userReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UserUpdateRequest request, CancellationToken cancellationToken)
        {
            UserUpdateViewModel vm = new(_userReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }


    }
}
