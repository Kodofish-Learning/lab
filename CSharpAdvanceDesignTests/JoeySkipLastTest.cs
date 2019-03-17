using System.Collections.Generic;
using ExpectedObjects;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    public class JoeySkipLastTest
    {
        [Test]
        public void skip_last_2()
        {
            var numbers = new[] { 10,20,30,40,50,60};
            var expected = new[]{10,20,30,40};
            var actual = JoeySkipLast(numbers, 2);
            actual.ToExpectedObject().ShouldMatch(expected);

        }

        private IEnumerable<int> JoeySkipLast(IEnumerable<int> numbers, int count)
        {
            var sourceEnumerator = numbers.GetEnumerator();
            var queue = new Queue<int>();
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                
                if (queue.Count == count)
                {
                    yield return queue.Dequeue();
                }
                queue.Enqueue(current);
                
            }
        }
    }
}