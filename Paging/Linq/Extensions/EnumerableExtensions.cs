using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace DNH.Infrastructure.Paging
{
    /// <summary> 
    /// Extension methods for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Groups the by many.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="elements">The elements.</param>
        /// <param name="groupSelectors">The group selectors.</param>
        /// <returns></returns>
        public static dynamic GroupByMany<TElement>(this IEnumerable<TElement> elements, IEnumerable<Group> groupSelectors)
        {
            var selectors = new List<GroupSelector<TElement>>(groupSelectors.Count());
            foreach (var selector in groupSelectors)
            {
                // Compile the Dynamic Expression Lambda for each one
                var expression = DynamicExpressionParser.ParseLambda(false, typeof(TElement), typeof(object), selector.Field);

                // Add it to the list
                selectors.Add(new GroupSelector<TElement>
                {
                    Selector = (Func<TElement, object>)expression.Compile(),
                    Field = selector.Field,
                    Aggregates = selector.Aggregates
                });
            }

            // Call the actual group by method
            return elements.GroupByMany(selectors.ToArray());
        }

        /// <summary>
        /// Groups the by many.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="elements">The elements.</param>
        /// <param name="groupSelectors">The group selectors.</param>
        /// <returns></returns>
        public static dynamic GroupByMany<TElement>(this IEnumerable<TElement> elements, params GroupSelector<TElement>[] groupSelectors)
        {
            if (groupSelectors.Length > 0)
            {
                // Get selector
                var selector = groupSelectors[0];
                var nextSelectors = groupSelectors.Skip(1).ToArray();   // Reduce the list recursively until zero

                // Group by and return                
                return elements.GroupBy(selector.Selector).Select(
                            g => new GroupResult
                            {
                                Value = g.Key,
                                Aggregates = g.AsQueryable().Aggregates(selector.Aggregates),
                                HasSubgroups = groupSelectors.Length > 1,
                                Count = g.Count(),
                                Items = g.GroupByMany(nextSelectors),   // Recursivly group the next selectors
                                SelectorField = selector.Field
                            });
            }

            // If there are not more group selectors return data
            return elements;
        }
    }
}
