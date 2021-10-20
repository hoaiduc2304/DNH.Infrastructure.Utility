using System;
using System.Collections.Generic;
using System.Text;

namespace DNH.Infrastructure.Paging
{
    public interface IHasTotalCount
    {
        /// <summary>
        /// Total count of Items.
        /// </summary>
        int TotalCount { get; set; }
    }
}
