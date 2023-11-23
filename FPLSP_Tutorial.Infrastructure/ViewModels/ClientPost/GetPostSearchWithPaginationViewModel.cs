using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.ClientPost
{
    public class GetPostSearchWithPaginationViewModel : ViewModelBase<ClientPostSearchWithPaginationRequest>
    {
        private readonly IClientPostReadOnlyRespository _clientPostReadOnly;
        private readonly ILocalizationService _localizationService;
        public GetPostSearchWithPaginationViewModel(IClientPostReadOnlyRespository clientPostReadOnly, ILocalizationService localizationService)
        {
            _clientPostReadOnly = clientPostReadOnly;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(ClientPostSearchWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _clientPostReadOnly.GetAllPostsBySearchAsyn(request, cancellationToken);
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
                Error = _localizationService["Error occurred while getting the list of post by search"],
                FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of post by search")
          }
     };
            }
        }
    }
}
