using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.Posts
{
    public class PostCreateViewModel : ViewModelBase<PostCreateRequest>
    {
        private readonly IPostReadOnlyRespository _postReadOnlyRespository;
        private readonly IPostReadWriteRespository _postReadWriteRespository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        public PostCreateViewModel(IPostReadOnlyRespository postReadOnlyRespository, IPostReadWriteRespository postReadWriteRespository, ILocalizationService localizationService, IMapper mapper)
        {
            _localizationService = localizationService;
            _mapper = mapper;
            _postReadOnlyRespository = postReadOnlyRespository;
            _postReadWriteRespository = postReadWriteRespository;
        }
        public override async Task HandleAsync(PostCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createPostResult = await _postReadWriteRespository.CreateNewPost(_mapper.Map<PostEntity>(request), cancellationToken);
                if (createPostResult.Success)
                {
                    var result = await _postReadOnlyRespository.GetPostByIdAsync(createPostResult.Data, cancellationToken);
                    Data = result.Data!;
                    Success = result.Success;
                    ErrorItems = result.Errors;
                    Message = result.Message;
                    return;
                }
                Success = createPostResult.Success;
                ErrorItems = createPostResult.Errors;
                Message = createPostResult.Message;
            }
            catch (Exception)
            {

                Success = false;
                ErrorItems = new[]
                    {
                    new ErrorItem
                    {
                        Error = _localizationService["Error occurred while getting the Post"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Post")
                    }
                };
            }
        }
    }
}
