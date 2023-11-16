using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request;
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
    public class MajorUserReadWriteResponsitory : IMajorUserReadWriteResponsitory
    {
        private readonly ILocalizationService _localizationService;
        private readonly AppReadOnlyDbContext _dbContext;

        public MajorUserReadWriteResponsitory(ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _localizationService = localizationService;
            _dbContext = dbContext;
        }
        public async Task<RequestResult<Guid>> AddMajorUserAsync(UserMajorEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.Id = new Guid();
                entity.CreatedTime = DateTimeOffset.UtcNow;
                await _dbContext.UserMajorEntities.AddAsync(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create MajorUser"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "MajorUser"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeleteMajorUserAsync(DeleteMajorUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userMajor = await GetMajorUserByIdAsync(request.Id, cancellationToken);

                userMajor!.Deleted = true;
                userMajor.DeletedBy = request.DeletedBy;
                userMajor.DeletedTime = DateTimeOffset.UtcNow;
                userMajor.Status = EntityStatus.Deleted;

                _dbContext.UserMajorEntities.Update(userMajor);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete userMajor"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "userMajor"
                    }
                });
            }

        }

        public async Task<RequestResult<int>> UpdateMajorUserAsync(UserMajorEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var majorUser = await GetMajorUserByIdAsync(entity.Id, cancellationToken);

                majorUser!.Status = entity.Status;

                _dbContext.UserMajorEntities.Update(majorUser);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update UserMajor"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "UserMajor"
                    }
                });
            }
        }

        private async Task<UserMajorEntity?> GetMajorUserByIdAsync(Guid idMajorUser, CancellationToken cancellationToken)
        {
            var userMajor = await _dbContext.UserMajorEntities.FirstOrDefaultAsync(c => c.Id == idMajorUser && !c.Deleted, cancellationToken);

            return userMajor;
        }
    }
}
