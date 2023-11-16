using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;
using FPLSP_Tutorial.Domain.Enums;
using FPLSP_Tutorial.Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadWrite
{
    public class MajorRequestReadWriteRepository : IMajorRequestReadWriteRespository
    {
        private readonly ILocalizationService _localizationService;
        private readonly AppReadWriteDbContext _dbContext;

        public MajorRequestReadWriteRepository(ILocalizationService localizationService, AppReadWriteDbContext dbContext)
        {
            _localizationService = localizationService;
            _dbContext = dbContext;
        }
        public async Task<RequestResult<Guid>> AddMajorRequestAsync(MajorRequestEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.Id = new Guid();
                entity.CreatedTime = DateTimeOffset.UtcNow;
                entity.ModifiedTime = DateTimeOffset.UtcNow;
                entity.DeletedTime = DateTimeOffset.UtcNow;
                await _dbContext.MajorRequestEntities.AddAsync(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create MajorRequest"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "MajorRequest"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeleteMajorRequestAsync(MajorRequestDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var majorRequest = await GetMajorRequestByIdAsync(request.Id, cancellationToken);

                // update trạng thái 
                majorRequest!.Deleted = true;
                majorRequest.Status = EntityStatus.Deleted;
                _dbContext.MajorRequestEntities.Update(majorRequest);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete majorRequest"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "majorRequest"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateMajorRequestAsync(MajorRequestEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var majorRequest = await GetMajorRequestByIdAsync(entity.Id, cancellationToken);

                majorRequest!.Status = entity.Status;
                majorRequest.ModifiedBy = entity.ModifiedBy;
                majorRequest.ModifiedTime = DateTimeOffset.UtcNow;
                _dbContext.MajorRequestEntities.Update(majorRequest);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update majorRequest"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "majorRequest"
                    }
                });
            }
        }
        private async Task<MajorRequestEntity?> GetMajorRequestByIdAsync(Guid idMajorRequest, CancellationToken cancellationToken)
        {
            var majorRequest = await _dbContext.MajorRequestEntities.FirstOrDefaultAsync(c => c.Id == idMajorRequest && !c.Deleted, cancellationToken);

            return majorRequest;
        }
    }
}
