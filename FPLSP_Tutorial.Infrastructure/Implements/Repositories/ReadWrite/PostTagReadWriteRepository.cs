using FPLSP_Tutorial.Application.DataTransferObjects.PostTag.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;
using FPLSP_Tutorial.Domain.Enums;
using FPLSP_Tutorial.Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadWrite;

public class PostTagReadWriteRepository : IPostTagReadWriteRepository
{
    private readonly AppReadWriteDbContext _dbContext;
    private readonly ILocalizationService _localizationService;

    public PostTagReadWriteRepository(AppReadWriteDbContext dbContext, ILocalizationService localizationService)
    {
        _dbContext = dbContext;
        _localizationService = localizationService;
    }

    public async Task<RequestResult<int>> AddRangeAsync(List<PostTagEntity> entity, CancellationToken cancellationToken)
    {
        try
        {
            await _dbContext.PostTagEntities.AddRangeAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return RequestResult<int>.Succeed(1);
        }
        catch (Exception e)
        {
            return RequestResult<int>.Fail(_localizationService["Unable to create List of PostTag"], new[]
            {
                new ErrorItem
                {
                    Error = e.Message,
                    FieldName = LocalizationString.Common.FailedToCreate + "List of PostTag"
                }
            });
        }
    }

    public async Task<RequestResult<int>> AddAsync(PostTagEntity entity, CancellationToken cancellationToken)
    {
        try
        {
            await _dbContext.PostTagEntities.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return RequestResult<int>.Succeed(1);
        }
        catch (Exception e)
        {
            return RequestResult<int>.Fail(_localizationService["Unable to create PostTag"], new[]
            {
                new ErrorItem
                {
                    Error = e.Message,
                    FieldName = LocalizationString.Common.FailedToCreate + "PostTag"
                }
            });
        }
    }

    public async Task<RequestResult<int>> UpdateAsync(PostTagEntity entity, CancellationToken cancellationToken)
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


    public async Task<RequestResult<int>> DeleteAsync(PostTagDeleteRequest request, CancellationToken cancellationToken)
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

    public async Task<RequestResult<int>> SyncRangeAsync(Guid PostId, List<TagEntity> request,
        CancellationToken cancellationToken)
    {
        try
        {
            var listTagExisted = _dbContext.PostTagEntities
                .Where(t => t.PostId == PostId && t.Status != EntityStatus.Deleted && !t.Deleted).Select(pt => pt.Tag)
                .ToList();
            foreach (var i in listTagExisted.Where(t => !request.Select(r => r.Id).Contains(t.Id)))
            {
                var postTag = _dbContext.PostTagEntities.FirstOrDefault(pt => pt.TagId == i.Id && pt.PostId == PostId);
                if (postTag != null)
                {
                    postTag.Status = EntityStatus.Deleted;
                    postTag.Deleted = true;


                    postTag.DeletedTime = DateTimeOffset.Now;
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            foreach (var i in request.Where(t => !listTagExisted.Select(r => r.Id).Contains(t.Id)))
            {
                await _dbContext.PostTagEntities.AddAsync(new PostTagEntity
                {
                    PostId = PostId,
                    TagId = i.Id,
                    CreatedTime = DateTimeOffset.Now
                });
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            await _dbContext.SaveChangesAsync();

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
        var posttag =
            await _dbContext.PostTagEntities.FirstOrDefaultAsync(c => c.Id == Id && !c.Deleted, cancellationToken);
        return posttag;
    }
}