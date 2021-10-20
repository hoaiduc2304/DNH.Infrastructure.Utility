using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNH.Infrastructure.Paging
{
    public class Group : Sort
    {
        public IEnumerable<Aggregator> Aggregates { get; set; }
    }
}
