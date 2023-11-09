﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Infrastructure.Extensions;
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
        private readonly DbSet<TagEntity> _tagEntities;
        private readonly DbSet<PostTagEntity> _postTagEntities;
		private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public TagReadOnlyRepository(AppReadOnlyDbContext dbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _tagEntities = dbContext.Set<TagEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
			_postTagEntities = dbContext.Set<PostTagEntity>();

		}

        public async Task<RequestResult<TagDto?>> GetTagByIdAsync(Guid idTag, CancellationToken cancellationToken)
        {
            try
            {
                var tag = await _tagEntities.AsNoTracking().Where(c => c.Id == idTag && !c.Deleted).ProjectTo<TagDto>(_mapper.ConfigurationProvider)
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

        public async Task<RequestResult<List<TagDto>?>> GetTagByIdMajorAsync(Guid? idMajor, Guid? idPost , CancellationToken cancellationToken)
        {
            try
            {
                var tags = _tagEntities.AsNoTracking().AsQueryable();

                //lấy listTag theo idMajor
                if (idMajor != null)
                {
                    tags.Where(x => x.MajorId == idMajor && !x.Deleted);
                }

                //lấy listTag theo idPost
                if (idPost != null)
                {
                    var lstPT = await _postTagEntities.ToListAsync();
                    lstPT.Where(c=>c.PostId == idPost && !c.Deleted);
                    if (lstPT != null)
                    {
                        foreach (var pt in lstPT)
                        {
                            tags.Where(c => c.Id == pt.TagId);
                        }
                    }
                }

                var tagDtos = await tags.ProjectTo<TagDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

                return RequestResult<List<TagDto>?>.Succeed(tagDtos);
            }
            catch (Exception e)
            {
                return RequestResult<List<TagDto>?>.Fail(_localizationService["Tags is not found"], new[]
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
                IQueryable<TagEntity> queryable = _tagEntities.AsNoTracking().AsQueryable();
                var result = await _tagEntities.AsNoTracking()
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
