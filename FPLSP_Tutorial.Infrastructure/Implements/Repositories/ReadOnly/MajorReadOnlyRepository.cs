using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Tutorial.Application.DataTransferObjects.Major;
using FPLSP_Tutorial.Application.DataTransferObjects.Major.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Enums;
using FPLSP_Tutorial.Infrastructure.Database.AppDbContext;
using FPLSP_Tutorial.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadOnly;

public class MajorReadOnlyRepository : IMajorReadOnlyRepository
{
    private readonly AppReadOnlyDbContext _dbContext;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public MajorReadOnlyRepository(IMapper mapper, ILocalizationService localizationService,
        AppReadOnlyDbContext dbContext)
    {
        _mapper = mapper;
        _localizationService = localizationService;
        _dbContext = dbContext;
    }

    public async Task<RequestResult<List<MajorDTO>>> GetMajorAsync(MajorViewRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var queryable = _dbContext.MajorEntities
                .AsNoTracking()
                .AsQueryable()
                .Where(c => c.Status != EntityStatus.Deleted && !c.Deleted);

            if (request.UserId != null)
            {
                if (request.NotJoined)
                {
                    //NotJoined
                    queryable = queryable.Where(m => !m.UserMajors.Any(um =>
                        um.UserId == request.UserId && um.Status != EntityStatus.Deleted && !um.Deleted));
                    //NotHavingRequest
                    queryable = queryable.Where(m => !m.MajorRequests.Any(mr =>
                        mr.CreatedBy == request.UserId && mr.Status != EntityStatus.Deleted && !mr.Deleted));
                }
                else
                {
                    queryable = queryable.Where(m =>
                        m.UserMajors
                            .Where(um =>
                                um.UserId == request.UserId && um.Status != EntityStatus.Deleted && !um.Deleted)
                            .Any(um => um.MajorId == m.Id));
                }
            }

            if (request.ContainPostOnly)
                queryable = queryable.Where(m =>
                    m.Tags.Where(t => t.Status != 0 && !t.Deleted).SelectMany(t => t.PostTags).Select(pt => pt.Post)
                        .Any(p => p.Status != 0 && !p.Deleted));

            if (!request.SearchString.IsNullOrEmpty())
                queryable = queryable.Where(m =>
                    m.Name.ToLower().Contains(request.SearchString!.ToLower()) ||
                    m.Code.ToLower().Contains(request.SearchString!.ToLower()));

            var result = await queryable
                .ProjectTo<MajorDTO>(_mapper.ConfigurationProvider)
                .SortData(request.SortingProperty, request.SortingDirection)
                .ToListAsync();

            if (request.UserId != null && result != null)
                foreach (var dto in result)
                {
                    dto.NumberOfPostByUser = _dbContext.PostEntities.AsNoTracking().AsQueryable().Where(c =>
                            c.CreatedBy == request.UserId &&
                            c.PostTags.Select(pt => pt.Tag).Any(t => t.MajorId == dto.Id))
                        .Count();
                    dto.IsManagerUser = await _dbContext.UserMajorEntities.AsNoTracking().AsQueryable()
                        .Where(c => c.UserId == request.UserId && c.MajorId == dto.Id &&
                                    c.Status != EntityStatus.Deleted && !c.Deleted).Select(c => c.IsManager)
                        .FirstOrDefaultAsync();
                    dto.HasSentRequest = _dbContext.MajorRequestEntities.AsNoTracking().AsQueryable().Any(c =>
                        c.CreatedBy == request.UserId && c.MajorId == dto.Id && c.Status != EntityStatus.Deleted &&
                        !c.Deleted);
                }

            return RequestResult<List<MajorDTO>>.Succeed(result);
        }
        catch (Exception e)
        {
            return RequestResult<List<MajorDTO>>.Fail(_localizationService["List of Major is not found"], new[]
            {
                new ErrorItem
                {
                    Error = e.Message,
                    FieldName = LocalizationString.Common.FailedToGet + "List of Major"
                }
            });
        }
    }

    public async Task<RequestResult<PaginationResponse<MajorDTO>>> GetMajorWithPaginationAsync(
        MajorViewWithPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var queryable = _dbContext.MajorEntities
                .AsNoTracking()
                .AsQueryable()
                .Where(c => c.Status != EntityStatus.Deleted && !c.Deleted);

            if (request.UserId != null)
            {
                if (request.NotJoined)
                {
                    //NotJoined
                    queryable = queryable.Where(m => !m.UserMajors.Any(um =>
                        um.UserId == request.UserId && um.Status != EntityStatus.Deleted && !um.Deleted));
                    //NotHavingRequest
                    queryable = queryable.Where(m => !m.MajorRequests.Any(mr =>
                        mr.CreatedBy == request.UserId && mr.Status != EntityStatus.Deleted && !mr.Deleted));
                }
                else
                {
                    queryable = queryable.Where(m =>
                        m.UserMajors
                            .Where(um =>
                                um.UserId == request.UserId && um.Status != EntityStatus.Deleted && !um.Deleted)
                            .Any(um => um.MajorId == m.Id));
                }
            }

            if (request.ContainPostOnly)
                queryable = queryable.Where(m =>
                    m.Tags.Where(t => t.Status != 0 && !t.Deleted).SelectMany(t => t.PostTags).Select(pt => pt.Post)
                        .Any(p => p.Status != 0 && !p.Deleted));

            if (!request.SearchString.IsNullOrEmpty())
                queryable = queryable.Where(m =>
                    m.Name.ToLower().Contains(request.SearchString!.ToLower()) ||
                    m.Code.ToLower().Contains(request.SearchString!.ToLower()));

            var result = await queryable
                .ProjectTo<MajorDTO>(_mapper.ConfigurationProvider)
                .SortData(request.SortingProperty, request.SortingDirection)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize + 1)
                .ToListAsync(cancellationToken);

            var hasNext = result.Count == request.PageSize + 1;

