using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.TagViewModels
{
    public class ListTagViewModel : ViewModelBase<Guid?>
    {
        private readonly ITagReadOnlyRepository _tagReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public ListTagViewModel(ITagReadOnlyRepository tagReadOnlyRepository, ILocalizationService localizationService)
        {
            _tagReadOnlyRepository = tagReadOnlyRepository;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(Guid? idMajor, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _tagReadOnlyRepository.GetTagByIdMajorAsync(idMajor,  cancellationToken);

                Data = result.Data!;
                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
                return;
            }
            catch
            {
                Success = false;
                ErrorItems = new[]
                {
                    new ErrorItem
                    {
                        Error = _localizationService["Error occurred while getting the tags"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Tags")
                    }
                };
            }
        }
    }
}
