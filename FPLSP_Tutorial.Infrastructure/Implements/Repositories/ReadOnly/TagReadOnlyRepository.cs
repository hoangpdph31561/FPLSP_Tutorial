using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;
using FPLSP_Tutorial.Infrastructure.Database.AppDbContext;
using FPLSP_Tutorial.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadOnly
{
    public class TagReadOnlyRepository : ITagReadOnlyRepository
    {
        private readonly AppReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public TagReadOnlyRepository(AppReadOnlyDbContext dbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<TagDto?>> GetTagByIdAsync(Guid idTag, CancellationToken cancellationToken)
        {
            try
            {
                var tag = await _dbContext.TagEntities.AsNoTracking().Where(c => c.Id == idTag && !c.Deleted).ProjectTo<TagDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                return RequestResult<TagDto?>.Succeed(tag);
            }
            catch (Exception e)
            {
                return RequestResult<TagDto?>.Fail(_localizationService["Tag is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "tag"
                    }
                });
            }
        }

        public async Task<RequestResult<List<TagDto>>> GetTagByIdMajorAsync(Guid? MajorId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _dbContext.TagEntities.AsNoTracking().Where(x => !x.Deleted && x.MajorId == MajorId).ProjectTo<TagDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
                return RequestResult<List<TagDto>>.Succeed(result);
            

            }
            catch (Exception e)
            {
                return RequestResult<List<TagDto>>.Fail(_localizationService["Tags is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "tags"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<TagDto>>> GetTagWithPaginationByAdminAsync(ViewTagWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _dbContext.TagEntities.AsNoTracking()
                    .PaginateAsync<TagEntity, TagDto>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<TagDto>>.Succeed(new PaginationResponse<TagDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<TagDto>>.Fail(_localizationService["List of tag are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of tag"
                    }
                });
            }
        }
    }
}
