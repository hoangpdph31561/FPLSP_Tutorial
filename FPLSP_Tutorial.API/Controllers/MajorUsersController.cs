using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadOnly;
using FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadWrite;
using FPLSP_Tutorial.Infrastructure.ViewModels.MajorRequests;
using FPLSP_Tutorial.Infrastructure.ViewModels.Posts;
using FPLSP_Tutorial.Infrastructure.ViewModels.UserMajors;
using Microsoft.AspNetCore.Mvc;

namespace FPLSP_Tutorial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajorUsersController : ControllerBase
    {
        private readonly IMajorUserReadWriteResponsitory _majorUserReadWriteRespository;
        private readonly IUserMajorReadOnlyRespository _userMajorReadOnlyRespository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        public MajorUsersController(IMajorUserReadWriteResponsitory majorUserReadWriteRespository, IUserMajorReadOnlyRespository userMajorReadOnlyRespository, ILocalizationService localizationService, IMapper mapper)
        {
            _localizationService = localizationService;
            _mapper = mapper;
            _majorUserReadWriteRespository = majorUserReadWriteRespository;
            _userMajorReadOnlyRespository = userMajorReadOnlyRespository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ViewMajorUserWithPaginationRequest request, CancellationToken cancellationToken)
        {
            MajorUserListWithPaginationViewModel vm = new(_userMajorReadOnlyRespository, _localizationService);
            vm = new(_userMajorReadOnlyRespository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                PaginationResponse<MajorUserDto> paginationResponse = new PaginationResponse<MajorUserDto>();
                paginationResponse = (PaginationResponse<MajorUserDto>)vm.Data;
                return Ok(paginationResponse);
            }
            return BadRequest();
        }
         [HttpGet("GetBySearch")]
        public async Task<IActionResult> GetBySearch([FromQuery] ViewMajorUserBySearchRequest request, CancellationToken cancellationToken)
        {
            MajorUserSearchByEmailViewModel vm = new(_userMajorReadOnlyRespository, _localizationService);
            vm = new(_userMajorReadOnlyRespository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                PaginationResponse<MajorUserDto> paginationResponse = new PaginationResponse<MajorUserDto>();
                paginationResponse = (PaginationResponse<MajorUserDto>)vm.Data;
                return Ok(paginationResponse);
            }
            return BadRequest();
        }
         [HttpGet("GetBySearchMajor")]
        public async Task<IActionResult> GetBySearchMajor([FromQuery] ViewMajorUserBySearchRequest request, CancellationToken cancellationToken)
        {
            MajorUserBySearchMajorViewModel vm = new(_userMajorReadOnlyRespository, _localizationService);
            vm = new(_userMajorReadOnlyRespository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                PaginationResponse<MajorUserDto> paginationResponse = new PaginationResponse<MajorUserDto>();
                paginationResponse = (PaginationResponse<MajorUserDto>)vm.Data;
                return Ok(paginationResponse);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewMajorUserRequest([FromBody] CreateUserMajorRequest request, CancellationToken cancellationToken)
        {
            MajorUserCreateViewModel vm = new(_majorUserReadWriteRespository, _localizationService, _mapper);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMajorUserRequest([FromQuery] DeleteMajorUserRequest request, CancellationToken cancellationToken)
        {
            MajorUserDeleteViewModel vm = new(_mapper, _majorUserReadWriteRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromQuery] UpdateMajorUserRequest request, CancellationToken cancellationToken)
        {
            MajorUserUpdateViewModel vm = new(_majorUserReadWriteRespository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }
    }
}
