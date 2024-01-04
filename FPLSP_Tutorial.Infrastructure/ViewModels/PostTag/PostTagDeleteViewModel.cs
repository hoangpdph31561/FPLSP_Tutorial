using FPLSP_Tutorial.Application.DataTransferObjects.PostTag.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.PostTag;

public class PostTagDeleteViewModel : ViewModelBase<PostTagDeleteRequest>
{
    private readonly ILocalizationService _localizationService;
    public readonly IPostTagReadWriteRepository _postTagReadWriteRespository;

    public PostTagDeleteViewModel(IPostTagReadWriteRepository postTagReadWriteRespository,
        ILocalizationService localizationService)
    {
        _postTagReadWriteRespository = postTagReadWriteRespository;
        _localizationService = localizationService;
    }

    public override async Task HandleAsync(PostTagDeleteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _postTagReadWriteRespository.DeleteAsync(request, cancellationToken);

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
                    Error = _localizationService["Error occurred while Delete the posttag"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "PostTag")
                }
            };
        }
    }
}