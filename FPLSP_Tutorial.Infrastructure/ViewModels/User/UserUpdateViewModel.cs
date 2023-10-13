using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.User.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.User
{
    public class UserUpdateViewModel : ViewModelBase<UserUpdateRequest>
    {
        public readonly IUserReadWriteRepository _userReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        public UserUpdateViewModel(IUserReadWriteRepository UserReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _userReadWriteRepository = UserReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(UserUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userReadWriteRepository.UpdateUserAsync(_mapper.Map<UserEntity>(request), cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the User"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "User")
                    }
                };
            }
        }
    }
}
