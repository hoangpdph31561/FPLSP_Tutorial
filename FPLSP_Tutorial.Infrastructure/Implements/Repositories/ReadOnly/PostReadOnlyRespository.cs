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
using FPLSP_Tutorial.Infrastructure.Database.AppDbContext;
using FPLSP_Tutorial.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadOnly
{
    public class PostReadOnlyRespository : IPostReadOnlyRespository
    {
        private readonly AppReadOnlyDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public PostReadOnlyRespository(AppReadOnlyDbContext apDbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _appDbContext = apDbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<PaginationResponse<PostDto>>> GetPostWithPaginationAsync(ViewPostWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<PostEntity> queryable = _appDbContext.PostEntities.AsNoTracking().AsQueryable().Where(c => !c.Deleted);
                if(request.PostId != null) { queryable = queryable.Where(c => c.PostId == request.PostId); }
                var result = await queryable.PaginateAsync<PostEntity, PostDto>(request, _mapper, cancellationToken);
                return RequestResult<PaginationResponse<PostDto>>.Succeed(new PaginationResponse<PostDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<RequestResult<PostDto?>> GetPostByIdAsync(Guid postId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _appDbContext.PostEntities.AsNoTracking().Where(x => x.Id == postId && !x.Deleted)
                    .ProjectTo<PostDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
                
                return RequestResult<PostDto?>.Succeed(result);

            }
            catch (Exception e)
            {

                return RequestResult<PostDto?>.Fail(_localizationService["Post cannot found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "example"
                    }
                });
            }
        }
    }
}
