using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.ClientPost;

public class PostMainViewModel : ViewModelBase<ClientPostListRequest>
{
    private readonly IClientPostReadOnlyRespository _clientPostReadRespository;
    private readonly ILocalizationService _localizationService;

    public PostMainViewModel(IClientPostReadOnlyRespository clientPostReadOnlyRespository,
        ILocalizationService localizationService)
    {
        _clientPostReadRespository = clientPostReadOnlyRespository;
        _localizationService = localizationService;
    }

    public override async Task HandleAsync(ClientPostListRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _clientPostReadRespository.GetAllPostByMajorAsync(request, cancellationToken);
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
                    Error = _localizationService["Error occurred while getting the list of post by MajorId"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of post by MajorId")
                }
            };
        }
    }
}