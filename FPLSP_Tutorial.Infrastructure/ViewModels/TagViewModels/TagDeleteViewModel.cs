using AutoMapper;
using Azure.Core;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.TagViewModels
{
    public class TagDeleteViewModel : ViewModelBase<TagDeleteRequest>
    {
        public readonly ITagReadWriteRepository _tagReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public TagDeleteViewModel(ITagReadWriteRepository tagReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _tagReadWriteRepository = tagReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public override async Task HandleAsync(TagDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _tagReadWriteRepository.DeleteTagAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the tag"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "Tag")
                    }
                };
            }
        }
    }
}
