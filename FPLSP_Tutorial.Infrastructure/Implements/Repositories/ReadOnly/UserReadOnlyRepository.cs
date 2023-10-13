﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Tutorial.Application.DataTransferObjects.User;
using FPLSP_Tutorial.Application.DataTransferObjects.User.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;
using FPLSP_Tutorial.Infrastructure.Database.AppDbContext;
using FPLSP_Tutorial.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadOnly
{
    public class UserReadOnlyRepository : IUserReadOnlyRepository
    {
        private readonly DbSet<UserEntity> _userEntities;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public UserReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _userEntities = dbContext.Set<UserEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<UserDTO?>> GetUserByIdAsync(Guid idUser, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userEntities.AsNoTracking().Where(c => c.Id == idUser && !c.Deleted).ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
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

        public async Task<RequestResult<PaginationResponse<UserDTO>>> GetUserWithPaginationByAdminAsync(ViewUserWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<UserEntity> queryable = _userEntities.AsNoTracking().AsQueryable();
                var result = await _userEntities.AsNoTracking()
                    .PaginateAsync<UserEntity, UserDTO>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<UserDTO>>.Succeed(new PaginationResponse<UserDTO>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<UserDTO>>.Fail(_localizationService["List of User are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of user"
                    }
                });
            }
        }
    }
}