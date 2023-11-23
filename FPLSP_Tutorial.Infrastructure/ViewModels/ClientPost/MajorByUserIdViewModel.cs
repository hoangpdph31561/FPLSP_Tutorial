using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.ClientPost
{
    public class MajorByUserIdViewModel : ViewModelBase<GetMajorByUserIdWithPaginationRequest>
    {
        private readonly IClientPostReadOnlyRespository _clientPostReadRespository;
        private readonly ILocalizationService _localizationService;
        public MajorByUserIdViewModel(IClientPostReadOnlyRespository clientPostReadOnlyRespository, ILocalizationService localizationService)
        {
            _clientPostReadRespository = clientPostReadOnlyRespository;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(GetMajorByUserIdWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _clientPostReadRespository.GetMajorByUserIdAsync(request, cancellationToken);
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
                           Error = _localizationService["Error occurred while getting the list of majors by UserId"],
                           FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of Majors by UserId")
                     }
                };
            }
        }
    }
}
