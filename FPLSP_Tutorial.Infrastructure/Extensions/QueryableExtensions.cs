using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace FPLSP_Tutorial.Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginationResponse<TSourceEntity>> PaginateAsync<TSourceEntity>(
        this IQueryable<TSourceEntity> queryable, PaginationRequest request,
        CancellationToken cancellationToken)
    {
        // Force to sort by CreateTime asc 
        var finalQuery = queryable;

        // Hit to the db to get data back to client side
        var result = await finalQuery
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize + 1)
            .ToListAsync(cancellationToken);

        var hasNext = result.Count == request.PageSize + 1;

        return new PaginationResponse<TSourceEntity>
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            HasNext = hasNext,
            Data = result.Take(request.PageSize).ToList()
        };
    }

    public static async Task<PaginationResponse<TTargetEntity>> PaginateAsync<TSourceEntity, TTargetEntity>(
        this IQueryable<TSourceEntity> queryable, PaginationRequest request, IMapper mapper,
        CancellationToken cancellationToken) where TSourceEntity : ICreatedBase
    {
        // Force to sort by CreateTime desc 
        var finalQuery = queryable;
        //IQueryable<TSourceEntity> finalQuery = queryable.OrderByDescending(x => x.CreatedTime);


        // Hit to the db to get data back to client side
        var result = await finalQuery
            .ProjectTo<TTargetEntity>(mapper.ConfigurationProvider)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize + 1)
            .ToListAsync(cancellationToken);

        var hasNext = result.Count == request.PageSize + 1;

        return new PaginationResponse<TTargetEntity>
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            HasNext = hasNext,
            Data = result.Take(request.PageSize).ToList()
        };
    }

    public static IQueryable<T> SortData<T>(this IQueryable<T> listData, string sortPropName = "CreatedTime",
        string sortDir = "desc")
    {
        var lstPropInfo = typeof(T).GetProperties();

        if (sortPropName != null && lstPropInfo.Any(c => c.Name == sortPropName))
        {
            var propInfo = lstPropInfo.First(c => c.Name == sortPropName);
            var parameter = Expression.Parameter(typeof(T), "c");
            var property = Expression.Property(parameter, propInfo);

            if (propInfo.PropertyType.IsGenericType &&
                propInfo.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
            {
                // Property is a list type
                var listCount = Expression.Property(property, "Count");
                var selector = Expression.Lambda(listCount, parameter);

                if (sortDir.ToLower() == "asc")
                    listData = Queryable.OrderBy(listData, (dynamic)selector);
                else if (sortDir.ToLower() == "desc")
                    listData = Queryable.OrderByDescending(listData, (dynamic)selector);
            }
            else
            {
                // Property is not a list type
                var selector = Expression.Lambda(property, parameter);

                if (sortDir.ToLower() == "asc")
                    listData = Queryable.OrderBy(listData, (dynamic)selector);
                else if (sortDir.ToLower() == "desc")
                    listData = Queryable.OrderByDescending(listData, (dynamic)selector);
            }
        }

        return listData;
    }
}