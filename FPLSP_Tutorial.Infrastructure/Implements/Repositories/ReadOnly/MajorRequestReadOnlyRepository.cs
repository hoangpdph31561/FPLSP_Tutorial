using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;
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

public class MajorRequestReadOnlyRepository : IMajorRequestReadOnlyRepository
{
    private readonly AppReadOnlyDbContext _dbContext;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public MajorRequestReadOnlyRepository(IMapper mapper, ILocalizationService localizationService,
        AppReadOnlyDbContext dbContext)
    {
        _mapper = mapper;
        _localizationService = localizationService;
        _dbContext = dbContext;
    }

    public async Task<RequestResult<List<MajorRequestDTO>>> GetMajorRequestAsync(MajorRequestViewRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var queryable = _dbContext.MajorRequestEntities
                .AsNoTracking()
                .AsQueryable()
                .Where(c => c.Status != EntityStatus.Deleted && !c.Deleted);

            var result = await queryable
                .ProjectTo<MajorRequestDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return RequestResult<List<MajorRequestDTO>>.Succeed(result);
        }
        catch (Exception e)
        {
            return RequestResult<List<MajorRequestDTO>>.Fail(_localizationService["List of MajorRequest are not found"],
                new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "List of MajorRequest"
                    }
                });
        }
    }

    public async Task<RequestResult<PaginationResponse<MajorRequestDTO>>> GetMajorRequestWithPaginationAsync(
        MajorRequestViewWithPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var queryable = _dbContext.MajorRequestEntities
                .AsNoTracking()
                .AsQueryable()
                .Where(c => c.Status != EntityStatus.Deleted && !c.Deleted);

            //User exist in DB only
            queryable = queryable.Where(mr =>
                _dbContext.UserEntities.Any(u => u.Id == mr.CreatedBy && u.Status != EntityStatus.Deleted));

            var result = await queryable
                .PaginateAsync<MajorRequestEntity, MajorRequestDTO>(request, _mapper, cancellationToken);

            if (result.Data != null)
                foreach (var dto in result.Data)
                {
                    var user = await _dbContext.UserEntities.FirstOrDefaultAsync(u => u.Id == dto.CreatedBy);
                    dto.CreatedByEmail = user == null ? "<N/A>" : user.Email;
                    dto.CreatedByName = user == null ? "<N/A>" : user.Username;
                }

            return RequestResult<PaginationResponse<MajorRequestDTO>>.Succeed(new PaginationResponse<MajorRequestDTO>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                HasNext = result.HasNext,
                Data = result.Data
            });
        }
        catch (Exception e)
        {
            return RequestResult<PaginationResponse<MajorRequestDTO>>.Fail(
                _localizationService["List of MajorRequest is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of MajorRequest"
                    }
                });
        }
    }

    public async Task<RequestResult<MajorRequestDTO?>> GetMajorRequestByIdAsync(Guid idMajorRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            var MajorRequest = await _dbContext.MajorRequestEntities
                .AsNoTracking()
                .Where(c => c.Id == idMajorRequest && !c.Deleted)
                .ProjectTo<MajorRequestDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return RequestResult<MajorRequestDTO?>.Succeed(MajorRequest);
        }
        catch (Exception e)
        {
            return RequestResult<MajorRequestDTO?>.Fail(_localizationService["MajorRequest is not found"], new[]
            {
                new ErrorItem
                {
                    Error = e.Message,
                    FieldName = LocalizationString.Common.FailedToGet + "MajorRequest"
                }
            });
        }
    }
}