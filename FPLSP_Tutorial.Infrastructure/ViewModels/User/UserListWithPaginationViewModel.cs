using FPLSP_Tutorial.Application.DataTransferObjects.User.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.User
{
    public class UserListWithPaginationViewModel : ViewModelBase<ViewUserWithPaginationRequest>
    {
        public readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly ILocalizationService _localizationService;
        public UserListWithPaginationViewModel(IUserReadOnlyRepository UserReadOnlyRepository, ILocalizationService localizationService)
        {
            _userReadOnlyRepository = UserReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(ViewUserWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userReadOnlyRepository.GetUserWithPaginationByAdminAsync(request, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of User"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of User")
                }
            };
            }
        }
    }
}
