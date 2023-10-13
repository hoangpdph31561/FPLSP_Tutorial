using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.User.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.User
{
    public class UserDeleteViewModel : ViewModelBase<UserDeleteRequest>
    {
        public readonly IUserReadWriteRepository _userReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public UserDeleteViewModel(IUserReadWriteRepository userReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _userReadWriteRepository = userReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(UserDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userReadWriteRepository.DeleteUserAsync(request, cancellationToken);

                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
                return;
            }
            catch (Exception)
            {
                Success = false;
                ErrorItems = new[]
                    {
                    new ErrorItem
                    {
                        Error = _localizationService["Error occurred while updating the User"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "User")
                    }
                };
            }
        }
    }
}
