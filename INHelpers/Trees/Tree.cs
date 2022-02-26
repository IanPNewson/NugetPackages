namespace INHelpers.Trees
{
    /// <inheritdoc />
    public class Tree<T> : ITree<T>
    {

        /// <summary>
        /// Creates an immutable tree node with the specified node value and no children
        /// </summary>
        public Tree(T item)
            : this(item, new Tree<T>[0]) { }

        /// <summary>
        /// Creates an immutable tree node with the specified node value and the suppled child nodes
        /// </summary>
        public Tree(T item, params Tree<T>[] children)
            : this(item, children?.ToList())
        { }

        /// <summary>
        /// Creates an immutable tree node with the specified node value and the suppled child nodes
        /// </summary>
        public Tree(T item, List<Tree<T>>? children)
        {
            Item = item;
            Children = children;
        }

        /// <inheritdoc />
        public T Item { get; }

        /// <inheritdoc />
        public IEnumerable<Tree<T>>? Children { get; }

        /// <inheritdoc />
        IEnumerable<ITree<T>>? ITree<T>.Children => Children;

        public override string ToString()
        {
            if (Children == null)
                return Item?.ToString()??"(item is null)";
            return $"{Item} > ({string.Join(" | ", Children.Select(x => x.ToString()))})";
        }
    }
}
