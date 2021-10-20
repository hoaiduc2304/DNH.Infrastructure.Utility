using DNH.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNH.Infrastructure.Paging
{
    /// <summary>
    /// This interface is defined to standardize to request a limited result.
    /// </summary>
    public interface ILimitedResultRequest
    {
        /// <summary>
        /// Max expected result count.
        /// </summary>
        int MaxResultCount { get; set; }

        FilterOperator Filter { get; set; }

        IEnumerable<Sort> Sorts { get; set; }

        IEnumerable<Aggregator> Aggregates { get; set; }

        IEnumerable<Group> Groups { get; set; }
    }
}
