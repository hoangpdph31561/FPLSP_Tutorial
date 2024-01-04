using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.Tag;

public class TagListViewModel : ViewModelBase<TagViewRequest>
{
    private readonly ILocalizationService _localizationService;
    private readonly ITagReadOnlyRepository _tagReadOnlyRepository;

    public TagListViewModel(ITagReadOnlyRepository tagReadOnlyRepository, ILocalizationService localizationService)
    {
        _tagReadOnlyRepository = tagReadOnlyRepository;
        _localizationService = localizationService;
    }

    public override async Task HandleAsync(TagViewRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _tagReadOnlyRepository.GetTagAsync(request, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the tag"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Tag")
                }
            };
        }
    }
}