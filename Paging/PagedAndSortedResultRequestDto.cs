using DNH.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNH.Infrastructure.Paging
{
    /// <summary>
    /// Simply implements <see cref="IPagedAndSortedResultRequest"/>.
    /// </summary>
    [Serializable]
    public class PagedAndSortedResultRequestDto : PagedResultRequestDto, IPagedAndSortedResultRequest
    {
        public virtual IEnumerable<Sort> Sort { get; set; }
    }
}
