using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Tutorial.Application.DataTransferObjects.Post;
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;
using FPLSP_Tutorial.Domain.Enums;
using FPLSP_Tutorial.Infrastructure.Database.AppDbContext;
using FPLSP_Tutorial.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadOnly;

public class PostReadOnlyRepository : IPostReadOnlyRespository
{
    private readonly AppReadOnlyDbContext _dbContext;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public PostReadOnlyRepository(AppReadOnlyDbContext dbContext, IMapper mapper,
        ILocalizationService localizationService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _localizationService = localizationService;
    }

    public async Task<RequestResult<List<PostDTO>>> GetPostAsync(PostViewRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var queryable = _dbContext.PostEntities
                .AsNoTracking()
                .AsQueryable()
                .Where(c => c.Status != EntityStatus.Deleted && !c.Deleted);

            if (request.UserId != null) queryable = queryable.Where(c => c.CreatedBy == request.UserId);
            if (request.PostId != null) queryable = queryable.Where(c => c.PostId == request.PostId);
            if (request.IsGetTopLevel) queryable = queryable.Where(c => c.PostId == null);
            if (request.MajorId != null)
                queryable = queryable
                    .Where(m => m.PostTags
                        .Where(pt => pt.Status != EntityStatus.Deleted && !pt.Deleted)
                        .Select(pt => pt.Tag)
                        .Any(t => t.MajorId == request.MajorId && t.Status != EntityStatus.Deleted && !t.Deleted));
            if (request.PostType == 1)
                queryable = queryable
                    .Where(m => m.PostTags
                        .Where(pt => pt.Status != EntityStatus.Deleted && !pt.Deleted)
                        .Select(pt => pt.Tag)
                        .Any(t => t.MajorId == null && t.Status != EntityStatus.Deleted && !t.Deleted));
            else if (request.PostType == 2)
                queryable = queryable
                    .Where(m => m.PostTags
                        .Where(pt => pt.Status != EntityStatus.Deleted && !pt.Deleted)
                        .Select(pt => pt.Tag)
                        .Any(t => t.MajorId != null && t.Status != EntityStatus.Deleted && !t.Deleted));

            if (!request.SearchString.IsNullOrEmpty())
                queryable = queryable.Where(m => m.Title.ToLower().Contains(request.SearchString!.ToLower()));

            if (request.ListTagId.Count != 0)
                queryable = queryable
                    .Where(p => p.PostTags
                        .Where(pt => pt.Status != EntityStatus.Deleted && !pt.Deleted)
                        .Select(pt => pt.Tag)
                        .Where(t => t.Status != EntityStatus.Deleted && !t.Deleted)
                        .Select(t => t.Id)
                        .Any(tid => request.ListTagId.Contains(tid)));

            //Filter deleted Major
            queryable = queryable
                .Where(p => p.PostTags
                                .Where(pt => pt.Status != EntityStatus.Deleted && !pt.Deleted)
                                .Select(pt => pt.Tag)
                                .Where(t => t.Status != EntityStatus.Deleted && !t.Deleted)
                                .Where(t => t.MajorId != null && !t.Major.Deleted)
                                .Count() > 0
                            ||
                            p.PostTags
                                .Where(pt => pt.Status != EntityStatus.Deleted && !pt.Deleted)
                                .Select(pt => pt.Tag)
                                .Where(t => t.Status != EntityStatus.Deleted && !t.Deleted)
                                .Where(t => t.MajorId == null)
                                .Count() > 0);

            var result = await queryable
                .OrderByDescending(c => c.CreatedTime)
                .ProjectTo<PostDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            foreach (var i in result)
            {
                var user = await _dbContext.UserEntities.AsNoTracking().Where(c => c.Id == i.CreatedBy)
                    .FirstOrDefaultAsync();
                i.CreatedByName = user == null ? "N/A" : user.Username;
                i.CreatedByEmail = user == null ? "N/A" : user.Email;
            }

            return RequestResult<List<PostDTO>>.Succeed(result);
        }
        catch (Exception e)
        {
            return RequestResult<List<PostDTO>>.Fail(_localizationService["List of Post is not found"], new[]
            {
                new ErrorItem
                {
                    Error = e.Message,
                    FieldName = LocalizationString.Common.FailedToGet + "List of Post"
                }
            });
        }
    }

    public async Task<RequestResult<PaginationResponse<PostDTO>>> GetPostWithPaginationAsync(
        PostViewWithPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var queryable = _dbContext.PostEntities
                .AsNoTracking()
                .AsQueryable()
                .Where(c => c.Status != EntityStatus.Deleted && !c.Deleted);

            if (request.UserId != null) queryable = queryable.Where(c => c.CreatedBy == request.UserId);
            if (request.PostId != null) queryable = queryable.Where(c => c.PostId == request.PostId);
            if (request.IsGetTopLevel) queryable = queryable.Where(c => c.PostId == null);
            if (request.MajorId != null)
                queryable = queryable
                    .Where(m => m.PostTags
                        .Where(pt => pt.Status != EntityStatus.Deleted && !pt.Deleted)
                        .Select(pt => pt.Tag)
                        .Any(t => t.MajorId == request.MajorId && t.Status != EntityStatus.Deleted && !t.Deleted));

            if (request.PostType == 1)
                queryable = queryable
                    .Where(m => m.PostTags
                        .Where(pt => pt.Status != EntityStatus.Deleted && !pt.Deleted)
                        .Select(pt => pt.Tag)
                        .Any(t => t.MajorId == null && t.Status != EntityStatus.Deleted && !t.Deleted));
            else if (request.PostType == 2)
                queryable = queryable
                    .Where(m => m.PostTags
                        .Where(pt => pt.Status != EntityStatus.Deleted && !pt.Deleted)
                        .Select(pt => pt.Tag)
                        .Any(t => t.MajorId != null && t.Status != EntityStatus.Deleted && !t.Deleted));

            if (!request.SearchString.IsNullOrEmpty())
                queryable = queryable.Where(m => m.Title.ToLower().Contains(request.SearchString!.ToLower()));

            if (request.ListTagId.Count != 0)
                queryable = queryable
                    .Where(p => p.PostTags
                        .Where(pt => pt.Status != EntityStatus.Deleted && !pt.Deleted)
                        .Select(pt => pt.Tag)
                        .Where(t => t.Status != EntityStatus.Deleted && !t.Deleted)
                        .Select(t => t.Id)
                        .Any(tid => request.ListTagId.Contains(tid)));

            //Filter deleted Major
            queryable = queryable
                .Where(p => p.PostTags
                                .Where(pt => pt.Status != EntityStatus.Deleted && !pt.Deleted)
                                .Select(pt => pt.Tag)
                                .Where(t => t.Status != EntityStatus.Deleted && !t.Deleted)
                                .Where(t => t.MajorId != null && !t.Major.Deleted)
                                .Count() > 0
                            ||
                            p.PostTags
                                .Where(pt => pt.Status != EntityStatus.Deleted && !pt.Deleted)
                                .Select(pt => pt.Tag)
                                .Where(t => t.Status != EntityStatus.Deleted && !t.Deleted)
                                .Where(t => t.MajorId == null)
                                .Count() > 0);

            var result = await queryable
                .OrderByDescending(c => c.CreatedTime)
                .PaginateAsync<PostEntity, PostDTO>(request, _mapper, cancellationToken);

            if (result.Data != null)
                foreach (var i in result.Data)
                {
                    var user = await _dbContext.UserEntities.AsNoTracking().Where(c => c.Id == i.CreatedBy)
                        .FirstOrDefaultAsync();
                    i.CreatedByName = user == null ? "N/A" : user.Username;
                    i.CreatedByEmail = user == null ? "N/A" : user.Email;
                }

            return RequestResult<PaginationResponse<PostDTO>>.Succeed(new PaginationResponse<PostDTO>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                HasNext = result.HasNext,
                Data = result.Data
            });
        }
        catch (Exception e)
        {
            return RequestResult<PaginationResponse<PostDTO>>.Fail(_localizationService["List of Post is not found"],
                new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "List of Post"
                    }
                });
        }
    }

    public async Task<RequestResult<PostDTO?>> GetPostByIdAsync(Guid idPost, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _dbContext.PostEntities
                .AsNoTracking()
                .Where(x => x.Id == idPost && !x.Deleted)
                .ProjectTo<PostDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            var user = await _dbContext.UserEntities.AsNoTracking().Where(c => c.Id == result.CreatedBy)
                .Select(c => c.Username).FirstOrDefaultAsync();
            result.CreatedByName = user ?? "N/A";

            return RequestResult<PostDTO?>.Succeed(result);
        }
        catch (Exception e)
        {
            return RequestResult<PostDTO?>.Fail(_localizationService["Post is not found"], new[]
            {
                new ErrorItem
                {
                    Error = e.Message,
                    FieldName = LocalizationString.Common.FailedToGet + "Post"
                }
            });
        }
    }
}