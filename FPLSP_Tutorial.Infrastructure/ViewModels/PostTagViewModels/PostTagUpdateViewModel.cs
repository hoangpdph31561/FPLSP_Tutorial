using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.PostTag.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.PostTagViewModels
{
    public class PostTagUpdateViewModel : ViewModelBase<PostTagUpdateRequest>
    {
        public readonly IPostTagReadWriteRespository _postTagReadWriteRespository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public PostTagUpdateViewModel(IPostTagReadWriteRespository postTagReadWriteRespository, ILocalizationService localizationService, IMapper mapper)
        {
            _postTagReadWriteRespository = postTagReadWriteRespository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public async override Task HandleAsync(PostTagUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _postTagReadWriteRespository.UpdatePostTagAsync(_mapper.Map<PostTagEntity>(request), cancellationToken);

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
                            Error = _localizationService["Error occurred while updating the posttag"],
                            FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "PostTag")
                        }
                    };
            }
        }
    }
}
