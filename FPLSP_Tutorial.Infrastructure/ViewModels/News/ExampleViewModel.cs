using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.News
{
    public class ExampleViewModel : ViewModelBase<Guid>
    {
        public readonly IExampleReadOnlyRepository _exampleReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public ExampleViewModel(IExampleReadOnlyRepository ExampleReadOnlyRepository, ILocalizationService localizationService)
        {
            _exampleReadOnlyRepository = ExampleReadOnlyRepository;
            _localizationService = localizationService;
        }

        public override async Task HandleAsync(Guid idExample, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _exampleReadOnlyRepository.GetExampleByIdAsync(idExample, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the Example"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Example")
                }
            };
            }
        }
    }
}