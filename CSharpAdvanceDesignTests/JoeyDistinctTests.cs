using ExpectedObjects;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using Lab.Entities;
using Lab.Extensions;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyDistinctTests
    {
        [Test]
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
            var actual = FishDistinct(employees);
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> FishDistinct(IEnumerable<int> numbers)
        {
            var sourceEnumerator = numbers.GetEnumerator();
            var result = new HashSet<int>();
            
            while (sourceEnumerator.MoveNext())
            {
                if (result.Add(sourceEnumerator.Current))
                {
                    yield return sourceEnumerator.Current;
                }
            }
        }
    }
}