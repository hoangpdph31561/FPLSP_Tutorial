using FPLSP_Tutorial.Application.DataTransferObjects.PostTag.Request;
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
    public class PostTagReadWriteRepository : IPostTagReadWriteRespository
    {
        private readonly AppReadWriteDbContext _dbContext;
        private readonly ILocalizationService _localizationService;

        public PostTagReadWriteRepository(AppReadWriteDbContext dbContext, ILocalizationService localizationService)
        {
            _dbContext = dbContext;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<int>> AddPostTagAsync(List<PostTagEntity> entity, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var item in entity)
                {
                    item.CreatedTime = DateTimeOffset.Now;
                }
                await _dbContext.PostTagEntities.AddRangeAsync(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to create PostTags"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "PostTags"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeletePostTagAsync(PostTagDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var posttag = await GetTagByIdAsync(request.Id, cancellationToken);

                posttag!.Deleted = true;
                posttag.DeletedBy = request.DeletedBy;
                posttag.DeletedTime = DateTimeOffset.UtcNow;

                posttag.Status = EntityStatus.Deleted;

                _dbContext.PostTagEntities.Remove(posttag);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete PostTags"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "PostTags"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdatePostTagAsync(PostTagEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var posttag = await GetTagByIdAsync(entity.Id, cancellationToken);
                posttag!.TagId = entity.TagId;
                posttag.PostId = entity.PostId;
     
                _dbContext.PostTagEntities.Update(posttag);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update PostTag"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "PostTag"
                    }
                });
            }
        }

        private async Task<PostTagEntity?> GetTagByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            var posttag = await _dbContext.PostTagEntities.FirstOrDefaultAsync(c => c.Id == Id && !c.Deleted, cancellationToken);
            return posttag;
        }
    }
}
