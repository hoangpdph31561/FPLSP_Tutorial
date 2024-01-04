using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.Tag;

public class TagViewModel : ViewModelBase<Guid>
{
    private readonly ILocalizationService _localizationService;
    private readonly ITagReadOnlyRepository _tagReadOnlyRepository;

    public TagViewModel(ITagReadOnlyRepository tagReadOnlyRepository, ILocalizationService localizationService)
    {
        _tagReadOnlyRepository = tagReadOnlyRepository;
        _localizationService = localizationService;
    }

    public override async Task HandleAsync(Guid idTag, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _tagReadOnlyRepository.GetTagByIdAsync(idTag, cancellationToken);

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