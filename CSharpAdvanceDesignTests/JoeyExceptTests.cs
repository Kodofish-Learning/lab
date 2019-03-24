using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyExceptTests
    {
        [Test]
        public void except_numbers()
        {
            var first = new[] { 1, 3, 5, 7 };
            var second = new[] { 7, 1, 4 };

            var actual = JoeyExcept(first, second);
            var expected = new[] { 3, 5 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> JoeyExcept(IEnumerable<int> first, IEnumerable<int> second)
        {
            var hashSet = new HashSet<int>();
            var sourceEnumerator = second.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                hashSet.Add(current);
            }

            sourceEnumerator = first.GetEnumerator();
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