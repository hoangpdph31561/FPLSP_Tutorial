using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.Tag;

public class TagDeleteViewModel : ViewModelBase<TagDeleteRequest>
{
    private readonly ILocalizationService _localizationService;
    public readonly ITagReadWriteRepository _tagReadWriteRepository;

    public TagDeleteViewModel(ITagReadWriteRepository tagReadWriteRepository, ILocalizationService localizationService)
    {
        _tagReadWriteRepository = tagReadWriteRepository;
        _localizationService = localizationService;
    }

    public override async Task HandleAsync(TagDeleteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _tagReadWriteRepository.DeleteTagAsync(request, cancellationToken);

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
                    Error = _localizationService["Error occurred while delete the tag"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "Tag")
                }
            };
        }
    }
}