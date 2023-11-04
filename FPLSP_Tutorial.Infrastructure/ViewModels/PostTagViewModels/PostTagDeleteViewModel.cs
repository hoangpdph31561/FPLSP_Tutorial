using FPLSP_Tutorial.Application.DataTransferObjects.PostTag.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.PostTagViewModels
{
    public class PostTagDeleteViewModel : ViewModelBase<PostTagDeleteRequest>
    {
        public readonly IPostTagReadWriteRespository _postTagReadWriteRespository;
        private readonly ILocalizationService _localizationService;

        public PostTagDeleteViewModel(IPostTagReadWriteRespository postTagReadWriteRespository, ILocalizationService localizationService)
        {
            _postTagReadWriteRespository = postTagReadWriteRespository;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(PostTagDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _postTagReadWriteRespository.DeletePostTagAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while Delete the posttag"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "PostTag")
                    }
                };
            }
        }
    }
}
