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

        /// <summary>
        /// Converts a tree like structure into a flat list with a breadth first search
        /// </summary>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> list, Func<T, IEnumerable<T>> getChildren)
        {
            if (null == list)
                throw new ArgumentNullException(nameof(list));
            if (null == getChildren)
                throw new ArgumentNullException(nameof(getChildren));

            var queue = new Queue<T>(list);

            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                foreach (var child in getChildren(item))
                    queue.Enqueue(child);

                yield return item;
            }
        }
    }
}
