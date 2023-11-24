using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Tutorial.Application.DataTransferObjects.Post;
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Response;
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

namespace FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadOnly
{
    public class PostReadOnlyRepository : IPostReadOnlyRespository
    {
        private readonly AppReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public PostReadOnlyRepository(AppReadOnlyDbContext dbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<List<PostDTO>>> GetPostAsync(PostViewRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<PostEntity> queryable = _dbContext.PostEntities
                    .AsNoTracking()
                    .AsQueryable()
                    .Where(c => c.Status != EntityStatus.Deleted && !c.Deleted);

                var result = await queryable
                    .ProjectTo<PostDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return RequestResult<List<PostDTO>>.Succeed(result);
            }
            catch (Exception e)
            {
                return RequestResult<List<PostDTO>>.Fail(_localizationService["List of Post is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "List of Post"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<PostDTO>>> GetPostWithPaginationAsync(PostViewWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<PostEntity> queryable = _dbContext.PostEntities
                    .AsNoTracking()
                    .AsQueryable()
                    .Where(c => c.Status != EntityStatus.Deleted && !c.Deleted);

                var result = await queryable.PaginateAsync<PostEntity, PostDTO>(request, _mapper, cancellationToken);
                return RequestResult<PaginationResponse<PostDTO>>.Succeed(new PaginationResponse<PostDTO>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<PostDTO>>.Fail(_localizationService["List of Post is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "List of Post"
                    }
                });
            }
        }

        public async Task<RequestResult<PostDTO?>> GetPostByIdAsync(Guid idPost, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _dbContext.PostEntities
                    .AsNoTracking()
                    .Where(x => x.Id == idPost && !x.Deleted)
                    .ProjectTo<PostDTO>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
                
                return RequestResult<PostDTO?>.Succeed(result);

            }
            catch (Exception e)
            {

                return RequestResult<PostDTO?>.Fail(_localizationService["Post is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "Post"
                    }
                });
            }
        }

        
    }
}
