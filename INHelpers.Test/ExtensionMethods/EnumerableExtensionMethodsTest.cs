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

        #region Flatten

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

        #endregion

        #region Permutations

        [Fact] public void Permutations()
        {
            var input = new[]
            {
                new[]{1,2},
                new[]{3,4,5},
                new[]{6,7},
            };

            var result = input.Permutations();

            Assert.Equal(12, result.Count());
            //All unique combinations
            Assert.Equal(12, result.Distinct().Count());

            Assert.True(result.Select(x => x.ElementAt(0)).Distinct().SequenceEqual(input[0]));
            Assert.True(result.Select(x => x.ElementAt(1)).Distinct().SequenceEqual(input[1]));
            Assert.True(result.Select(x => x.ElementAt(2)).Distinct().SequenceEqual(input[2]));
        }

        #endregion

        #region SequenceIndex

        [Fact] public void SequenceIndexAtStart()
        {
            var input = new[] { 0, 1, 1, 1, 0 };
            var result = input.SequenceIndex(new[] { 0 });
            Assert.Equal(0, result);
        }

        [Fact] public void SequenceIndexAtEnd()
        {
            var input = new[] { 0, 1, 1, 1, 0 };
            var result = input.SequenceIndex(new[] { 1, 0 });
            Assert.Equal(3, result);
        }

        [Fact] public void SequenceIndexNotFound()
        {
            var input = new[] { 0, 1, 1, 1, 0 };
            var result = input.SequenceIndex(new[] { 2 });
            Assert.Null(result);
        }

        #endregion

    }
}
