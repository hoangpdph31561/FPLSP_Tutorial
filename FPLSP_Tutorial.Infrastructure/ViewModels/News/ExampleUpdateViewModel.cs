﻿using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Example.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.News;

public class ExampleUpdateViewModel : ViewModelBase<ExampleUpdateRequest>
{
    public readonly IExampleReadWriteRepository _exampleReadWriteRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public ExampleUpdateViewModel(IExampleReadWriteRepository ExampleReadWriteRepository,
        ILocalizationService localizationService, IMapper mapper)
    {
        _exampleReadWriteRepository = ExampleReadWriteRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public override async Task HandleAsync(ExampleUpdateRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result =
                await _exampleReadWriteRepository.UpdateExampleAsync(_mapper.Map<ExampleEntity>(request),
                    cancellationToken);

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
                    Error = _localizationService["Error occurred while updating the Example"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "Example")
                }
            };
        }
    }
}