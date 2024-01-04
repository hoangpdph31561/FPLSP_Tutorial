using FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.Posts;

public class PostListViewModel : ViewModelBase<PostViewRequest>
{
    private readonly ILocalizationService _localizationService;
    private readonly IPostReadOnlyRespository _postReadOnlyRespository;

    public PostListViewModel(IPostReadOnlyRespository postReadOnlyRespository, ILocalizationService localizationService)
    {
        _postReadOnlyRespository = postReadOnlyRespository;
        _localizationService = localizationService;
    }

    public override async Task HandleAsync(PostViewRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _postReadOnlyRespository.GetPostAsync(request, cancellationToken);
            Data = result.Data!;
            Success = result.Success;
            ErrorItems = result.Errors;
            Message = result.Message;
        }
        catch (Exception)
        {
            Success = false;
            ErrorItems = new[]
            {
                new ErrorItem
                {
                    Error = _localizationService["Error occurred while getting the list of Example"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of Example")
                }
            };
        }
    }
}