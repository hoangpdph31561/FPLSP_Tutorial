using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Tutorial.Application.DataTransferObjects.UserMajor;
using FPLSP_Tutorial.Application.DataTransferObjects.UserMajor.Request;
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

public class UserMajorReadOnlyRepository : IUserMajorReadOnlyRepository
{
    private readonly AppReadOnlyDbContext _dbContext;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public UserMajorReadOnlyRepository(IMapper mapper, ILocalizationService localizationService,
        AppReadOnlyDbContext dbContext)
    {
        _mapper = mapper;
        _localizationService = localizationService;
        _dbContext = dbContext;
    }

    public async Task<RequestResult<List<UserMajorDTO>>> GetMajorUserAsync(UserMajorViewRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var queryable = await _dbContext.UserMajorEntities
                .AsNoTracking()
                .Where(c => c.Status != EntityStatus.Deleted && !c.Deleted)
                .ProjectTo<UserMajorDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            ;


            return RequestResult<List<UserMajorDTO>>.Succeed(queryable);
        }
        catch (Exception e)
        {
            return RequestResult<List<UserMajorDTO>>.Fail(_localizationService["List of UserMajor are not found"], new[]
            {
                new ErrorItem
                {
                    Error = e.Message,
                    FieldName = LocalizationString.Common.FailedToGet + "list of UserMajor"
                }
            });
        }
    }

    public async Task<RequestResult<PaginationResponse<UserMajorDTO>>> GetMajorUserWithPaginationAsync(
        UserMajorViewWithPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var queryable = _dbContext.UserMajorEntities
                .AsNoTracking()
                .Where(c => c.Status != EntityStatus.Deleted && !c.Deleted);


            var result =
                await queryable.PaginateAsync<UserMajorEntity, UserMajorDTO>(request, _mapper, cancellationToken);
            return RequestResult<PaginationResponse<UserMajorDTO>>.Succeed(new PaginationResponse<UserMajorDTO>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                HasNext = result.HasNext,
                Data = result.Data
            });
        }
        catch (Exception e)
        {
            return RequestResult<PaginationResponse<UserMajorDTO>>.Fail(
                _localizationService["List of UserMajor are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of UserMajor"
                    }
                });
        }
    }
}