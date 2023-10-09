using AutoMapper;
using Azure.Core;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request;
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

namespace FPLSP_Tutorial.Infrastructure.ViewModels.UserMajors
{
    public class MajorUserCreateViewModel : ViewModelBase<CreateUserMajorRequest>
    {
        private readonly IMajorUserReadWriteResponsitory _majorUserReadWriteResponsitory;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public MajorUserCreateViewModel(IMajorUserReadWriteResponsitory majorUserReadWriteResponsitory, ILocalizationService localizationService, IMapper mapper)
        {
            _majorUserReadWriteResponsitory = majorUserReadWriteResponsitory;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(CreateUserMajorRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _majorUserReadWriteResponsitory.AddMajorUserAsync(_mapper.Map<UserMajorEntity>(data), cancellationToken);
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
                        Error = _localizationService["Error occurred while getting the Example"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Example")
                    }
                };
            }
        }
    }
}
