using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.TagViewModels
{
    public class TagDeleteViewModel : ViewModelBase<TagDeleteRequest>
    {
        public readonly ITagReadWriteRepository _tagReadWriteRepository;
        private readonly ILocalizationService _localizationService;

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
                return;
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
}
