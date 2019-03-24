using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyMaxTests
    {
        [Test]
        public void get_max_number()
        {
            var numbers = new[] { 1, 3, 91, 5 };

            var max = JoeyMax(numbers);

            Assert.AreEqual(91, max);
        }

        private int JoeyMax(IEnumerable<int> numbers)
        {
            var max = 0;
            var sourceEnumerator = numbers.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                if (current > max)
                {
                    max = current;
                }
            }

            return max;
        }
    }
}