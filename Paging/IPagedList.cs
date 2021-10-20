using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DNH.Infrastructure.Paging
{
    public interface IPagedList<T> : IList<T>
    {
        /// <summary>
        /// Page index
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// Page size
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// Total count
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// Total pages
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        /// Has previous page
        /// </summary>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Has next age
        /// </summary>
        bool HasNextPage { get; }

        /// <summary>
        /// Represents a single page of processed grouped data.
        /// </summary>
        IEnumerable Groups { get; set; }

        /// <summary>
        /// Represents a requested aggregates.
        /// </summary>
        object Aggregates { get; set; }
    }
}
