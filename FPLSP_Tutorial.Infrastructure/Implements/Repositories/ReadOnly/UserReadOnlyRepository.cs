using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Tutorial.Application.DataTransferObjects.User;
using FPLSP_Tutorial.Application.DataTransferObjects.User.Request;
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

public class UserReadOnlyRepository : IUserReadOnlyRepository
{
    private readonly AppReadOnlyDbContext _dbcontext;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public UserReadOnlyRepository(IMapper mapper, ILocalizationService localizationService,
        AppReadOnlyDbContext dbContext)
    {
        _dbcontext = dbContext;
        _mapper = mapper;
        _localizationService = localizationService;
    }

    public Task<RequestResult<PaginationResponse<UserDTO>>> GetUserAsync(UserViewRequest request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<RequestResult<PaginationResponse<UserDTO>>> GetUserWithPaginationAsync(
        UserViewWithPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var queryable = _dbcontext.UserEntities
                .AsNoTracking()
                .AsQueryable();

            if (!request.SearchString.IsNullOrEmpty())
                queryable = queryable.Where(c =>
                    c.Email.ToLower().Contains(request.SearchString.ToLower()) ||
                    c.Username.ToLower().Contains(request.SearchString.ToLower()));

            var result = await queryable
                .PaginateAsync<UserEntity, UserDTO>(request, _mapper, cancellationToken);

            return RequestResult<PaginationResponse<UserDTO>>.Succeed(new PaginationResponse<UserDTO>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                HasNext = result.HasNext,
                Data = result.Data
            });
        }
        catch (Exception e)
        {
            return RequestResult<PaginationResponse<UserDTO>>.Fail(_localizationService["List of User is not found"],
                new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of User"
                    }
                });
        }
    }

    public async Task<RequestResult<UserDTO?>> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _dbcontext.UserEntities
                .AsNoTracking()
                .Where(c => c.Email == email && c.Status != EntityStatus.Deleted)
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            return RequestResult<UserDTO?>.Succeed(user);
        }
        catch (Exception e)
        {
            return RequestResult<UserDTO?>.Fail(_localizationService["User is not found"], new[]
            {
                new ErrorItem
                {
                    Error = e.Message,
                    FieldName = LocalizationString.Common.FailedToGet + "User"
                }
            });
        }
    }

    public async Task<RequestResult<UserDTO?>> GetUserByIdAsync(Guid idUser, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _dbcontext.UserEntities
                .AsNoTracking()
                .Where(c => c.Id == idUser && c.Status != EntityStatus.Deleted)
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            return RequestResult<UserDTO?>.Succeed(user);
        }
        catch (Exception e)
        {
            return RequestResult<UserDTO?>.Fail(_localizationService["User is not found"], new[]
            {
                new ErrorItem
                {
                    Error = e.Message,
                    FieldName = LocalizationString.Common.FailedToGet + "User"
                }
            });
        }
    }
}