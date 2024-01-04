using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.UserMajor.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.UserMajors;

public class UserMajorDeleteViewModel : ViewModelBase<UserMajorDeleteRequest>
{
    private readonly ILocalizationService _localizationService;
    public readonly IUserMajorReadWriteRepository _majorUserReadWriteResponsitory;
    private readonly IMapper _mapper;

    public UserMajorDeleteViewModel(IMapper mapper, IUserMajorReadWriteRepository majorUserReadWriteResponsitory,
        ILocalizationService localizationService)
    {
        _mapper = mapper;
        _majorUserReadWriteResponsitory = majorUserReadWriteResponsitory;
        _localizationService = localizationService;
    }

    public override async Task HandleAsync(UserMajorDeleteRequest data, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _majorUserReadWriteResponsitory.DeleteUserMajorAsync(data, cancellationToken);

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
                    FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "Example")
                }
            };
        }
    }
}