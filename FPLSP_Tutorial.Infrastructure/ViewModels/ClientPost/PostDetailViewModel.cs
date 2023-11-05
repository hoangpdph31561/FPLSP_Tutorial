﻿using AutoMapper;
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
    public class PostDetailViewModel : ViewModelBase<Guid>
    {
        private readonly IClientPostReadOnlyRespository _clientPostReadRespository;
        private readonly ILocalizationService _localizationService;
        public PostDetailViewModel(IClientPostReadOnlyRespository clientPostReadOnlyRespository, ILocalizationService localizationService)
        {
            _clientPostReadRespository = clientPostReadOnlyRespository;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(Guid id, CancellationToken cancellationToken)
        {

            try
            {
                var result = await _clientPostReadRespository.GetPostDetailByIdAsync(id, cancellationToken);
                Data = result.Data!;
                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
                return;
            }
            catch (Exception)
            {

                Success = false;
                ErrorItems = new[]
                {
                new ErrorItem
                {
                    Error = _localizationService["Error occurred while getting post "],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "post")
                }
            };
            }
        }
    }
}