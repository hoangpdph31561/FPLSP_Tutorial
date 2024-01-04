using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
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

namespace FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadOnly;

public class TagReadOnlyRepository : ITagReadOnlyRepository
{
    private readonly AppReadOnlyDbContext _dbContext;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public TagReadOnlyRepository(AppReadOnlyDbContext dbContext, IMapper mapper,
        ILocalizationService localizationService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _localizationService = localizationService;
    }

    public async Task<RequestResult<List<TagDTO>>> GetTagAsync(TagViewRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = _dbContext.TagEntities
                .AsNoTracking()
                .AsQueryable()
                .Where(c => c.Status != EntityStatus.Deleted && !c.Deleted);

            if (!request.IgnoreMajorId) query = query.Where(c => c.MajorId == request.MajorId);

            if (request.Name != null) query = query.Where(c => c.Name.ToLower().Contains(request.Name));

            return RequestResult<List<TagDTO>>.Succeed(
                await query
                    .ProjectTo<TagDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken));
        }
        catch (Exception e)
        {
            return RequestResult<List<TagDTO>>.Fail(_localizationService["List of Tag is not found"], new[]
            {
                new ErrorItem
                {
                    Error = e.Message,
                    FieldName = LocalizationString.Common.FailedToGet + "List of Tag"
                }
            });
        }
    }

    public async Task<RequestResult<PaginationResponse<TagDTO>>> GetTagWithPaginationAsync(
        TagViewWithPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _dbContext.TagEntities
                .AsNoTracking()
                .AsQueryable()
                .Where(c => c.Status != EntityStatus.Deleted && !c.Deleted);

            query = query.Where(c => c.MajorId == request.MajorId);
            if (request.Name != null) query = query.Where(c => c.Name.ToLower().Contains(request.Name));

            var result = await query.PaginateAsync<TagEntity, TagDTO>(request, _mapper, cancellationToken);
            return RequestResult<PaginationResponse<TagDTO>>.Succeed(new PaginationResponse<TagDTO>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                HasNext = result.HasNext,
                Data = result.Data
            });
        }
        catch (Exception e)
        {
            return RequestResult<PaginationResponse<TagDTO>>.Fail(_localizationService["List of Tag is not found"],
                new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "List of Tag"
                    }
                });
        }
    }

    public async Task<RequestResult<TagDTO?>> GetTagByIdAsync(Guid idTag, CancellationToken cancellationToken)
    {
        try
        {
            var tag = await _dbContext.TagEntities
                .AsNoTracking()
                .Where(c => c.Id == idTag && !c.Deleted)
                .ProjectTo<TagDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return RequestResult<TagDTO?>.Succeed(tag);
        }
        catch (Exception e)
        {
            return RequestResult<TagDTO?>.Fail(_localizationService["Tag is not found"], new[]
            {
                new ErrorItem
                {
                    Error = e.Message,
                    FieldName = LocalizationString.Common.FailedToGet + "Tag"
                }
            });
        }
    }

    public async Task<RequestResult<List<TagDTO>>> GetTagByIdMajorAsync(Guid? MajorId,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = _dbContext.TagEntities
                .AsNoTracking()
                .AsQueryable()
                .Where(c => !c.Deleted && c.MajorId == MajorId);

            return RequestResult<List<TagDTO>>.Succeed(
                await query
                    .ProjectTo<TagDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken));
        }
        catch (Exception e)
        {
            return RequestResult<List<TagDTO>>.Fail(_localizationService["List of Tag is not found"], new[]
            {
                new ErrorItem
                {
                    Error = e.Message,
                    FieldName = LocalizationString.Common.FailedToGet + "List of Tag"
                }
            });
        }
    }
}