using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySequenceEqualTests
    { 
        [Test]
        public void compare_two_numbers_all_empty()
        {
            var first = new List<int> {};
            var second = new List<int> {};

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }
        [Test]
        public void compare_two_numbers_length_not_equal2()
        {
            var first = new List<int> { 3, 2, 1};
            var second = new List<int> { 3, 2, 1, 0 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }
        [Test]
        public void compare_two_numbers_length_not_equal1()
        {
            var first = new List<int> { 3, 2, 1, 0 };
            var second = new List<int> { 3, 2, 1 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }
        [Test]
        public void compare_two_numbers_not_equal()
        {
            var first = new List<int> { 3, 4, 1 };
            var second = new List<int> { 3, 2, 1 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }
        [Test]
        public void compare_two_numbers_equal()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2, 1 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }

        private bool JoeySequenceEqual(IEnumerable<int> first, IEnumerable<int> second)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();

            while (true)
            {
                var hasFirst = firstEnumerator.MoveNext();
                var hasSecond = secondEnumerator.MoveNext();
                
                if (!hasFirst && !hasSecond)
                {
                    return true;
                }
                
                if (hasFirst != hasSecond)
                {
                    return false;
                }
                
                
                var firstElement = firstEnumerator.Current;
                var secondElement = secondEnumerator.Current;
                if (firstElement != secondElement)
                {
                    return false;
                }
            }
            
            

            return true;
        }
    }
}