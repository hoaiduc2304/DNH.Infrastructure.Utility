using System;
using System.Collections.Generic;
using System.Text;

namespace DNH.Infrastructure.Paging
{
    public interface IListResult<T>
    {
        /// <summary>
        /// List of items.
        /// </summary>
        IReadOnlyList<T> Items { get; set; }
    }
}
