using AutoMapper;
using Azure.Core;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;
using FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadOnly;
using FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.TagViewModels
{
    public class TagCreateViewModel : ViewModelBase<TagCreateRequest>
    {
        private readonly ITagReadWriteRepository _tagReadWriteRepository;
        private readonly ILocalizationService _localizationService; 
        private readonly IMapper _mapper;

        public TagCreateViewModel(ITagReadWriteRepository tagReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _tagReadWriteRepository = tagReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public override async Task HandleAsync(TagCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _tagReadWriteRepository.AddTagAsync(_mapper.Map<List<TagEntity>>(request.tasks), cancellationToken);

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
                        Error = _localizationService["Error occurred while getting tags"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Tags")
                    }
                };
            }
        }
    }
}
