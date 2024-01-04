using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.UserMajor.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.UserMajors;

public class UserMajorCreateViewModel : ViewModelBase<UserMajorCreateRequest>
{
    private readonly ILocalizationService _localizationService;
    private readonly IUserMajorReadWriteRepository _majorUserReadWriteResponsitory;
    private readonly IMapper _mapper;

    public UserMajorCreateViewModel(IUserMajorReadWriteRepository majorUserReadWriteResponsitory,
        ILocalizationService localizationService, IMapper mapper)
    {
        _majorUserReadWriteResponsitory = majorUserReadWriteResponsitory;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public override async Task HandleAsync(UserMajorCreateRequest data, CancellationToken cancellationToken)
    {
        try
        {
            var createResult =
                await _majorUserReadWriteResponsitory.AddUserMajorAsync(_mapper.Map<UserMajorEntity>(data),
                    cancellationToken);
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
                    Error = _localizationService["Error occurred while getting the UserMajor"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "UserMajor")
                }
            };
        }
    }
}