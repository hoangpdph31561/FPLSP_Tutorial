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
using FPLSP_Tutorial.Domain.Enums;
using FPLSP_Tutorial.Infrastructure.Database.AppDbContext;
using FPLSP_Tutorial.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        public async Task<RequestResult<MajorDTO?>> GetMajorByIdAsync(Guid idMajor, CancellationToken cancellationToken)
        {
            try
            {
                var major = await _majorEntities.AsNoTracking().ProjectTo<MajorDTO>(_mapper.ConfigurationProvider)
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
                        FieldName = LocalizationString.Common.FailedToGet + "major"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<MajorDTO>>> GetMajorWithPaginationByAdminAsync(ViewMajorWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var queryable = _majorEntities.AsNoTracking();

                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    queryable = queryable.Where(c => c.Name.Contains(request.Name));
                }

                queryable = queryable.Where(c => c.Status != EntityStatus.Deleted);

                var result = await queryable.PaginateAsync<MajorEntity, MajorDTO>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<MajorDTO>>.Succeed(new PaginationResponse<MajorDTO>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<MajorDTO>>.Fail(_localizationService["List of major are not found"], new[]
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
