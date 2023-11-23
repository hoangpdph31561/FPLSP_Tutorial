using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.ClientPost
{
    public class ClientPostMajorViewModel : ViewModelBase<Guid>
    {
        private readonly IClientPostReadOnlyRespository _clientPostReadOnly;
        private readonly ILocalizationService _localizationService;
        public ClientPostMajorViewModel(IClientPostReadOnlyRespository clientPostReadOnly, ILocalizationService localizationService)
        {
            _clientPostReadOnly = clientPostReadOnly;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _clientPostReadOnly.GetMajorByIdAsync(id, cancellationToken);
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
                           Error = _localizationService["Error occurred while getting major"],
                           FieldName = string.Concat(LocalizationString.Common.FailedToGet, "major")
                     }
                };
            }
        }
    }
}
