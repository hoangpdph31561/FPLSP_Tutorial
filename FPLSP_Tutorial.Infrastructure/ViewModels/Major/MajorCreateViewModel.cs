using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Major.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.Major
{
    public class MajorCreateViewModel : ViewModelBase<MajorCreateRequest>
    {
        public readonly IMajorReadOnlyRepository _majorReadOnlyRepository;
        public readonly IMajorReadWriteRepository _majorReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public MajorCreateViewModel(IMajorReadOnlyRepository majorReadOnlyRepository, IMajorReadWriteRepository majorReadWriteRepository,
            ILocalizationService localizationService, IMapper mapper)
        {
            _majorReadOnlyRepository = majorReadOnlyRepository;
            _majorReadWriteRepository = majorReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(MajorCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _majorReadWriteRepository.AddMajorAsync(_mapper.Map<MajorEntity>(request), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _majorReadOnlyRepository.GetMajorByIdAsync(createResult.Data, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the Major"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Major")
                    }
                };
            }
        }
    }
}
