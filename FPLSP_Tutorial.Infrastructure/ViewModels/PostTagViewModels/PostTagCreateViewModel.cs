﻿using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.PostTag.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.PostTagViewModels
{
    public class PostTagCreateViewModel : ViewModelBase<PostTagCreateRequest>
    {
        private readonly IPostTagReadWriteRepository _postTagReadWriteRespository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public PostTagCreateViewModel(IPostTagReadWriteRepository postTagReadWriteRespository, ILocalizationService localizationService, IMapper mapper)
        {
            _postTagReadWriteRespository = postTagReadWriteRespository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public async override Task HandleAsync(PostTagCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _postTagReadWriteRespository.AddPostTagAsync(_mapper.Map<PostTagEntity>(request), cancellationToken);

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
                        Error = _localizationService["Error occurred while create posttags"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "PostTags")
                    }
                };
            }
        }
    }
}
