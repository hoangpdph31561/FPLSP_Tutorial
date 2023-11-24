using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.UserMajors
{
    public class MajorUserListViewModel : ViewModelBase<UserMajorViewRequest>
    {
        public readonly IUserMajorReadOnlyRepository _userMajorReadOnlyRespository;
        private readonly ILocalizationService _localizationService;

        public MajorUserListViewModel(IUserMajorReadOnlyRepository userMajorReadOnlyRespository, ILocalizationService localizationService)
        {
            _userMajorReadOnlyRespository = userMajorReadOnlyRespository;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(UserMajorViewRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userMajorReadOnlyRespository.GetMajorUserAsync(data, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of MajorUser"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of MajorUser")
                }
            };
            }
        }
    }
}
