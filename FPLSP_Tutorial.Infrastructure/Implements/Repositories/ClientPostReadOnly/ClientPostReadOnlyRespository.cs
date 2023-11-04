using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Response;

using FPLSP_Tutorial.Application.Interfaces.Repositories.ClientPostReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;
using FPLSP_Tutorial.Infrastructure.Database.AppDbContext;
using FPLSP_Tutorial.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FPLSP_Tutorial.Infrastructure.Implements.Repositories.ClientPostReadOnly
{

    public class ClientPostReadOnlyRespository : IClientPostReadOnlyRespository
    {
        private readonly AppReadOnlyDbContext _readOnlyDbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public ClientPostReadOnlyRespository(AppReadOnlyDbContext readOnlyDbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _readOnlyDbContext = readOnlyDbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<PaginationResponse<ClientPostListResponse>>> GetAllClientPostListAsync(ClientPostListRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var groupPostId = await _readOnlyDbContext.TagEntities.AsNoTracking().Where(x => x.MajorId == request.MajorId).Select(x => x.PostTags.Select(pt => pt.PostId).ToList()).ToListAsync(cancellationToken);

                List<Guid> lstPostId = new List<Guid>();
                foreach (var post in groupPostId)
                {
                    lstPostId.AddRange(post);
                }

                var test = await _readOnlyDbContext.PostEntities.AsNoTracking().Where(x => lstPostId.Contains(x.Id) && !x.Deleted).PaginateAsync<PostEntity, ClientPostListResponse>(request, _mapper, cancellationToken);
                foreach (var entity in test.Data!)
                {
                    var createdName = await _readOnlyDbContext.UserEntities.AsNoTracking().Where(x => x.Id == entity.CreatedBy).Select(x => x.Username).FirstOrDefaultAsync(cancellationToken);
                    entity.UserCreatedName = createdName == null ? "N/A" : createdName!;
                }

                return RequestResult<PaginationResponse<ClientPostListResponse>>.Succeed(new PaginationResponse<ClientPostListResponse>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = test.HasNext,
                    Data = test.Data
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<ClientPostListResponse>>.Fail(_localizationService["List of post cannot found"], new[]
               {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of posts"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<ClientPost_MajorDTO>>> GetAllMajorAsync(ClientPost_GetMajorRequestWithPagination request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _readOnlyDbContext.MajorEntities.AsNoTracking().PaginateAsync<MajorEntity, ClientPost_MajorDTO>(request, _mapper, cancellationToken);
                return RequestResult<PaginationResponse<ClientPost_MajorDTO>>.Succeed(new PaginationResponse<ClientPost_MajorDTO>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<ClientPost_MajorDTO>>.Fail(_localizationService["List of Major cannot found"], new[]
                {
                    new ErrorItem
                    {
                        Error= e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of Major"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<ClientPostListResponse>>> GetClientPostBySearchAsync(ClientPostSearchRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var groupPostId = await _readOnlyDbContext.TagEntities.Where(x => x.MajorId == request.MajorId && request.LstTags!.Contains(x.Id)).Select(x => x.PostTags.Select(pt => pt.PostId).ToList()).ToListAsync(cancellationToken);
                List<Guid> lstPostId = new List<Guid>();
                foreach (var post in groupPostId)
                {
                    lstPostId.AddRange(post);
                }
                var result = await _readOnlyDbContext.PostEntities.AsNoTracking().Where(x => lstPostId.Contains(x.Id) && x.Title.ToLower().Trim().Contains(request.NamePost.ToLower().Trim())).PaginateAsync<PostEntity, ClientPostListResponse>(request, _mapper, cancellationToken);

                foreach (var item in result.Data!)
                {
                    var createdName = await _readOnlyDbContext.UserEntities.AsNoTracking().Where(x => x.Id == item.Id).Select(x => x.Username).FirstOrDefaultAsync(cancellationToken);
                    item.UserCreatedName = createdName == null ? "N/A" : createdName!;
                }
                return RequestResult<PaginationResponse<ClientPostListResponse>>.Succeed(new PaginationResponse<ClientPostListResponse>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<ClientPostListResponse>>.Fail(_localizationService["List of post cannot found"], new[]
               {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of posts"
                    }
                });
            }
        }

        public async Task<RequestResult<ClientPostDetailResponse>> GetClientPostDetailAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _readOnlyDbContext.PostEntities.AsNoTracking().Where(x => x.Id == id && !x.Deleted).ProjectTo<ClientPostDetailResponse>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);

                result!.UserCreatedName = await _readOnlyDbContext.UserEntities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == result.CreatedBy) == null ? "N/A" : _readOnlyDbContext.UserEntities.AsNoTracking().First(x => x.Id == result.CreatedBy).Username;
                return RequestResult<ClientPostDetailResponse>.Succeed(result);
            }
            catch (Exception e)
            {

                return RequestResult<ClientPostDetailResponse>.Fail(_localizationService["Post is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "post"
                    }
                });
            }
        }

        public async Task<RequestResult<MajorRequestEntity>> GetMajorRequestByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _readOnlyDbContext.MajorRequestEntities.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
                return RequestResult<MajorRequestEntity>.Succeed(result);
            }
            catch (Exception e)
            {

                return RequestResult<MajorRequestEntity>.Fail(_localizationService["Example is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "example"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<ClientPostParentChildResponse>>> GetParentChildPostAsync(ClientPostRequestIdWithPagination request, CancellationToken cancellationToken)
        {
            try
            {
                //IQueryable<PostEntity> queryable = _readOnlyDbContext.PostEntities.AsNoTracking().Where(x => x.Id == request.Id && !x.Deleted).AsQueryable();
                var result_Test = await _readOnlyDbContext.PostEntities.AsNoTracking().Where(x => x.Id == request.Id && !x.Deleted).PaginateAsync<PostEntity, ClientPostParentChildResponse>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<ClientPostParentChildResponse>>.Succeed(new PaginationResponse<ClientPostParentChildResponse>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result_Test.HasNext,
                    Data = result_Test.Data
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<ClientPostParentChildResponse>>.Fail(_localizationService["List of parent post child cannot found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of parent child posts"
                    }
                });

            }


        }

        public async Task<RequestResult<ClientTagResponse>> GetPostTagsAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _readOnlyDbContext.PostEntities.AsNoTracking().Where(x => x.Id == id).ProjectTo<ClientTagResponse>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
                return RequestResult<ClientTagResponse>.Succeed(result);

            }
            catch (Exception e)
            {

                return RequestResult<ClientTagResponse>.Fail(_localizationService["Tags of Post are not found"], new[]
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
