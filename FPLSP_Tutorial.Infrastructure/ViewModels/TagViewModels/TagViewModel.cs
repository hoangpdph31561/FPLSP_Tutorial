﻿using AutoMapper;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.News
{
    public class TagViewModel : ViewModelBase<Guid>
    {
        private readonly ITagReadOnlyRepository _tagReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public TagViewModel(ITagReadOnlyRepository tagReadOnlyRepository, ILocalizationService localizationService)
        {
            _tagReadOnlyRepository = tagReadOnlyRepository;
            _localizationService = localizationService;
        }

        public override async Task HandleAsync(Guid idTag, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _tagReadOnlyRepository.GetTagByIdAsync(idTag, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the tag"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Tag")
                    }
                };
            }
        }
    }
}