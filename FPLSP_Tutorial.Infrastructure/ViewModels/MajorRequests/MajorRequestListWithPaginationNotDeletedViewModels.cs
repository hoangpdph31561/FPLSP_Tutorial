using FPLSP_Tutorial.Application.DataTransferObjects.Major;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.MajorRequests
{
    public class MajorRequestListWithPaginationNotDeletedViewModels : ViewModelBase<ViewMajorRequestWithPaginationRequest>
    {

        private readonly IMajorRequestReadOnlyRespository _majorRequestReadOnlyRespository;
        private readonly ILocalizationService _localizationService;

        public MajorRequestListWithPaginationNotDeletedViewModels(IMajorRequestReadOnlyRespository majorRequestReadOnlyRespository, ILocalizationService localizationService)
        {
            _majorRequestReadOnlyRespository = majorRequestReadOnlyRespository;
            _localizationService = localizationService;
        }

        public override async Task HandleAsync(ViewMajorRequestWithPaginationRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _majorRequestReadOnlyRespository.GetMajorRequestWithPaginationByNotDeletedAsync(data, cancellationToken);
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
                    Error = _localizationService["Error occurred while getting the MajorRequest"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "MajorRequest")
                }
            };
            }
        }
    }
}
