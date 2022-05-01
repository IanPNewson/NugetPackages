using INHelpers.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace INHelpers.Test.Reflection
{
    public class FactoryTest
    {

        [Fact]
        public void InstantiateBestMatch_DefaultCoonstructorOnly()
        {
            var instance = typeof(DefaultConstructor).InstantiateBestMatch();

            Assert.NotNull(instance);
        }

        [Fact]
        public void InstantiateBestMatch_OneArgSuccess()
        {
            var instance = Factory.InstantiateBestMatch<OneArg<string>>("hi");

            Assert.Equal("hi", instance.Parameters.Single());
        }

        [Fact]
        public void InstantiateBestMatch_OneArgFail_WrongType()
        {
            Assert.Throws<ArgumentException>(() => typeof(OneArg<string>).InstantiateBestMatch(DateTime.Now));
        }

        [Fact]
        public void InstantiateBestMatch_TwoArgsOneMatch()
        {
            var instance = Factory.InstantiateBestMatch<OneArg<string>>(DateTime.Now, "hi");

            Assert.Equal("hi", instance.Parameters.Single());
        }

        [Fact]
        public void InstantiateBestMatch_InsufficientParameters()
        {
            Assert.Throws<ArgumentException>(() =>
                Factory.InstantiateBestMatch<TwoArg<string, string>>("1")
            );
        }

        [Fact]
        public void InstantiateBestMatch_TwoArgsSameType()
        {
            var args = new[] { "one", "two" };
            var instance = Factory.InstantiateBestMatch<TwoArg<string,string>>(args);

            Assert.True(args.SequenceEqual(instance.Parameters));
        }

        [Fact]
        public void InstantiateBestMatch_TwoArgsNoMatch()
        {
            Assert.Throws<ArgumentException>(() =>
                typeof(OneArg<string>).InstantiateBestMatch()
            );
        }

        [Fact]
        public void InstantiateBestMatch_NoCtors()
        {
            Assert.Throws<ArgumentException>(() =>
                typeof(NoCtors).InstantiateBestMatch("", "")
            );
        }

        [Fact]
        public void InstantiateBestMatch_Interface()
        {
            Assert.Throws<ArgumentException>(() =>
                Factory.InstantiateBestMatch<IFace>("", "")
            );
        }

        public interface IFace { }

        private class TwoArg<T, U> : Base
        {
            public TwoArg(T one, U two) : base(one, two)
            {
            }
        }

        private class OneArg<T> : Base
        {
            public OneArg(T arg) : base(arg) { }
        }

        public abstract class NoCtors { }

        private abstract class Base
        {
            protected Base(params object[] parameters)
            {
                Parameters = parameters;
            }

            public object[] Parameters { get; }
        }

        private class DefaultConstructor { }

    }
}
