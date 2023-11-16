using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.UserMajors
{
    public class MajorUserUpdateViewModel : ViewModelBase<UpdateMajorUserRequest>
    {
        private readonly IMajorUserReadWriteResponsitory _majorUserReadWriteRespository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public MajorUserUpdateViewModel(IMajorUserReadWriteResponsitory majorUserReadWriteRespository, ILocalizationService localizationService, IMapper mapper)
        {
            _majorUserReadWriteRespository = majorUserReadWriteRespository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(UpdateMajorUserRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _majorUserReadWriteRespository.UpdateMajorUserAsync(_mapper.Map<UserMajorEntity>(data), cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the UserMajor"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "UserMajor")
                    }
                };
            }
        }
    }
}
