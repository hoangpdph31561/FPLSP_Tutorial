using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;

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
                var createResult = await _tagReadWriteRepository.AddTagAsync(_mapper.Map<List<TagCreateModel>,List<TagEntity>>(request.tasks!), cancellationToken);

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
                        Error = _localizationService["Error occurred while create tags"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Tags")
                    }
                };
            }
        }
    }
}
