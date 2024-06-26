﻿using FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.Posts;

public class PostDeleteViewModel : ViewModelBase<PostDeleteRequest>
{
    private readonly ILocalizationService _localizationService;
    private readonly IPostReadWriteRepository _postReadWriteRespository;

    public PostDeleteViewModel(IPostReadWriteRepository postReadWriteRespository,
        ILocalizationService localizationService)
    {
        _postReadWriteRespository = postReadWriteRespository;
        _localizationService = localizationService;
    }

    public override async Task HandleAsync(PostDeleteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _postReadWriteRespository.DeleteAsync(request, cancellationToken);
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
                    FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "Post")
                }
            };
        }
    }
}