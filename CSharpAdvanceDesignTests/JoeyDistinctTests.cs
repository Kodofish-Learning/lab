using ExpectedObjects;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using Lab.Entities;
using Lab.EqualityComparer;
using Lab.Extensions;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyDistinctTests
    {
        public void distinct_numbers()
        {
            var numbers = new[] { 91, 3, 91, -1 };
            var actual = FishDistinct(numbers);

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
            var actual = FishDistinct(employees, new JoeyEmployeeWithPhoneEqualityComparer());
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TSource> FishDistinct<TSource>(IEnumerable<TSource> employees, IEqualityComparer<TSource> comparer)
        {
            var sourceEnumerator = employees.GetEnumerator();
            var hashSet = new HashSet<TSource>(comparer);
            while (sourceEnumerator.MoveNext())
            {
                if (hashSet.Add(sourceEnumerator.Current))
                    yield return sourceEnumerator.Current;
            }
        }

        private IEnumerable<T> FishDistinct<T>(IEnumerable<T> numbers)
        {
            return FishDistinct(numbers, EqualityComparer<T>.Default);
        }
    }
}