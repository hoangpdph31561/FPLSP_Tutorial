using FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;
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
    public class PostReadWriteRespository : IPostReadWriteRespository
    {
        private readonly AppReadOnlyDbContext _appDbContext;
        private readonly ILocalizationService _localizationService;
        public PostReadWriteRespository(AppReadOnlyDbContext appDbContext, ILocalizationService localizationService)
        {
            _appDbContext = appDbContext;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<Guid>> CreateNewPost(PostEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.Id = Guid.NewGuid();
                entity.CreatedTime = DateTimeOffset.UtcNow;
                await _appDbContext.PostEntities.AddAsync(entity);
                await _appDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {

                return RequestResult<Guid>.Fail(_localizationService["Unable to create new post"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "post"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeletePost(PostDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var postDeleting = await GetPostByIdAsync(request.Id, cancellationToken);

                postDeleting!.Deleted = true;
                postDeleting.DeletedBy = request.DeletedBy;
                postDeleting.DeletedTime = DateTimeOffset.UtcNow;
                postDeleting.Status = EntityStatus.Deleted;
                _appDbContext.PostEntities.Update(postDeleting);
                await _appDbContext.SaveChangesAsync();
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {

                return RequestResult<int>.Fail(_localizationService["Unable to delete post"], new[]
               {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "post"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdatePost(PostEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var postUpdating = await GetPostByIdAsync(entity.Id, cancellationToken);
                postUpdating!.ModifiedTime = DateTimeOffset.UtcNow;
                postUpdating.ModifiedBy = entity.ModifiedBy;
                postUpdating.PostId = entity.PostId;
                postUpdating.PostType = entity.PostType;
                postUpdating.Title =  entity.Title;
                postUpdating.Content = entity.Content;
                postUpdating.Status = entity.Status;
                _appDbContext.PostEntities.Update(postUpdating);
                await _appDbContext.SaveChangesAsync();
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {

                return RequestResult<int>.Fail(_localizationService["Unable to update post"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "post"
                    }
                });
            }
        }
        private async Task<PostEntity?> GetPostByIdAsync(Guid postId, CancellationToken cancellationToken)
        {
            var post = await _appDbContext.PostEntities.FirstOrDefaultAsync(x => x.Id == postId && !x.Deleted, cancellationToken);
            return post;
        }
    }
}
