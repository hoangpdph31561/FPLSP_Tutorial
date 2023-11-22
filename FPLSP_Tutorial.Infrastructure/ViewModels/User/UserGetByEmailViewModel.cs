using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.User
{
    public class UserGetByEmailViewModel : ViewModelBase<string>
    {
        private readonly IUserReadOnlyRepository _repoUserReadOnly;
        private readonly ILocalizationService _svLocalization;

        public UserGetByEmailViewModel(ILocalizationService svLocalization, IUserReadOnlyRepository repoUserReadOnly)
        {
            _svLocalization = svLocalization;
            _repoUserReadOnly = repoUserReadOnly;
        }
        public override async Task HandleAsync(string email, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repoUserReadOnly.GetUserByEmailAsync(email, cancellationToken);

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
                    Error = _svLocalization["Error occurred while getting the User"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "User")
                }
            };
            }
        }
    }
}
