using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace INHelpers.Trees
{

    /// <summary>
    /// Represents a tree structure containing items of type T.
    /// Each node in the tree has an item which the node represents,
    /// and zero to many children.
    /// </summary>
    /// <typeparam name="T">The type of items contained by this tree.</typeparam>
    public interface ITree<T>
    {

        /// <summary>
        /// The item this node represents
        /// </summary>
        T Item { get; }

        /// <summary>
        /// This child nodes of this tree node
        /// </summary>
        IEnumerable<ITree<T>>? Children { get; }
    }
}
