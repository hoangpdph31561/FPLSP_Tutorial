using FPLSP_Tutorial.Application.Interfaces.Repositories.ClientPostReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.ClientPost
{
    public class GetAllMajorsListViewModel : ViewModelBase<int>
    {
        private readonly IClientPostReadOnlyRespository _clientPostReadOnly;
        private readonly ILocalizationService _localizationService;
        public GetAllMajorsListViewModel(IClientPostReadOnlyRespository clientPostReadOnly, ILocalizationService localizationService)
        {
            _clientPostReadOnly = clientPostReadOnly;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(int data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _clientPostReadOnly.GetAllMajors(cancellationToken);
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
                           Error = _localizationService["Error occurred while getting the list of majors"],
                           FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of Majors")
                     }
                };
            }
        }
    }
}
