using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INHelpers.ExtensionMethods
{
    public static class EnumerableExtensionMethods
    {

        /// <summary>
        /// Returns all non-null items from this list
        /// </summary>
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> list)
            where T : class
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            return list.Where(x => null != x);
        }

        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> list, Func<T, IEnumerable<T>> getChildren)
        {
            if (null == list)
                throw new ArgumentNullException(nameof(list));
            if (null == getChildren)
                throw new ArgumentNullException(nameof(getChildren));

            var result = new List<T>(list);
            foreach (var t in list)
                result.AddRange(getChildren(t).Flatten(getChildren));
            return result;
        }
    }
}
