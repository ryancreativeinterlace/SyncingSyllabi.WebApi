using SyncingSyllabi.Data.Dtos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncingSyllabi.Common.Tools.Extensions
{
    public static class LinqExtensions
    {
        public static PaginatedResultDto<TSource> Page<TSource>(this IEnumerable<TSource> source, PaginationDto pagination)
        {
            var pagedResults = new PaginatedResultDto<TSource>();

            if (pagination != null)
            {
                var query = source;

                if (pagination.IncludeTotal)
                {
                    pagedResults.TotalCount = query.Count();
                }

                if (pagination.Skip > 0)
                {
                    query = query.Skip(pagination.Skip);
                }

                if (pagination.Take > 0)
                {
                    query = query.Take(pagination.Take);
                }

                pagedResults.Items = query.ToList();
            }

            return pagedResults;
        }

        public static void Page<TSource>(this PaginatedResultDto<TSource> pagedResults, IEnumerable<TSource> source, PaginationDto pagination)
        {
            if (pagination != null)
            {
                var query = source;

                if (pagination.IncludeTotal)
                {
                    pagedResults.TotalCount = query.Count();
                }

                if (pagination.Skip > 0)
                {
                    query = query.Skip(pagination.Skip);
                }

                if (pagination.Take > 0)
                {
                    query = query.Take(pagination.Take);
                }

                pagedResults.Items = query.ToList();
            }
        }
    }
}
