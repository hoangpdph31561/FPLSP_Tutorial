using FPLSP_Tutorial.Application.DataTransferObjects.Major.Request;
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
    public class MajorReadWriteRepository : IMajorReadWriteRepository
    {
        private readonly AppReadWriteDbContext _dbContext;
        private readonly ILocalizationService _localizationService;
        public MajorReadWriteRepository(AppReadWriteDbContext dbContext, ILocalizationService localizationService)
        {
            _dbContext = dbContext;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<Guid>> AddMajorAsync(MajorEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                await _dbContext.MajorEntities.AddAsync(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create major"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "major"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeleteMajorAsync(MajorDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var major = await GetMajorByIdAsync(request.Id, cancellationToken);

                major!.Deleted = true;
                major.DeletedBy = request.DeletedBy;
                major.DeletedTime = DateTimeOffset.UtcNow;
                major.Status = EntityStatus.Deleted;

                _dbContext.MajorEntities.Update(major);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete major"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "major"
                    }
                });
            }

        }

        private async Task<MajorEntity?> GetMajorByIdAsync(Guid idMajor, CancellationToken cancellationToken)
        {
            var major = await _dbContext.MajorEntities.FirstOrDefaultAsync(c => c.Id == idMajor && !c.Deleted, cancellationToken);

            return major;
        }

        public async Task<RequestResult<int>> UpdateMajorAsync(MajorEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var major = await GetMajorByIdAsync(entity.Id, cancellationToken);

                major!.Name = string.IsNullOrEmpty(entity.Name) ? major.Name :entity.Name;
                major!.Code = entity.Code;
                major!.Status = entity.Status;
                major.ModifiedBy = entity.ModifiedBy;
                major.ModifiedTime = DateTimeOffset.UtcNow;

                _dbContext.MajorEntities.Update(major);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update major"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "major"
                    }
                });
            }
        }
    }
}
