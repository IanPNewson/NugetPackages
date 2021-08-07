using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using INHelpers.ExtensionMethods;

namespace INHelpers.Test.ExtensionMethods
{
    public class EnumerableExtensionMethodsTest
    {

        [Fact] public void FlattenTest()
        {
            var data = new[] {
                new Tree(
                    "1",
                    new Tree("1.1",
                        new Tree("1.1.1")
                    ),
                    new Tree("1.2",
                        new Tree("1.2.1"),
                        new Tree("1.2.2"),
                        new Tree("1.2.3")
                    )
                ),
                new Tree("2")
            };

            var result = data.Flatten(tree=>tree.Children)
                .Select(x => x.Name).OrderBy(x => x).ToArray();

            var expected = new[] { "1", "1.1", "1.1.1", "1.2", "1.2.1", "1.2.2", "1.2.3", "2" };

            Assert.True(result.SequenceEqual(expected));
        }

        public class Tree
        {
            public string Name { get; set; }
            public List<Tree> Children { get; set; }

            public Tree(string name, params Tree[] children)
            {
                Name = name;
                Children = children.ToList();
            }
        }
    }
}
