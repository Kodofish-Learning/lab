using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;
using Lab.Extensions;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyUnionTests
    {
        [Test]
        public void union_numbers()
        {
            var first = new[] { 1, 3, 5 };
            var second = new[] { 5, 3, 7 };

            var actual = JoeyUnion(first, second);
            var expected = new[] { 1, 3, 5, 7 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> JoeyUnion(IEnumerable<int> first, IEnumerable<int> second)
        {
            var joeyConcat = first.JoeyConcat(second);
            var sourceEnumerator = joeyConcat.GetEnumerator();
            var hashSet = new HashSet<int>();
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                if (hashSet.Add(current))
                {
                    yield return current;
                }
            }
        }
    }
}