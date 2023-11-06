using AutoMapper;
using BaseSolution.Infrastructure.Extensions;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;
using FPLSP_Tutorial.Infrastructure.Database.AppDbContext;
using FPLSP_Tutorial.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadOnly
{
    public class UserMajorReadOnlyRespository : IUserMajorReadOnlyRespository
    {

        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        private readonly AppReadOnlyDbContext _dbContext;

        public UserMajorReadOnlyRespository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _mapper = mapper;
            _localizationService = localizationService;
            _dbContext = dbContext;
        }
        public async Task<RequestResult<PaginationResponse<MajorUserDto>>> GetMajorUserWithPaginationByAdminAsync(ViewMajorUserWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<UserMajorEntity> queryable = _dbContext.UserMajorEntities.AsNoTracking().AsQueryable();
                var result = await _dbContext.UserMajorEntities.AsNoTracking()
                    .PaginateAsync<UserMajorEntity, MajorUserDto>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<MajorUserDto>>.Succeed(new PaginationResponse<MajorUserDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<MajorUserDto>>.Fail(_localizationService["List of UserMajor are not found"], new[]
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
}
