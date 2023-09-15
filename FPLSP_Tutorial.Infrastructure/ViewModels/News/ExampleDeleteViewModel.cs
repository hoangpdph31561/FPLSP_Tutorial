using AutoMapper;
using FPLSPTutorial.Application.DataTransferObjects.Example.Request;
using FPLSPTutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSPTutorial.Application.Interfaces.Services;
using FPLSPTutorial.Application.ValueObjects.Common;
using FPLSPTutorial.Application.ViewModels;

namespace FPLSPTutorial.Infrastructure.ViewModels.News
{
    public class ExampleDeleteViewModel : ViewModelBase<ExampleDeleteRequest>
    {
        public readonly IExampleReadWriteRepository _exampleReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public ExampleDeleteViewModel(IExampleReadWriteRepository ExampleReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _exampleReadWriteRepository = ExampleReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public override async Task HandleAsync(ExampleDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _exampleReadWriteRepository.DeleteExampleAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the Example"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "Example")
                    }
                };
            }
        }
    }
}
