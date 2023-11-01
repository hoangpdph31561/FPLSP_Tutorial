﻿
using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request;

using FPLSP_Tutorial.Application.Interfaces.Repositories.ClientPostReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ClientPostReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if(createResult.Success)
                {
                    var result = await _clientPostReadOnlyRespository.GetMajorRequestByIdAsync(createResult.Data,cancellationToken);
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
            catch (Exception )
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