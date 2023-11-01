
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.Posts
{
    public class PostViewModel : ViewModelBase<Guid>
    {
        private readonly IPostReadOnlyRespository _postReadOnlyRespository;
        private readonly ILocalizationService _localizationService;
        public PostViewModel(IPostReadOnlyRespository postReadOnlyRespository, ILocalizationService localizationService)
        {
            _localizationService = localizationService;
            _postReadOnlyRespository = postReadOnlyRespository;
        }
        public override async Task HandleAsync(Guid idPost, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _postReadOnlyRespository.GetPostByIdAsync(idPost, cancellationToken);
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
                        Error = _localizationService["Error occurred while getting the Post"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Example")
                    }
                };
            }
        }
    }
}
