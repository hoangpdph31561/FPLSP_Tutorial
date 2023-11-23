using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request;
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


        public async Task<RequestResult<PaginationResponse<MajorBaseDTO>>> GetAllMajorAsync(ClientPostMajorRequestWithPagination request, CancellationToken cancellationToken)
        {
            try
            {

                var result = await _readOnlyDbContext.MajorEntities.AsNoTracking().Where(x => !x.Deleted).ProjectTo<MajorBaseDTO>(_mapper.ConfigurationProvider).Where(x => x.NumberOfPosts > 0).PaginateAsync(request, cancellationToken);
                return RequestResult<PaginationResponse<MajorBaseDTO>>.Succeed(new PaginationResponse<MajorBaseDTO>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<MajorBaseDTO>>.Fail(_localizationService["List of Major cannot found"], new[]
                {
                    new ErrorItem
                    {
                        Error= e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of Major"
                    }
                });
            }
        }



        public async Task<RequestResult<PaginationResponse<PostMainDTO>>> GetAllPostByMajorAsync(ClientPostListRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var groupPostId = await _readOnlyDbContext.TagEntities.AsNoTracking().Where(x => x.MajorId == request.MajorId).Select(x => x.PostTags.Select(pt => pt.PostId).ToList()).ToListAsync(cancellationToken);

                List<Guid> lstPostId = new List<Guid>();
                foreach (var post in groupPostId)
                {
                    lstPostId.AddRange(post);
                }

                var test = await _readOnlyDbContext.PostEntities.AsNoTracking().Where(x => !x.Deleted && lstPostId.Contains(x.Id)).PaginateAsync<PostEntity, PostMainDTO>(request, _mapper, cancellationToken);
                foreach (var entity in test.Data!)
                {
                    var createdName = await _readOnlyDbContext.UserEntities.AsNoTracking().Where(x => x.Id == entity.CreatedBy).Select(x => x.Username).FirstOrDefaultAsync(cancellationToken);
                    entity.CreatedName = createdName == null ? "N/A" : createdName!;
                }

                return RequestResult<PaginationResponse<PostMainDTO>>.Succeed(new PaginationResponse<PostMainDTO>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = test.HasNext,
                    Data = test.Data,
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<PostMainDTO>>.Fail(_localizationService["List of post cannot found"], new[]
               {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of posts"
                    }
                });
            }
        }

        public async Task<RequestResult<List<PostMainDTO>>> GetAllPostByMajorId(Guid? id, CancellationToken cancellationToken)
        {
            try
            {
                var groupPostId = await _readOnlyDbContext.TagEntities.AsNoTracking().Where(x => x.Id == id).Select(x => x.PostTags.Select(pt => pt.PostId).ToList()).ToListAsync(cancellationToken);

                List<Guid> lstPostId = new List<Guid>();
                foreach (var post in groupPostId)
                {
                    lstPostId.AddRange(post);
                }
                var test = await _readOnlyDbContext.PostEntities.AsNoTracking().Where(x => lstPostId.Contains(x.Id) && !x.Deleted).ProjectTo<PostMainDTO>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
                return RequestResult<List<PostMainDTO>>.Succeed(test);
            }
            catch (Exception e)
            {

                return RequestResult<List<PostMainDTO>>.Fail(_localizationService["List of post cannot found"], new[]
               {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of posts"
                    }
                });
            }
        }



        public async Task<RequestResult<PaginationResponse<PostMainDTO>>> GetAllPostsBySearchAsyn(ClientPostSearchWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {

                IQueryable<PostEntity> query = _readOnlyDbContext.PostEntities.AsNoTracking().Where(x => !x.Deleted);
                var groupPostId = await _readOnlyDbContext.TagEntities.AsNoTracking().Where(x => x.MajorId == request.MajorId).Select(x => x.PostTags.Select(pt => pt.PostId).ToList()).ToListAsync(cancellationToken);

                List<Guid> lstPostIdByMajor = new List<Guid>();
                foreach (var post in groupPostId)
                {
                    lstPostIdByMajor.AddRange(post);
                }
                query = query.Where(x => lstPostIdByMajor.Contains(x.Id));
                var queryable = query.ProjectTo<PostMainDTO>(_mapper.ConfigurationProvider);
                List<Guid>? lstPostId;
                if (request.LstTagsId != null && request.LstTagsId!.Count > 0)
                {
                    lstPostId = await _readOnlyDbContext.PostTagEntities.AsNoTracking().Where(x => request.LstTagsId.Contains(x.TagId)).Select(x => x.PostId).Distinct().ToListAsync(cancellationToken);
                    if (lstPostId.Count > 0)
                    {
                        queryable = queryable.Where(x => lstPostId.Contains(x.Id));
                    }
                    else
                    {

                        return RequestResult<PaginationResponse<PostMainDTO>>.Succeed(new PaginationResponse<PostMainDTO>
                        {
                            PageNumber = request.PageNumber,
                            PageSize = request.PageSize,
                            HasNext = false,
                            Data = new List<PostMainDTO>()
                        });
                    }
                }
                if (!string.IsNullOrEmpty(request.StringSearch))
                {
                    queryable = queryable.Where(x => x.Title.ToLower().Contains(request.StringSearch!.ToLower()));
                }
                var result = await queryable.PaginateAsync(request, cancellationToken);
                return RequestResult<PaginationResponse<PostMainDTO>>.Succeed(new PaginationResponse<PostMainDTO>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<PostMainDTO>>.Fail(_localizationService["List of post cannot found"], new[]
               {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of posts"
                    }
                });
            }
        }

        public async Task<RequestResult<List<TagBaseDTO>>> GetAllTagList(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _readOnlyDbContext.TagEntities.AsNoTracking().Where(x => !x.Deleted).ProjectTo<TagBaseDTO>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
                return RequestResult<List<TagBaseDTO>>.Succeed(result);
            }
            catch (Exception e)
            {

                return RequestResult<List<TagBaseDTO>>.Fail(_localizationService["List of tags cannot found"], new[]
               {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of tags"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<PostMainDTO?>>> GetChildPostsByPostIdAsync(PostIdRequestWithPagination request, CancellationToken cancellationToken)
        {
            try
            {
                var post = await _readOnlyDbContext.PostEntities.AsNoTracking().Where(x => x.Id == request.Id && !x.Deleted).FirstOrDefaultAsync(cancellationToken);
                PaginationResponse<PostMainDTO> result = new();
                if (post!.PostId == null)
                {
                    var querry = await _readOnlyDbContext.PostEntities.AsNoTracking().Where(x => x.Id == request.Id && !x.Deleted).Select(x => x.Posts.Select(z => z.Id).ToList()).FirstOrDefaultAsync(cancellationToken);
                    result = await _readOnlyDbContext.PostEntities.AsNoTracking().Where(x => querry!.Contains(x.Id) && !x.Deleted && x.Id != post.Id).PaginateAsync<PostEntity, PostMainDTO>(request, _mapper, cancellationToken);
                }
                else
                {
                    var querry = await _readOnlyDbContext.PostEntities.AsNoTracking().Where(x => x.Id == post.PostId).Select(x => x.Posts.Select(child => child.Id).ToList()).FirstOrDefaultAsync(cancellationToken);
                    result = await _readOnlyDbContext.PostEntities.AsNoTracking().Where(x => querry!.Contains(x.Id) && !x.Deleted && x.Id != post.Id).PaginateAsync<PostEntity, PostMainDTO>(request, _mapper, cancellationToken);
                }
                return RequestResult<PaginationResponse<PostMainDTO?>>.Succeed(new PaginationResponse<PostMainDTO?>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<PostMainDTO?>>.Fail(_localizationService["List of post cannot found"], new[]
               {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of posts"
                    }
                });
            }
        }

        public async Task<RequestResult<MajorBaseDTO?>> GetMajorByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _readOnlyDbContext.MajorEntities.Where(x => x.Id == id).ProjectTo<MajorBaseDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
                return RequestResult<MajorBaseDTO?>.Succeed(result);
            }
            catch (Exception e)
            {

                return RequestResult<MajorBaseDTO?>.Fail(_localizationService["Major cannot found"], new[]
               {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "major"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<MajorBaseDTO>>> GetMajorByUserIdAsync(GetMajorByUserIdWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var querry = await _readOnlyDbContext.UserMajorEntities.AsNoTracking().Where(x => x.UserId == request.Id && !x.Deleted).Select(x => x.MajorId).ToListAsync(cancellationToken);
                var result = await _readOnlyDbContext.MajorEntities.AsNoTracking().Where(x => querry.Contains(x.Id) && !x.Deleted).PaginateAsync<MajorEntity, MajorBaseDTO>(request, _mapper, cancellationToken);
                return RequestResult<PaginationResponse<MajorBaseDTO>>.Succeed(new PaginationResponse<MajorBaseDTO>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<MajorBaseDTO>>.Fail(_localizationService["List of majors cannot found"], new[]
               {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of majors"
                    }
                });
            }
        }



        public async Task<RequestResult<PostMainDTO?>> GetParentPostByPostIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _readOnlyDbContext.PostEntities.AsNoTracking().Where(x => x.Id == id).Select(x => x.Post).Where(x => !x.Deleted).ProjectTo<PostMainDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
                return RequestResult<PostMainDTO?>.Succeed(result);

            }
            catch (Exception e)
            {

                return RequestResult<PostMainDTO?>.Fail(_localizationService["ParentPost is not found"], new[]
               {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "ParentPost"
                    }
                });
            }
        }

        public async Task<RequestResult<PostDetailDTO>> GetPostDetailByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _readOnlyDbContext.PostEntities.AsNoTracking().Where(x => x.Id == id && !x.Deleted).ProjectTo<PostDetailDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);

                result!.CreatedName = await _readOnlyDbContext.UserEntities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == result.CreatedBy) == null ? "N/A" : _readOnlyDbContext.UserEntities.AsNoTracking().First(x => x.Id == result.CreatedBy).Username;
                return RequestResult<PostDetailDTO>.Succeed(result);
            }
            catch (Exception e)
            {

                return RequestResult<PostDetailDTO>.Fail(_localizationService["Post is not found"], new[]
               {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "Post"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<TagBaseDTO>>> GetPostTagsByPostIdAsync(PostIdRequestWithPagination request, CancellationToken cancellationToken)
        {
            try
            {
                var querry = await _readOnlyDbContext.PostEntities.Where(x => x.Id == request.Id && !x.Deleted).Select(x => x.PostTags.Select(pt => pt.TagId).ToList()).FirstOrDefaultAsync(cancellationToken);
                var result = await _readOnlyDbContext.TagEntities.Where(x => querry!.Contains(x.Id) && !x.Deleted).PaginateAsync<TagEntity, TagBaseDTO>(request, _mapper, cancellationToken);
                return RequestResult<PaginationResponse<TagBaseDTO>>.Succeed(new PaginationResponse<TagBaseDTO>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<TagBaseDTO>>.Fail(_localizationService["List of tags of post cannot found"], new[]
{
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of tags of post"
                    }
                });
            }
        }
    }
}
