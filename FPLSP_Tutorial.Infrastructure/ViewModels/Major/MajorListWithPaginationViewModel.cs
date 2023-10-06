using FPLSP_Tutorial.Application.DataTransferObjects.Major.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.Major
{
    public class MajorListWithPaginationViewModel : ViewModelBase<ViewMajorWithPaginationRequest>
    {
        public readonly IMajorReadOnlyRepository _majorReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public MajorListWithPaginationViewModel(IMajorReadOnlyRepository majorReadOnlyRepository, ILocalizationService localizationService)
        {
            _majorReadOnlyRepository = majorReadOnlyRepository;
            _localizationService = localizationService;
        }

        public override async Task HandleAsync(ViewMajorWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _majorReadOnlyRepository.GetMajorWithPaginationByAdminAsync(request, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of major"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of major")
                }
            };
            }
        }
    }
}
