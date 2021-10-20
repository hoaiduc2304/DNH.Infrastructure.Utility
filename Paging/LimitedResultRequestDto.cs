
using DNH.Infrastructure.Paging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DNH.Infrastructure.Paging
{
    public class LimitedResultRequestDto : ILimitedResultRequest
    {
        [Range(0, int.MaxValue)]
        public virtual int SkipCount { get; set; }

        [Range(1, int.MaxValue)]
        public virtual int MaxResultCount { get; set; } = 10;

        public virtual FilterOperator Filter { get; set; }

        public virtual IEnumerable<Sort> Sorts { get; set; }

        public virtual IEnumerable<Aggregator> Aggregates { get; set; }

        public virtual IEnumerable<Group> Groups { get; set; }
    }
}
