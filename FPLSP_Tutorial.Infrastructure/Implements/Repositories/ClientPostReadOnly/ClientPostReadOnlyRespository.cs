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
using FPLSP_Tutorial.Infrastructure.Database.AppDbContext;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var result = await (from post in _readOnlyDbContext.PostEntities
                             join postTag in _readOnlyDbContext.PostTagEntities on post.Id equals postTag.PostId
                             join tag in _readOnlyDbContext.TagEntities on postTag.TagId equals tag.Id
                             join user in _readOnlyDbContext.UserEntities on post.CreatedBy equals user.Id
                             where tag.MajorId == request.MajorId && !post.Deleted
                             orderby post.CreatedTime descending
                             select new ClientPostListResponse
                             {
                                 Id = post.Id,
                                 Title = post.Title,
                                 Status = post.Status,
                                 CreatedTime = post.CreatedTime,
                                 CreatedBy =  post.CreatedBy,
                                 UserCreatedName = user.Username
                             }).AsNoTracking().ToListAsync(cancellationToken);
                
                PaginationResponse<ClientPostListResponse> response = new()
                {
                    Data = result,
                };
                return RequestResult<PaginationResponse<ClientPostListResponse>>.Succeed(new PaginationResponse<ClientPostListResponse>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = response.HasNext,
                    Data = response.Data
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

        public async Task<RequestResult<PaginationResponse<ClientPostListResponse>>> GetClientPostBySearchAsync(ClientPostSearchRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await (from post in _readOnlyDbContext.PostEntities
                             join postTag in _readOnlyDbContext.PostTagEntities on post.Id equals postTag.PostId
                             join tag in _readOnlyDbContext.TagEntities on postTag.TagId equals tag.Id
                             join user in _readOnlyDbContext.UserEntities on post.CreatedBy equals user.Id
                             where tag.MajorId == request.MajorId && 
                             !post.Deleted && 
                             (request.LstTags == null || request.LstTags.Count == 0 || request.LstTags.Contains(tag.Id)) 
                             && (request.NamePost == null || post.Title.ToLower().Trim().Contains(request.NamePost.ToLower().Trim()))
                             orderby post.CreatedTime descending
                             select  new ClientPostListResponse
                             {
                                 Id = post.Id,
                                 Title = post.Title,
                                 Status = post.Status,
                                 CreatedTime = post.CreatedTime,
                                 CreatedBy = post.CreatedBy,
                                 UserCreatedName = user.Username
                             }).AsNoTracking().ToListAsync(cancellationToken);
                PaginationResponse<ClientPostListResponse> response = new()
                {
                    Data =result,
                };
                return RequestResult<PaginationResponse<ClientPostListResponse>>.Succeed(new PaginationResponse<ClientPostListResponse>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = response.HasNext,
                    Data = response.Data
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
                var userPostName = await _readOnlyDbContext.UserEntities.AsNoTracking().Where(x => x.Id == result.CreatedBy).FirstOrDefaultAsync(cancellationToken);
                result.UserCreatedName = userPostName.Username;
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

        public async Task<RequestResult<PaginationResponse<ClientPostParentChildResponse>>> GetParentChildPostAsync(ClientPostRequestIdWithPagination request, CancellationToken cancellationToken)
        {
            try
            {
                var query = await _readOnlyDbContext.PostEntities.AsNoTracking().Where(x => x.Id == request.Id && !x.Deleted).FirstOrDefaultAsync(cancellationToken);
                ClientPostParentChildResponse response = new();
                if (query.PostId == null)
                {
                    response.ParentPost = _mapper.Map<ClientPostDTO>(query);
                }
                else
                {
                    var parentPost = await _readOnlyDbContext.PostEntities.AsNoTracking().Where(x => x.Id == query.PostId).FirstOrDefaultAsync(cancellationToken);
                    response.ParentPost = _mapper.Map<ClientPostDTO>(parentPost);
                }
                var childPosts = await _readOnlyDbContext.PostEntities.AsNoTracking().Where(x => x.Id != query.Id && !x.Deleted && x.PostId == response.ParentPost.Id).OrderByDescending(x => x.CreatedTime).ToListAsync(cancellationToken);

                response.ChildPosts = _mapper.Map<List<ClientPostDTO>>(childPosts);
                PaginationResponse<ClientPostParentChildResponse> result = new()
                {
                    Data = (ICollection<ClientPostParentChildResponse>)response
                };
                return RequestResult<PaginationResponse<ClientPostParentChildResponse>>.Succeed(new PaginationResponse<ClientPostParentChildResponse>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
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

        public async Task<RequestResult<PaginationResponse<ClientTagResponse>>> GetPostTagsAsync(ClientPostRequestIdWithPagination request, CancellationToken cancellationToken)
        {
            try
            {
                var tagsOfPost = await (from post in _readOnlyDbContext.PostEntities
                                  join postTag in _readOnlyDbContext.PostTagEntities on post.Id equals postTag.PostId
                                  join tag in _readOnlyDbContext.TagEntities on postTag.TagId equals tag.Id
                                  where post.Id == request.Id
                                  select new ClientTagResponse
                                  {
                                      Id = tag.Id,
                                      Name = tag.Name,
                                  }).AsNoTracking().ToListAsync(cancellationToken);
                PaginationResponse<ClientTagResponse> result = new()
                {
                    Data = tagsOfPost
                };
                return RequestResult<PaginationResponse<ClientTagResponse>>.Succeed(new PaginationResponse<ClientTagResponse>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });

            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<ClientTagResponse>>.Fail(_localizationService["List of tags cannot found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of tags"
                    }
                });
            }
        }
    }
}
