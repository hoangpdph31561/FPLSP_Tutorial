using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.Posts;

public class PostUpdateViewModel : ViewModelBase<PostUpdateRequest>
{
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;
    private readonly IPostReadWriteRepository _postReadWriteRespository;

    public PostUpdateViewModel(IPostReadWriteRepository postReadWriteRespository,
        ILocalizationService localizationService, IMapper mapper)
    {
        _postReadWriteRespository = postReadWriteRespository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public override async Task HandleAsync(PostUpdateRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result =
                await _postReadWriteRespository.UpdateAsync(_mapper.Map<PostEntity>(request), cancellationToken);
            Success = result.Success;
            ErrorItems = result.Errors;
            Message = result.Message;
        }
        catch (Exception)
        {
            Success = false;
            ErrorItems = new[]
            {
                new ErrorItem
                {
                    Error = _localizationService["Error occurred while updating the Post"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "Post")
                }
            };
        }
    }
}