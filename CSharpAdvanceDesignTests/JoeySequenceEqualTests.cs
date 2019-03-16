using NUnit.Framework;
using System;
using System.Collections.Generic;
using ExpectedObjects;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySequenceEqualTests
    {
        [Test]
        public void compare_two_numbers_equal()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2, 1 };

            var actual = JoeySequenceEqual(first, second, EqualityComparer<int>.Default);

            Assert.IsTrue(actual);
        }

        [Test]
        public void compare_two_numbers_sequence_not_equal()
        {
            var first = new List<int> { 3, 2, 1, 0 };
            var second = new List<int> { 3, 2, 1 };

            var actual = JoeySequenceEqual(first, second, EqualityComparer<int>.Default);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_sequence_not_equal2()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2, 1, 0 };

            var actual = JoeySequenceEqual(first, second, EqualityComparer<int>.Default);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_sequence_diff_not_equal()
        {
            var first = new List<int> { 3, 1, 2 };
            var second = new List<int> { 3, 2, 1 };

            var actual = JoeySequenceEqual(first, second, EqualityComparer<int>.Default);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_sequence_all_empty__equal()
        {
            var first = new List<int> {  };
            var second = new List<int> {  };

            var actual = JoeySequenceEqual(first, second, EqualityComparer<int>.Default);

            Assert.IsTrue(actual);
        }

        private bool JoeySequenceEqual<T>(IEnumerable<T> first, IEnumerable<T> second, EqualityComparer<T> comparer)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            while (firstEnumerator.MoveNext())
            {
                if (!secondEnumerator.MoveNext()) return false;
                
                if (!comparer.Equals(firstEnumerator.Current, secondEnumerator.Current)) return false;
            }

            return !secondEnumerator.MoveNext();
        }
    }
}