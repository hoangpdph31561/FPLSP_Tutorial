using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Major.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.Major
{
    public class MajorUpdateViewModel : ViewModelBase<MajorUpdateRequest>
    {
        public readonly IMajorReadWriteRepository _majorReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        public MajorUpdateViewModel(IMajorReadWriteRepository majorReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _majorReadWriteRepository = majorReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public override async Task HandleAsync(MajorUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _majorReadWriteRepository.UpdateMajorAsync(_mapper.Map<MajorEntity>(request), cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the major"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "major")
                    }
                };
            }
        }
    }
}
