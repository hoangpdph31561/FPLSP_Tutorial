using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSPTutorial.Application.DataTransferObjects.Example;
using FPLSPTutorial.Application.DataTransferObjects.Example.Request;
using FPLSPTutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSPTutorial.Application.Interfaces.Services;
using FPLSPTutorial.Application.ValueObjects.Common;
using FPLSPTutorial.Application.ValueObjects.Pagination;
using FPLSPTutorial.Application.ValueObjects.Respone;
using FPLSPTutorial.Domain.Entities;
using FPLSPTutorial.Infrastructure.Database.AppDbContext;
using FPLSPTutorial.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FPLSPTutorial.Infrastructure.Implements.Repositories.ReadOnly
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
                IQueryable<ExampleEntity> queryable = _exampleEntities.AsNoTracking().AsQueryable();
                var result = await _exampleEntities.AsNoTracking()
                    .PaginateAsync<ExampleEntity, ExampleDto>(request, _mapper, cancellationToken);

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
