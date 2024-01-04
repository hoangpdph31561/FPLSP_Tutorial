using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.PostTag.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.PostTag;

public class PostTagSyncRangeViewModel : ViewModelBase<PostTagSyncRangeRequest>
{
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;
    public readonly IPostTagReadWriteRepository _postTagReadWriteRespository;

    public PostTagSyncRangeViewModel(IPostTagReadWriteRepository postTagReadWriteRespository,
        ILocalizationService localizationService, IMapper mapper)
    {
        _postTagReadWriteRespository = postTagReadWriteRespository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public override async Task HandleAsync(PostTagSyncRangeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _postTagReadWriteRespository.SyncRangeAsync(request.PostId,
                _mapper.Map<List<TagEntity>>(request.ListTag), cancellationToken);

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
                    Error = _localizationService["Error occurred while updating the posttag"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "PostTag")
                }
            };
        }
    }
}