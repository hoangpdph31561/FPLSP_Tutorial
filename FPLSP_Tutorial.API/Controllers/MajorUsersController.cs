using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadOnly;
using FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadWrite;
using FPLSP_Tutorial.Infrastructure.ViewModels.Posts;
using FPLSP_Tutorial.Infrastructure.ViewModels.UserMajors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FPLSP_Tutorial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajorUsersController : ControllerBase
    {
        private readonly IMajorUserReadWriteResponsitory _majorUserReadWriteRespository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        public MajorUsersController(IMajorUserReadWriteResponsitory majorUserReadWriteRespository, ILocalizationService localizationService, IMapper mapper)
        {
            _localizationService = localizationService;
            _mapper = mapper;
            _majorUserReadWriteRespository = majorUserReadWriteRespository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewMajorRequest(CreateUserMajorRequest request, CancellationToken cancellationToken)
        {
            MajorUserCreateViewModel vm = new(_majorUserReadWriteRespository, _localizationService, _mapper);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMajorRequest(DeleteMajorUserRequest request, CancellationToken cancellationToken)
        {
            MajorUserDeleteViewModel vm = new(_mapper,_majorUserReadWriteRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
    }
}
