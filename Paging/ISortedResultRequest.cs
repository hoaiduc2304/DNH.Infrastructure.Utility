using DNH.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNH.Infrastructure.Paging
{
    public interface ISortedResultRequest
    {
        IEnumerable<Sort> Sort { get; set; }
    }
}
