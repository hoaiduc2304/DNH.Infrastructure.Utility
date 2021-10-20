using DNH.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNH.Infrastructure.Paging
{
    public class FilterRequest
    {
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        //public IEnumerable<FilterField> filter { get; set; }
        public IEnumerable<FilterOperator> filter { get; set; }
        public virtual IEnumerable<Sort> Sorts { get; set; }
    }
    //public static class FilterRequestExtension
    //{
    //    public static IEnumerable<FilterOperator> ToFilterOperator(this IEnumerable<FilterField> filter)
    //    {
    //        return filter.Select(x => new FilterOperator()
    //        {
    //            Field = x.Field,
    //            Logic = x.Logic,
    //            Operator = x.Operator,
    //            Value = x.Value
    //        });
    //    }
    //}
    //public class FilterField
    //{
    //    public string Field { get; set; }

    //    /// <summary>
    //    /// Gets or sets the filtering operator. Set to <c>null</c> if the <c>Filters</c> property is set.
    //    /// </summary>
    //    public string Operator { get; set; }

    //    /// <summary>
    //    /// Gets or sets the filtering value. Set to <c>null</c> if the <c>Filters</c> property is set.
    //    /// </summary>
    //    /// 
       
    //    public object Value { get; set; }

    //    /// <summary>
    //    /// Gets or sets the filtering logic. Can be set to "or" or "and". Set to <c>null</c> unless <c>Filters</c> is set.
    //    /// </summary>
    //    public string Logic { get; set; }
    //    public IEnumerable<FilterOperator> IncludeFilterList { get; set; }
      
    //}
}
