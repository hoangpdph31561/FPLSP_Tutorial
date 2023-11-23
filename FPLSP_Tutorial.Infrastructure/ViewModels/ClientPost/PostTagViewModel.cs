using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.ClientPost
{
    public class PostTagViewModel : ViewModelBase<PostIdRequestWithPagination>
    {
        private readonly IClientPostReadOnlyRespository _clientPostReadOnlyRespository;
        private readonly ILocalizationService _localizationService;
        public PostTagViewModel(IClientPostReadOnlyRespository clientPostReadOnlyRespository, ILocalizationService localizationService)
        {
            _clientPostReadOnlyRespository = clientPostReadOnlyRespository;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(PostIdRequestWithPagination request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _clientPostReadOnlyRespository.GetPostTagsByPostIdAsync(request, cancellationToken);
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
                           Error = _localizationService["Error occurred while getting the list of tags of post"],
                           FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of tags of post")
                     }
                };
            }
        }
    }
}
