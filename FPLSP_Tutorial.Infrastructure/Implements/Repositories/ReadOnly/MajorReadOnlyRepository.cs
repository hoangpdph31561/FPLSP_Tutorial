using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Tutorial.Application.DataTransferObjects.Major;
using FPLSP_Tutorial.Application.DataTransferObjects.Major.Request;
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
    public class MajorReadOnlyRepository : IMajorReadOnlyRepository
    {
        private readonly DbSet<MajorEntity> _majorEntities;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public MajorReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _majorEntities = dbContext.Set<MajorEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<MajorDTOs?>> GetMajorByIdAsync(Guid idMajor, CancellationToken cancellationToken)
        {
            try
            {
                var major = await _majorEntities.AsNoTracking().Where(c => c.Id == idMajor && !c.Deleted).ProjectTo<MajorDTOs>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                return RequestResult<MajorDTOs?>.Succeed(major);
            }
            catch (Exception e)
            {
                return RequestResult<MajorDTOs?>.Fail(_localizationService["Major is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "major"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<MajorDTOs>>> GetMajorWithPaginationByAdminAsync(ViewMajorWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<MajorEntity> queryable = _majorEntities.AsNoTracking().AsQueryable();
                var result = await _majorEntities.AsNoTracking()
                    .PaginateAsync<MajorEntity, MajorDTOs>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<MajorDTOs>>.Succeed(new PaginationResponse<MajorDTOs>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<MajorDTOs>>.Fail(_localizationService["List of Major are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of major"
                    }
                });
            }
        }
    }
}
