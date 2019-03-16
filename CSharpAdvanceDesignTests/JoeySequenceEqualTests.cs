using NUnit.Framework;
using System.Collections.Generic;
using Lab.Entities;
using Lab.EqualityComparer;
using Lab.Extensions;

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

            var actual = first.JoeySequenceEqual(second, EqualityComparer<int>.Default);

            Assert.IsTrue(actual);
        }

        [Test]
        public void compare_two_numbers_sequence_not_equal()
        {
            var first = new List<int> { 3, 2, 1, 0 };
            var second = new List<int> { 3, 2, 1 };

            var actual = first.JoeySequenceEqual(second, EqualityComparer<int>.Default);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_sequence_not_equal2()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2, 1, 0 };

            var actual = first.JoeySequenceEqual(second, EqualityComparer<int>.Default);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_sequence_diff_not_equal()
        {
            var first = new List<int> { 3, 1, 2 };
            var second = new List<int> { 3, 2, 1 };

            var actual = first.JoeySequenceEqual(second, EqualityComparer<int>.Default);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_sequence_all_empty__equal()
        {
            var first = new List<int> {  };
            var second = new List<int> {  };

            var actual = first.JoeySequenceEqual(second, EqualityComparer<int>.Default);

            Assert.IsTrue(actual);
        }

        
        [Test]
        public void two_employees_sequence_equal()
        {
            var first = new List<Employee>
            {
                new Employee() {FirstName = "Joey", LastName = "Chen", Phone = "123"},
                new Employee() {FirstName = "Tom", LastName = "Li", Phone = "456"},
                new Employee() {FirstName = "David", LastName = "Wang", Phone = "789"},
            };

            var second = new List<Employee>
            {
                new Employee() {FirstName = "Joey", LastName = "Chen", Phone = "123"},
                new Employee() {FirstName = "Tom", LastName = "Li", Phone = "123"},
                new Employee() {FirstName = "David", LastName = "Wang", Phone = "123"},
            };

            IEqualityComparer<Employee> equalityComparer = new JoeyEmployeeWithPhoneEqualityComparer();
            var actual = first.JoeySequenceEqual(second,
                equalityComparer);

            Assert.IsTrue(actual);
        }
    }
}