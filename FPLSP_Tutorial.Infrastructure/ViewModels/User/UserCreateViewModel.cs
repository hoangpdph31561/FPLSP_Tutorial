using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.User.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.User
{
    public class UserCreateViewModel : ViewModelBase<UserCreateRequest>
    {
        public readonly IUserReadOnlyRepository _userReadOnlyRepository;
        public readonly IUserReadWriteRepository _userReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public UserCreateViewModel(IUserReadOnlyRepository userReadOnlyRepository, IUserReadWriteRepository userReadWriteRepository
            , ILocalizationService localizationService, IMapper mapper)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _userReadWriteRepository = userReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(UserCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _userReadWriteRepository.AddUserAsync(_mapper.Map<UserEntity>(request), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _userReadOnlyRepository.GetUserByIdAsync(createResult.Data, cancellationToken);

                    Data = result.Data!;
                    Success = result.Success;
                    ErrorItems = result.Errors;
                    Message = result.Message;
                    return;
                }

                Success = createResult.Success;
                ErrorItems = createResult.Errors;
                Message = createResult.Message;
            }
            catch (Exception)
            {
                Success = false;
                ErrorItems = new[]
                    {
                    new ErrorItem
                    {
                        Error = _localizationService["Error occurred while getting the User"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "User")
                    }
                };
            }
        }
    }
}
