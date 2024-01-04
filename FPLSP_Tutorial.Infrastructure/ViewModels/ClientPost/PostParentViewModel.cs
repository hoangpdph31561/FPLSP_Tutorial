using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.ClientPost;

public class PostParentViewModel : ViewModelBase<Guid>
{
    private readonly IClientPostReadOnlyRespository _clientPostReadRespository;
    private readonly ILocalizationService _localizationService;

    public PostParentViewModel(IClientPostReadOnlyRespository clientPostReadOnlyRespository,
        ILocalizationService localizationService)
    {
        _clientPostReadRespository = clientPostReadOnlyRespository;
        _localizationService = localizationService;
    }

    public override async Task HandleAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _clientPostReadRespository.GetParentPostByPostIdAsync(id, cancellationToken);
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
                    Error = _localizationService["Error occurred while getting parent post"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "parent post")
                }
            };
        }
    }
}