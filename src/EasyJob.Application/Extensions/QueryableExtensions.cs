using EasyJob.Domain.Exceptions;

namespace EasyJob.Application.Extensions;

public static class QueryableExtensions
{
    private static int maxPageSize = 100;

    public static IQueryable<T> ToPagedList<T>(
        this IQueryable<T> source,
        int pageSize,
        int pageIndex)
    {
        if(pageSize <= 0 || pageIndex <= 0)
        {
            throw new ValidationException(
                "Page size or index should be greater than 0");
        }

        if(pageSize > maxPageSize)
        {
            throw new ValidationException(
                $"Page size should be less than {maxPageSize}");
        }

        return source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
    }
}