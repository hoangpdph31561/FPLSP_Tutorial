using AutoMapper;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ClientPostReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ViewModels;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.ClientPost
{
    public class ClientPostGetPostTagViewModel : ViewModelBase<Guid>
    {
        private readonly IClientPostReadOnlyRespository _clientPostReadRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public ClientPostGetPostTagViewModel(IClientPostReadOnlyRespository clientPostReadOnlyRespository, IMapper mapper, ILocalizationService localizationService)
        {
            _clientPostReadRespository = clientPostReadOnlyRespository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _clientPostReadRespository.GetPostTagsAsync(id, cancellationToken);
                Data = result.Data!;
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
        Error = _localizationService["Error occurred while getting the list of Tags of post"],
        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of Tags of post")
    }
};
            }
        }
    }
}
