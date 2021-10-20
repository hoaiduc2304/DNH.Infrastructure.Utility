using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNH.Infrastructure.Paging
{
    /// <summary>
    /// Represents a filter expression of DataSource.
    /// </summary>
    public class FilterOperator
    {
        /// <summary>
        /// Gets or sets the name of the sorted field (property). Set to <c>null</c> if the <c>Filters</c> property is set.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets the filtering operator. Set to <c>null</c> if the <c>Filters</c> property is set.
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Gets or sets the filtering value. Set to <c>null</c> if the <c>Filters</c> property is set.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the filtering logic. Can be set to "or" or "and". Set to <c>null</c> unless <c>Filters</c> is set.
        /// </summary>
        public string Logic { get; set; }

        /// <summary>
        /// Gets or sets the child filter expressions. Set to <c>null</c> if there are no child expressions.
        /// </summary>
        public IEnumerable<FilterOperator> Filters { get; set; }

        /// <summary>
        /// Mapping of DataSource filtering operators to Dynamic Linq
        /// </summary>
        private static readonly IDictionary<string, string> operators = new Dictionary<string, string>
        {
            {"eq", "="},
            {"neq", "!="},
            {"lt", "<"},
            {"lte", "<="},
            {"gt", ">"},
            {"gte", ">="},
            {"startswith", "StartsWith"},
            {"endswith", "EndsWith"},
            {"contains", "Contains"},
            {"doesnotcontain", "DoesNotContain"}
        };

        /// <summary>
        /// Get a flattened list of all child filter expressions.
        /// </summary>
        public IList<FilterOperator> All()
        {
            var filters = new List<FilterOperator>();

            Collect(filters);

            return filters;
        }

        private void Collect(IList<FilterOperator> filters)
        {
            if (Filters != null && Filters.Any())
            {
                foreach (FilterOperator filter in Filters)
                {
                    filters.Add(filter);

                    filter.Collect(filters);
                }
            }
        }

        /// <summary>
        /// Converts the filter expression to a predicate suitable for Dynamic Linq e.g. "Field1 = @1 and Field2.Contains(@2)"
        /// </summary>
        /// <param name="filters">A list of flattened filters.</param>
        public string ToExpression(IList<FilterOperator> filters)
        {
            if (Filters != null && Filters.Any())
            {
                return "(" + String.Join(" " + filters[0].Logic + " ", Filters.Select(filter => filter.ToExpression(filters)).ToArray()) + ")";
            }

            int index = filters.IndexOf(this);

            string comparison = operators[Operator];

            //we ignore case
            if (comparison == "Contains")
            {
                return String.Format("{0}.Contains(@{1})", Field, index);
            }
            if (comparison == "DoesNotContain")
            {
                return String.Format("!{0}.Contains(@{1})", Field, index);
            }
            if (comparison == "StartsWith" || comparison == "EndsWith")
            {
                return String.Format("{0}.{1}(@{2})", Field, comparison, index);
            }

            return String.Format("{0} {1} @{2}", Field, comparison, index);

        }
        public string ToExpressionOption(IList<FilterOperator> filters)
        {
            //if (Filters != null && Filters.Any())
            //{
            //    return "(" + String.Join(" " + filters[0].Logic + " ", Filters.Select(filter => filter.ToExpression(filters)).ToArray()) + ")";
            //}
            string CombineFilter = "";
            for(int i=0; i <= filters.Count-1; i++)
            {
                string comparison = operators[filters[i].Operator];
                string query = String.Format("{0} {1} @{2}", filters[i].Field, comparison, i);
                if (comparison == "Contains")
                {
                    query = String.Format("{0}.Contains(@{1})", filters[i].Field, i);
                }
                if (comparison == "DoesNotContain")
                {
                    query = String.Format("!{0}.Contains(@{1})", filters[i].Field, i);
                }
                if (comparison == "StartsWith" || comparison == "EndsWith")
                {
                    query = String.Format("{0}.{1}(@{2})", Field, comparison, i);
                }
                CombineFilter+= "(" + String.Join(" " + filters[i].Logic + " ", query) + ")";
            }
           // int index = filters.IndexOf(this);
            //  if (index == -1) index = 0;

            //string comparison = operators[filters[0].Operator];
            //string query = String.Format("{0} {1} @{2}", Field, comparison, index);
            ////we ignore case
            //if (comparison == "Contains")
            //{
            //    query = String.Format("{0}.Contains(@{1})", Field, index);
            //}
            //if (comparison == "DoesNotContain")
            //{
            //    query = String.Format("!{0}.Contains(@{1})", Field, index);
            //}
            //if (comparison == "StartsWith" || comparison == "EndsWith")
            //{
            //    query = String.Format("{0}.{1}(@{2})", Field, comparison, index);
            //}


            return CombineFilter;
        }

    }
}
