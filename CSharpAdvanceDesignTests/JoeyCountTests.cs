using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyCountTests
    {
        [Test]
        public void count_of_numbers()
        {
            var numbers = new[] { 10, 20, 30, 40, 50 };

            var count = JoeyCount(numbers);
            var expected = 5;
            Assert.AreEqual(expected, count);
        }

        private int JoeyCount(IEnumerable<int> numbers)
        {
            var count = 0;
            var sourceEnumerator = numbers.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                count++;
            }

            return count;
        }
    }
}