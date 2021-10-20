using DNH.Infrastructure.AutoMap;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace DNH.Infrastructure.Paging
{
    public static class PaggingExtenstion
    {
        //public static PagedResultDto<T> Getpaging<T>(this IQueryable<T> query, PagedResultRequestDto input)
        //{
        //    var result = new PagedList<T>(query, input);
        //    return new PagedResultDto<T>(result.TotalCount, result.ToList());
        //}

        public static PagedResultDto<T> Getpaging<T>(this IQueryable<T> query, FilterRequest request)
        {

            var input = new PagedResultRequestDto
            {
                SkipCount = (request.page - 1),
                MaxResultCount = request.pageSize,
                Filter = new FilterOperator
                {
                    Filters = request.filter
                },
                Sorts = request.Sorts
            };
            var result = new PagedList<T>(query, input);
            return new PagedResultDto<T>(result.TotalCount, result.ToList());
        }

        //public static PagedResultDto<T> GetpagingOption<T>(this IQueryable<T> query, FilterRequest request)
        //{

        //    var input = new PagedResultRequestDto
        //    {
        //        SkipCount = (request.page - 1),
        //        MaxResultCount = request.pageSize,
        //        Filter = new FilterOperator
        //        {
        //            Filters = request.requestFilter.IncludeFilterList
        //        },
        //        Sorts = request.Sorts
        //    };
        //    var result = new PagedList<T>(query, input);
        //    return new PagedResultDto<T>(result.TotalCount, result.ToList());
        //}
    }
  
}
