using System.Collections;

namespace DNH.Infrastructure.Paging
{
    public class GroupResult
    {
        public object Value { get; set; }

        public string SelectorField { get; set; }

        public string Field
        {
            get { return string.Format("{0} ({1})", this.SelectorField, this.Count); }
        }

        public int Count { get; set; }

        public object Aggregates { get; set; }

        public IEnumerable Items { get; set; }

        public bool HasSubgroups { get; set; } // true if there are subgroups
    }
}
