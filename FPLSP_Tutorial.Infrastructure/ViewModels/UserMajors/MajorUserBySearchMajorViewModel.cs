using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.UserMajors
{
    public class MajorUserBySearchMajorViewModel : ViewModelBase<ViewMajorUserBySearchRequest>
    {
        private readonly IUserMajorReadOnlyRespository _majorUserReadOnlyRespository;
        private readonly ILocalizationService _localizationService;

        public MajorUserBySearchMajorViewModel(IUserMajorReadOnlyRespository userMajorReadOnlyRespository, ILocalizationService localizationService)
        {
            _majorUserReadOnlyRespository = userMajorReadOnlyRespository;
            _localizationService = localizationService;
        }

        public override async Task HandleAsync(ViewMajorUserBySearchRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _majorUserReadOnlyRespository.GetMajorUserWithPaginationBySearchMajordAsync(data, cancellationToken);
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
                    Error = _localizationService["Error occurred while getting the majorUser"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "majorUser")
                }
            };
            }
        }
    }
}
