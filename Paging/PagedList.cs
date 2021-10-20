using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DNH.Infrastructure.Paging
{
    [Serializable]
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList(IQueryable<T> source, PagedResultRequestDto info, bool getOnlyTotalCount = false)
        {
            source = source.Filter(info.Filter);

            // Calculate the total number of records (needed for paging)   
            var total = source.Count();

            // Calculate the aggregates
            var aggregate = source.Aggregates(info.Aggregates);

            if (info.Groups?.Any() == true)
            {
                //if(sort == null) sort = GetDefaultSort(queryable.ElementType, sort);
                if (info.Sorts == null) info.Sorts = new List<Sort>();

                foreach (var group in info.Groups.Reverse())
                {
                    info.Sorts = info.Sorts.Append(new Sort
                    {
                        Field = group.Field,
                        Dir = group.Dir
                    });
                }
            }

            // Sort the data
            source = source.Sort(info.Sorts);

            Aggregates = aggregate;

            if (info.Groups?.Any() == true)
            {
                Groups = source.GroupByMany(info.Groups);
            }

            TotalCount = total;
            TotalPages = total / info.MaxResultCount;

            if (total % info.MaxResultCount > 0)
                TotalPages++;

            PageSize = info.MaxResultCount;
            PageIndex = info.SkipCount;
            if (getOnlyTotalCount)
                return;
            AddRange(source.Skip(info.SkipCount * info.MaxResultCount).Take(info.MaxResultCount).ToList());
        }
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="filter">Filter</param>
        /// <param name="getOnlyTotalCount">A value in indicating whether you want to load only total number of records. Set to "true" if you don't want to load data from database</param>
        public PagedList(IQueryable<T> source, int pageIndex, int pageSize, FilterOperator filter = null, IEnumerable<Sort> sort = null, IEnumerable<Aggregator> aggregates = null, IEnumerable<Group> groups = null, bool getOnlyTotalCount = false)
        {
            // Filter the data first
            source = source.Filter(filter);

            // Calculate the total number of records (needed for paging)   
            var total = source.Count();

            // Calculate the aggregates
            var aggregate = source.Aggregates(aggregates);

            if (groups?.Any() == true)
            {
                //if(sort == null) sort = GetDefaultSort(queryable.ElementType, sort);
                if (sort == null) sort = new List<Sort>();

                foreach (var group in groups.Reverse())
                {
                    sort = sort.Append(new Sort
                    {
                        Field = group.Field,
                        Dir = group.Dir
                    });
                }
            }

            // Sort the data
            source = source.Sort(sort);

            Aggregates = aggregate;

            if (groups?.Any() == true)
            {
                Groups = source.GroupByMany(groups);
            }

            TotalCount = total;
            TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            PageSize = pageSize;
            PageIndex = pageIndex;
            if (getOnlyTotalCount)
                return;
            AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());

        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        public PagedList(IList<T> source, int pageIndex, int pageSize)
        {
            TotalCount = source.Count;
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            PageSize = pageSize;
            PageIndex = pageIndex;
            AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="totalCount">Total count</param>
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            TotalCount = totalCount;
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            PageSize = pageSize;
            PageIndex = pageIndex;
            AddRange(source);
        }

        /// <summary>
        /// Page index
        /// </summary>
        public int PageIndex { get; }

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Total count
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// Total pages
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// Has previous page
        /// </summary>
        public bool HasPreviousPage => PageIndex > 0;

        /// <summary>
        /// Has next page
        /// </summary>
        public bool HasNextPage => PageIndex + 1 < TotalPages;

        /// <summary>
        /// Represents a single page of processed grouped data.
        /// </summary>
        public IEnumerable Groups { get; set; }

        /// <summary>
        /// Represents a requested aggregates.
        /// </summary>
        public object Aggregates { get; set; }
    }
}
