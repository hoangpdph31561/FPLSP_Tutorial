using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.ClientPost;

public class ListTagsViewModel : ViewModelBase<int>
{
    private readonly IClientPostReadOnlyRespository _clientPostReadRespository;
    private readonly ILocalizationService _localizationService;

    public ListTagsViewModel(IClientPostReadOnlyRespository clientPostReadOnlyRespository,
        ILocalizationService localizationService)
    {
        _clientPostReadRespository = clientPostReadOnlyRespository;
        _localizationService = localizationService;
    }

    public override async Task HandleAsync(int data, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _clientPostReadRespository.GetAllTagList(cancellationToken);
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
                    Error = _localizationService["Error occurred while getting the list of tags by UserId"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of tags by UserId")
                }
            };
        }
    }
}