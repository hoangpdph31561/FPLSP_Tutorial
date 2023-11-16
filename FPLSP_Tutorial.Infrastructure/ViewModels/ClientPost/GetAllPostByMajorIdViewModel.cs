using Azure.Core;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ClientPostReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.ClientPost
{
    public class GetAllPostByMajorIdViewModel : ViewModelBase<Guid?>
    {
        private readonly IClientPostReadOnlyRespository _clientPostReadOnly;
        private readonly ILocalizationService _localizationService;
        public GetAllPostByMajorIdViewModel(IClientPostReadOnlyRespository clientPostReadOnly, ILocalizationService localizationService)
        {
            _clientPostReadOnly = clientPostReadOnly;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(Guid? id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _clientPostReadOnly.GetAllPostByMajorId(id,cancellationToken);
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
                Error = _localizationService["Error occurred while getting the list of post by MajorId"],
                FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of post by MajorId")
          }
     };
            }
        }
    }
}
