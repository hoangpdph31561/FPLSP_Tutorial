using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.Major
{
    public class MajorViewModel : ViewModelBase<Guid>
    {
        public readonly IMajorReadOnlyRepository _majorReadOnlyRepository;
        private readonly ILocalizationService _localizationService;
        public MajorViewModel(IMajorReadOnlyRepository majorReadOnlyRepository, ILocalizationService localizationService)
        {
            _majorReadOnlyRepository = majorReadOnlyRepository;
            _localizationService = localizationService;
        }

        public override async Task HandleAsync(Guid idMajor, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _majorReadOnlyRepository.GetMajorByIdAsync(idMajor, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the Major"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Major")
                }
            };
            }
        }
    }
}
