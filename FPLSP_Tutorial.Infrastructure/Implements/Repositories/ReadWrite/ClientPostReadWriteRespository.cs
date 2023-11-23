using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;
using FPLSP_Tutorial.Infrastructure.Database.AppDbContext;

namespace FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadWrite
{
    public class ClientPostReadWriteRespository : IClientPostReadWriteRespository
    {
        private readonly AppReadWriteDbContext _dbContext;
        private readonly ILocalizationService _localizationService;
        public ClientPostReadWriteRespository(AppReadWriteDbContext dbContext, ILocalizationService localizationService)
        {
            _dbContext = dbContext;
            _localizationService = localizationService;

        }
        public async Task<RequestResult<Guid>> AddMajorRequest(MajorRequestEntity entity, CancellationToken cancellationToken)
        {
            try
            {

                entity.CreatedTime = DateTimeOffset.Now;
                entity.ModifiedTime = DateTimeOffset.Now;
                entity.DeletedTime = DateTimeOffset.Now;
                entity.Deleted = false;
                entity.IsManager = false;
                entity.DeletedBy = null;
                await _dbContext.MajorRequestEntities.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {

                return RequestResult<Guid>.Fail(_localizationService["Unable to create major request"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "major request"
                    }
                });
            }
        }
    }
}
