using SyncingSyllabi.Data.Dtos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static SyncingSyllabi.Data.Constants.GoalConstants;

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
                    pagedResults.Skip = pagination.Skip;
                }

                if (pagination.Take > 0)
                {
                    query = query.Take(pagination.Take);
                    pagedResults.Take = pagination.Take;
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

        public static IEnumerable<T> MultipleSort<T>(this IEnumerable<T> data, List<SortColumnDto> sortColumn)
        {
            var sortExpressions = new List<Tuple<string, string>>();
            for (int i = 0; i < sortColumn.Count(); i++)
            {
                var fieldName = GoaldFieldsIds.GetName(sortColumn[i].FieldCode);
                var sortOrder = (sortColumn[i].Direction.Length > 1) ? sortColumn[i].Direction.Trim().ToLower() : "asc";

                //Sort GoalTypeName
                //fieldName = (fieldName == "GoalTypeName") ? fieldName + "Text" : fieldName;

                sortExpressions.Add(new Tuple<string, string>(fieldName, sortOrder));
            }

            // No sorting needed  
            if ((sortExpressions == null) || (sortExpressions.Count <= 0))
            {
                return data;
            }

            IEnumerable<T> query = from item in data select item;
            IOrderedEnumerable<T> orderedQuery = null;
            for (int i = 0; i < sortExpressions.Count; i++)
            {
                var index = i;
                Func<T, object> expression = item => item.GetType().GetProperty(sortExpressions[index].Item1).GetValue(item, null);

                if (sortExpressions[index].Item2 == "asc")
                {
                    orderedQuery = (index == 0) ? query.OrderBy(expression) : orderedQuery.ThenBy(expression);
                }
                else
                {
                    orderedQuery = (index == 0) ? query.OrderByDescending(expression) : orderedQuery.ThenByDescending(expression);
                }
            }

            query = orderedQuery;

            return query;
        }
    }
}