#warning MAINTAIN: Phải tách pthuc + DTO (CountPostByUser). Cần phải đặt sau skip take (.ToList()) mới orderby đc > chưa đáp ứng đc chức năng view này

            if (request.UserId != null && result != null)
                foreach (var dto in result)
                {
                    dto.NumberOfPostByUser = _dbContext.PostEntities.AsNoTracking().AsQueryable().Where(c =>
                            c.CreatedBy == request.UserId &&
                            c.PostTags.Select(pt => pt.Tag).Any(t => t.MajorId == dto.Id))
                        .Count();
                    dto.IsManagerUser = await _dbContext.UserMajorEntities.AsNoTracking().AsQueryable()
                        .Where(c => c.UserId == request.UserId && c.MajorId == dto.Id &&
                                    c.Status != EntityStatus.Deleted && !c.Deleted).Select(c => c.IsManager)
                        .FirstOrDefaultAsync();
                    dto.HasSentRequest = _dbContext.MajorRequestEntities.AsNoTracking().AsQueryable().Any(c =>
                        c.CreatedBy == request.UserId && c.MajorId == dto.Id && c.Status != EntityStatus.Deleted &&
                        !c.Deleted);
                }

            return RequestResult<PaginationResponse<MajorDTO>>.Succeed(new PaginationResponse<MajorDTO>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                HasNext = hasNext,
                Data = result.Take(request.PageSize).ToList()
            });
        }
        catch (Exception e)
        {
            return RequestResult<PaginationResponse<MajorDTO>>.Fail(_localizationService["List of Major is not found"],
                new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "List of Major"
                    }
                });
        }
    }

    public async Task<RequestResult<MajorDTO?>> GetMajorByIdAsync(Guid idMajor, CancellationToken cancellationToken)
    {
        try
        {
            var major = await _dbContext.MajorEntities
                .AsNoTracking()
                .Where(c => c.Id == idMajor && !c.Deleted)
                .ProjectTo<MajorDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return RequestResult<MajorDTO?>.Succeed(major);
        }
        catch (Exception e)
        {
            return RequestResult<MajorDTO?>.Fail(_localizationService["Major is not found"], new[]
            {
                new ErrorItem
                {
                    Error = e.Message,
                    FieldName = LocalizationString.Common.FailedToGet + "Major"
                }
            });
        }
    }
}