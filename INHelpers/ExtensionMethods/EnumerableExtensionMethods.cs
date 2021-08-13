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

        /// <summary>
        /// Gets all permutations of all elements in a set of sets.
        /// E.g. [[1,2],[3,4]] yields [[1,3],[1,4],[2,3],[2,4]].
        /// Returned sets could be in any order, but the elements within them will be in the order
        /// of the provided sets.
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<IEnumerable<T>> setOfSets)
        {
            if (setOfSets is null)
                throw new ArgumentNullException(nameof(setOfSets));

            if (!setOfSets.Any() ||
                setOfSets.All(set => !set.Any()))
                return new List<T[]>();

            var count = setOfSets.First().Count();

            foreach (var otherSet in setOfSets.Skip(1))
            {
                count = count * otherSet.Count();
            }

            var results = new List<T[]>(count);

            for (var i = 0; i < count; ++i)
            {
                results.Add(new T[setOfSets.Count()]);
                for (var j = 0; j < setOfSets.Count(); ++j)
                {
                    results[i][j] = setOfSets.ElementAt(j).ElementAt(i % setOfSets.ElementAt(j).Count());
                }
            }

            return results;
        }

        /// <summary>
        /// Returns the first index of a sequence within another sequence
        /// </summary>
        public static int? SequenceIndex<T>(this IEnumerable<T> input, IEnumerable<T> find)
        {
            if (input == null) return null;
            if (find == null) return null;

            var findCount = find.Count();
            var inputCount = input.Count();

            if (findCount > inputCount) return null;

            for (var start = 0; start <= inputCount - findCount; ++start)
            {
                if (input.Skip(start).Take(findCount)
                    .SequenceEqual(find))
                    return start;
            }
            return null;
        }
    }
}
