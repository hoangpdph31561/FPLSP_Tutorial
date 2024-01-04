using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.MajorRequests;

public class MajorRequestViewModel : ViewModelBase<Guid>
{
    private readonly ILocalizationService _localizationService;
    private readonly IMajorRequestReadOnlyRepository _majorRequestReadOnlyRespository;

    public MajorRequestViewModel(IMajorRequestReadOnlyRepository majorRequestReadOnlyRespository,
        ILocalizationService localizationService)
    {
        _majorRequestReadOnlyRespository = majorRequestReadOnlyRespository;
        _localizationService = localizationService;
    }

    public override async Task HandleAsync(Guid idMajorRequest, CancellationToken cancellationToken)
    {
        try
        {
            var result =
                await _majorRequestReadOnlyRespository.GetMajorRequestByIdAsync(idMajorRequest, cancellationToken);
            Data = result.Data!;
            Success = result.Success;
            ErrorItems = result.Errors;
            Message = result.Message;
        }
        catch
        {
            Success = false;
            ErrorItems = new[]
            {
                new ErrorItem
                {
                    Error = _localizationService["Error occurred while getting the MajorRequest"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "MajorRequest")
                }
            };
        }
    }
}