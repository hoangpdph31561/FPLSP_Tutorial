using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Tutorial.Application.DataTransferObjects.Example;
using FPLSP_Tutorial.Application.DataTransferObjects.Example.Request;
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
    public class ExampleReadOnlyRepository : IExampleReadOnlyRepository
    {
        private readonly DbSet<ExampleEntity> _exampleEntities;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public ExampleReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, ExampleReadOnlyDbContext dbContext)
        {
            _exampleEntities = dbContext.Set<ExampleEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<ExampleDto?>> GetExampleByIdAsync(Guid idExample, CancellationToken cancellationToken)
        {
            try
            {
                var example = await _exampleEntities.AsNoTracking().Where(c => c.Id == idExample && !c.Deleted).ProjectTo<ExampleDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                return RequestResult<ExampleDto?>.Succeed(example);
            }
            catch (Exception e)
            {
                return RequestResult<ExampleDto?>.Fail(_localizationService["Example is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "example"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<ExampleDto>>> GetExampleWithPaginationByAdminAsync(ViewExampleWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var queryable = _exampleEntities.AsNoTracking();

                // check if search field is null
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    queryable = queryable.Where(c => c.Name.Contains(request.Name!));
                }

                // check if sort field is null
                queryable = queryable.OrderBy(c => c.Status);

                var result = await queryable.PaginateAsync<ExampleEntity, ExampleDto>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<ExampleDto>>.Succeed(new PaginationResponse<ExampleDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<ExampleDto>>.Fail(_localizationService["List of example are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of example"
                    }
                });
            }
        }
    }
}
