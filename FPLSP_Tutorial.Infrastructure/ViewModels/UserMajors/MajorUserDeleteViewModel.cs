using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.UserMajors
{
    public class MajorUserDeleteViewModel : ViewModelBase<DeleteMajorUserRequest>
    {
        public readonly IMajorUserReadWriteResponsitory _majorUserReadWriteResponsitory;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        public MajorUserDeleteViewModel(IMapper mapper, IMajorUserReadWriteResponsitory majorUserReadWriteResponsitory, ILocalizationService localizationService)
        {
            _mapper = mapper;
            _majorUserReadWriteResponsitory = majorUserReadWriteResponsitory;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(DeleteMajorUserRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _majorUserReadWriteResponsitory.DeleteMajorUserAsync(data, cancellationToken);

                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
                return;
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
}
