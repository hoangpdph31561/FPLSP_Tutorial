﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Tutorial.Application.DataTransferObjects.Major;
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
using static FPLSP_Tutorial.Application.ValueObjects.Common.QueryConstant;

namespace FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadOnly
{
    public class MajorRequestReadOnlyRepository : IMajorRequestReadOnlyRespository
    {
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        private readonly AppReadOnlyDbContext _dbContext;

        public MajorRequestReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _mapper = mapper;
            _localizationService = localizationService;
            _dbContext = dbContext;
        }
        public async Task<RequestResult<MajorRequestDto?>> GetMajorRequestByIdAsync(Guid idMajorRequest, CancellationToken cancellationToken)
        {
            try
            {
                var MajorRequest = await _dbContext.MajorRequestEntities.AsNoTracking().Where(c => c.Id == idMajorRequest && !c.Deleted).ProjectTo<MajorRequestDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                return RequestResult<MajorRequestDto?>.Succeed(MajorRequest);
            }
            catch (Exception e)
            {
                return RequestResult<MajorRequestDto?>.Fail(_localizationService["MajorRequest is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "MajorRequest"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<MajorRequestDto>>> GetMajorRequestWithPaginationByADeletedAsync(ViewMajorRequestWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request.SearchByFields = new List<SearchModel>()
                {
                    new()
                    {
                        SearchFieldName = nameof(MajorRequestEntity.CreatedBy),
                        SearchValue = "6D795437-8325-42AC-C661-08DBDA231D0C",
                        MatchType = MatchTypes.Equal
                    }
                } ;

                // Lấy được result == MajorRequest
                var result = await _dbContext.MajorRequestEntities.AsNoTracking().Where(x => x.Deleted == false && x.Status != EntityStatus.Deleted)

                    .PaginateAsync<MajorRequestEntity, MajorRequestDto>(request, _mapper, cancellationToken);

                // lấy ra List data majorRequest
                List<MajorRequestDto> majorRequest = new List<MajorRequestDto>();
                if (result != null)
                {
                    majorRequest = (List<MajorRequestDto>)result.Data;

                    // từ líst data majorRequest => lấy ra Createby = id User => lấy ra Email
                    foreach (var item in majorRequest)
                    {
                        UserEntity userEntity = _dbContext.UserEntities.AsNoTracking().Where(x => x.Id == item.CreatedBy).FirstOrDefault();
                        if (userEntity != null)
                        {
                            item.Email = userEntity.Email;
                        }
                    }
                  
                }
                return RequestResult<PaginationResponse<MajorRequestDto>>.Succeed(new PaginationResponse<MajorRequestDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data =majorRequest
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<MajorRequestDto>>.Fail(_localizationService["List of MajorRequest are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of MajorRequest"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<MajorRequestDto>>> GetMajorRequestWithPaginationByAdminAsync(ViewMajorRequestWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {

                IQueryable<MajorRequestEntity> queryable = _dbContext.MajorRequestEntities.AsNoTracking().AsQueryable();
                var result = await _dbContext.MajorRequestEntities.AsNoTracking()
                    .PaginateAsync<MajorRequestEntity, MajorRequestDto>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<MajorRequestDto>>.Succeed(new PaginationResponse<MajorRequestDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<MajorRequestDto>>.Fail(_localizationService["List of MajorRequest are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of MajorRequest"
                    }
                });
            }
        }
    }
}
