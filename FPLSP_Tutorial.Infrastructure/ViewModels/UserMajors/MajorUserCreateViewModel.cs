using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.UserMajors
{
    public class MajorUserCreateViewModel : ViewModelBase<CreateUserMajorRequest>
    {
        private readonly IMajorUserReadWriteResponsitory _majorUserReadWriteResponsitory;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public MajorUserCreateViewModel(IMajorUserReadWriteResponsitory majorUserReadWriteResponsitory, ILocalizationService localizationService, IMapper mapper)
        {
            _majorUserReadWriteResponsitory = majorUserReadWriteResponsitory;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(CreateUserMajorRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _majorUserReadWriteResponsitory.AddMajorUserAsync(_mapper.Map<UserMajorEntity>(data), cancellationToken);
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
}
