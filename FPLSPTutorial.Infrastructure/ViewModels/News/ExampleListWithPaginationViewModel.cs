using FPLSPTutorial.Application.DataTransferObjects.Example.Request;
using FPLSPTutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSPTutorial.Application.Interfaces.Services;
using FPLSPTutorial.Application.ValueObjects.Common;
using FPLSPTutorial.Application.ViewModels;

namespace FPLSPTutorial.Infrastructure.ViewModels.News
{
    public class ExampleListWithPaginationViewModel : ViewModelBase<ViewExampleWithPaginationRequest>
    {
        public readonly IExampleReadOnlyRepository _exampleReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public ExampleListWithPaginationViewModel(IExampleReadOnlyRepository ExampleReadOnlyRepository, ILocalizationService localizationService)
        {
            _exampleReadOnlyRepository = ExampleReadOnlyRepository;
            _localizationService = localizationService;
        }

        public override async Task HandleAsync(ViewExampleWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _exampleReadOnlyRepository.GetExampleWithPaginationByAdminAsync(request, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of Example"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of Example")
                }
            };
            }
        }
    }
}
