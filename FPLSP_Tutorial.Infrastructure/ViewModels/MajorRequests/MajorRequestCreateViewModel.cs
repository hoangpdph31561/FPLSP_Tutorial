using AutoMapper;
using Azure.Core;
using FPLSP_Tutorial.Application.DataTransferObjects.Example.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;
using FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadOnly;
using FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadWrite;
using FPLSP_Tutorial.Infrastructure.Implements.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.MajorRequests
{
    public class MajorRequestCreateViewModel : ViewModelBase<MajorRequestCreateRequest>
    {
        private readonly IMajorRequestReadOnlyRespository _majorRequestReadOnlyRespository;
        private readonly IMajorRequestReadWriteRespository _majorRequestReadWriteRespository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public MajorRequestCreateViewModel(IMajorRequestReadOnlyRespository majorRequestReadOnlyRespository, IMajorRequestReadWriteRespository majorRequestReadWriteRespository, ILocalizationService localizationService, IMapper mapper)
        {
            _majorRequestReadOnlyRespository = majorRequestReadOnlyRespository;
            _majorRequestReadWriteRespository = majorRequestReadWriteRespository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public async override Task HandleAsync(MajorRequestCreateRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _majorRequestReadWriteRespository.AddMajorRequestAsync(_mapper.Map<MajorRequestEntity>(data), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _majorRequestReadOnlyRespository.GetMajorRequestByIdAsync(createResult.Data, cancellationToken);

                    Data = result.Data!;
                    Success = result.Success;
                    ErrorItems = result.Errors;
                    Message = result.Message;
                    return;
                }

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
                        Error = _localizationService["Error occurred while getting the MajorRequest"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "MajorRequest")
                    }
                };
            }
        }
    }
}
