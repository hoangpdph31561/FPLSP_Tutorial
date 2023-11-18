using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
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
    public class TagReadWriteRepository : ITagReadWriteRepository
    {
        private readonly AppReadWriteDbContext _dbContext;
        private readonly ILocalizationService _localizationService;

        public TagReadWriteRepository(AppReadWriteDbContext dbContext, ILocalizationService localizationService)
        {
            _dbContext = dbContext;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<int>> AddTagAsync(List<TagEntity> lstTagCreate, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var item in lstTagCreate)
                {
                    item.CreatedTime = DateTimeOffset.Now;
                    item.Status = EntityStatus.Active;
                }
                await _dbContext.TagEntities.AddRangeAsync(lstTagCreate);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to create Tags"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "Tags"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeleteTagAsync(TagDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var tag = await GetTagByIdAsync(request.Id, cancellationToken);

                tag!.Deleted = true;
                tag.DeletedBy = request.DeletedBy;
                tag.DeletedTime = DateTimeOffset.UtcNow;

                tag.Status = EntityStatus.Deleted;

                _dbContext.TagEntities.Remove(tag);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete tag"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "tag"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateTagAsync(TagEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var tag = await GetTagByIdAsync(entity.Id, cancellationToken);
                tag!.Name = entity.Name;
                tag.MajorId = entity.MajorId;
                tag.Status = entity.Status;
                tag.ModifiedBy = entity.ModifiedBy;
                tag.ModifiedTime = DateTimeOffset.UtcNow;

                _dbContext.TagEntities.Update(tag);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update tag"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "tag"
                    }
                });
            }
        }

        private async Task<TagEntity?> GetTagByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            var tag = await _dbContext.TagEntities.FirstOrDefaultAsync(c => c.Id == Id && !c.Deleted, cancellationToken);
            return tag;
        }
    }
}
