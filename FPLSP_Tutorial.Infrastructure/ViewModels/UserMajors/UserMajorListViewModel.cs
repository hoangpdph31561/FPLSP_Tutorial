using FPLSP_Tutorial.Application.DataTransferObjects.UserMajor.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.UserMajors;

public class UserMajorListViewModel : ViewModelBase<UserMajorViewRequest>
{
    private readonly ILocalizationService _localizationService;
    public readonly IUserMajorReadOnlyRepository _userMajorReadOnlyRespository;

    public UserMajorListViewModel(IUserMajorReadOnlyRepository userMajorReadOnlyRespository,
        ILocalizationService localizationService)
    {
        _userMajorReadOnlyRespository = userMajorReadOnlyRespository;
        _localizationService = localizationService;
    }

    public override async Task HandleAsync(UserMajorViewRequest data, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _userMajorReadOnlyRespository.GetMajorUserAsync(data, cancellationToken);

            Data = result.Data!;
            Success = result.Success;
            ErrorItems = result.Errors;
            Message = result.Message;
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