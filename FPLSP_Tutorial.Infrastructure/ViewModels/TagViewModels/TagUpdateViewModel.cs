using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.TagViewModels
{
    public class TagUpdateViewModel : ViewModelBase<TagUpdateRequest>
    {
        public readonly ITagReadWriteRepository _tagReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public TagUpdateViewModel(ITagReadWriteRepository tagReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _tagReadWriteRepository = tagReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public override async Task HandleAsync(TagUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _tagReadWriteRepository.UpdateTagAsync(_mapper.Map<TagEntity>(request), cancellationToken);

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
                            FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "Tag")
                        }
                    };
            }
        }
    }
}
