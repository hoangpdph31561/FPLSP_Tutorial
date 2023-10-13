using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.User
{
    public class UserViewModel : ViewModelBase<Guid>
    {
        public readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly ILocalizationService _localizationService;
        public UserViewModel(IUserReadOnlyRepository userReadOnlyRepository, ILocalizationService localizationService)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(Guid idUser, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userReadOnlyRepository.GetUserByIdAsync(idUser, cancellationToken);

                Data = result.Data!;
                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
                return;
            }
            catch
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
