using ExpectedObjects;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using Lab;
using Lab.Entities;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyDistinctTests
    {
        [Test]
        public void distinct_numbers()
        {
            var numbers = new[] { 91, 3, 91, -1 };
            var actual = Distinct(numbers);

            var expected = new[] { 91, 3, -1 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void distinct_employee()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"}
            };
            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"}
            };
            var actual = Distinct(employees, new EmployeeComparer());
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TSource> Distinct<TSource>(IEnumerable<TSource> employees, IEqualityComparer<TSource> comparer)
        {
            var sourceEnumerator = employees.GetEnumerator();
            var hashSet = new HashSet<TSource>(comparer);
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                if (hashSet.Add(current))
                {
                    yield return current;
                }
            }
        }

        private IEnumerable<int> Distinct(IEnumerable<int> numbers)
        {
            var sourceEnumerator = numbers.GetEnumerator();
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