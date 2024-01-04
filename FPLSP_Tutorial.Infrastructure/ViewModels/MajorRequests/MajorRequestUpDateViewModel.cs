using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.MajorRequests;

public class MajorRequestUpdateViewModel : ViewModelBase<MajorRequestUpdateRequest>
{
    private readonly ILocalizationService _localizationService;
    private readonly IMajorRequestReadWriteRepository _majorRequestReadWriteRespository;
    private readonly IMapper _mapper;

    public MajorRequestUpdateViewModel(IMajorRequestReadWriteRepository majorRequestReadWriteRespository,
        ILocalizationService localizationService, IMapper mapper)
    {
        _majorRequestReadWriteRespository = majorRequestReadWriteRespository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public override async Task HandleAsync(MajorRequestUpdateRequest data, CancellationToken cancellationToken)
    {
        try
        {
            var result =
                await _majorRequestReadWriteRespository.UpdateMajorRequestAsync(_mapper.Map<MajorRequestEntity>(data),
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
                    Error = _localizationService["Error occurred while updating the majorRequest"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "majorRequest")
                }
            };
        }
    }
}