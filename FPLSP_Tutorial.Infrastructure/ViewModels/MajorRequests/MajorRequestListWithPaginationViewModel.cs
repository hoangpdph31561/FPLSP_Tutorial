using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.MajorRequests;

public class MajorRequestListWithPaginationViewModel : ViewModelBase<MajorRequestViewWithPaginationRequest>

{
    private readonly ILocalizationService _localizationService;
    public readonly IMajorRequestReadOnlyRepository _MajorRequestReadOnlyRepository;

    public MajorRequestListWithPaginationViewModel(IMajorRequestReadOnlyRepository MajorRequestReadOnlyRepository,
        ILocalizationService localizationService)
    {
        _MajorRequestReadOnlyRepository = MajorRequestReadOnlyRepository;
        _localizationService = localizationService;
    }

    public override async Task HandleAsync(MajorRequestViewWithPaginationRequest data,
        CancellationToken cancellationToken)
    {
        try
        {
            var result =
                await _MajorRequestReadOnlyRepository.GetMajorRequestWithPaginationAsync(data, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of MajorRequest"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of MajorRequest")
                }
            };
        }
    }
}