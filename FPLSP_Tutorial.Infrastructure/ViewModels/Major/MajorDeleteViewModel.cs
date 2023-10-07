using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Major.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.Major
{
    public class MajorDeleteViewModel : ViewModelBase<MajorDeleteRequest>
    {
        public readonly IMajorReadWriteRepository _majorReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public MajorDeleteViewModel(IMajorReadWriteRepository majorReadWriteRepository, ILocalizationService localizationService
            , IMapper mapper)
        {
            _majorReadWriteRepository = majorReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public override async Task HandleAsync(MajorDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _majorReadWriteRepository.DeleteMajorAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the Major"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "Major")
                    }
                };
            }
        }
    }
}
