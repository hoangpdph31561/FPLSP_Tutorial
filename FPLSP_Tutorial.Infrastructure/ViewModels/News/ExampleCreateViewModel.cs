using AutoMapper;
using FPLSPTutorial.Application.DataTransferObjects.Example.Request;
using FPLSPTutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSPTutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSPTutorial.Application.Interfaces.Services;
using FPLSPTutorial.Application.ValueObjects.Common;
using FPLSPTutorial.Application.ViewModels;
using FPLSPTutorial.Domain.Entities;

namespace FPLSPTutorial.Infrastructure.ViewModels.News
{
    public class ExampleCreateViewModel : ViewModelBase<ExampleCreateRequest>
    {
        public readonly IExampleReadOnlyRepository _exampleReadOnlyRepository;
        public readonly IExampleReadWriteRepository _exampleReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public ExampleCreateViewModel(IExampleReadOnlyRepository ExampleReadOnlyRepository, IExampleReadWriteRepository ExampleReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _exampleReadOnlyRepository = ExampleReadOnlyRepository;
            _exampleReadWriteRepository = ExampleReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public override async Task HandleAsync(ExampleCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _exampleReadWriteRepository.AddExampleAsync(_mapper.Map<ExampleEntity>(request), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _exampleReadOnlyRepository.GetExampleByIdAsync(createResult.Data, cancellationToken);

                    Data = result.Data!;
                    Success = result.Success;
                    ErrorItems = result.Errors;
                    Message = result.Message;
                    return;
                }

                Success = createResult.Success;
                ErrorItems = createResult.Errors;
                Message = createResult.Message;
            }
            catch (Exception)
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
