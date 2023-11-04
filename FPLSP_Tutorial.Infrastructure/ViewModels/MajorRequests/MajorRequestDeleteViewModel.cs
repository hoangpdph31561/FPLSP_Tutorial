using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.MajorRequests
{
    public class MajorRequestDeleteViewModel : ViewModelBase<MajorRequestDeleteRequest>
    {
        private readonly IMajorRequestReadWriteRespository _majorRequestReadWriteRespository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public MajorRequestDeleteViewModel(IMajorRequestReadWriteRespository majorRequestReadWriteRespository, ILocalizationService localizationService, IMapper mapper)
        {
            _majorRequestReadWriteRespository = majorRequestReadWriteRespository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public async override Task HandleAsync(MajorRequestDeleteRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _majorRequestReadWriteRespository.DeleteMajorRequestAsync(data, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the MajorRequest"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "MajorRequest")
                    }
                };
            }
        }
    }
}
