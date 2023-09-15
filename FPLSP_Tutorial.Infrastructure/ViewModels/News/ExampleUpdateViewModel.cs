using AutoMapper;
using FPLSPTutorial.Application.DataTransferObjects.Example.Request;
using FPLSPTutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSPTutorial.Application.Interfaces.Services;
using FPLSPTutorial.Application.ValueObjects.Common;
using FPLSPTutorial.Application.ViewModels;
using FPLSPTutorial.Domain.Entities;

namespace FPLSPTutorial.Infrastructure.ViewModels.News
{
    public class ExampleUpdateViewModel : ViewModelBase<ExampleUpdateRequest>
    {
        public readonly IExampleReadWriteRepository _exampleReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public ExampleUpdateViewModel(IExampleReadWriteRepository ExampleReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _exampleReadWriteRepository = ExampleReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public override async Task HandleAsync(ExampleUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _exampleReadWriteRepository.UpdateExampleAsync(_mapper.Map<ExampleEntity>(request), cancellationToken);

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
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "Example")
                    }
                };
            }
        }
    }
}
