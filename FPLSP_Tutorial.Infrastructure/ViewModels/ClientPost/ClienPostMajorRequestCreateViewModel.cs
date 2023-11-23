
using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.ClientPost
{
    public class ClienPostMajorRequestCreateViewModel : ViewModelBase<InputMajorRequest>
    {
        private readonly IClientPostReadOnlyRespository _clientPostReadOnlyRespository;
        private readonly IClientPostReadWriteRespository _clientPostReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public ClienPostMajorRequestCreateViewModel(IClientPostReadOnlyRespository clientPostReadOnlyRespository, IClientPostReadWriteRespository clientPostReadWriteRespository, IMapper mapper, ILocalizationService localizationService)
        {
            _clientPostReadOnlyRespository = clientPostReadOnlyRespository;
            _clientPostReadWriteRespository = clientPostReadWriteRespository;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public override async Task HandleAsync(InputMajorRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _clientPostReadWriteRespository.AddMajorRequest(_mapper.Map<MajorRequestEntity>(request), cancellationToken);
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
                        Error = _localizationService["Error occurred while getting the Major request"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Major request")
                    }
                };

            }
        }
    }
}
