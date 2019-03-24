using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using ExpectedObjects;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyLastOrDefaultTests
    {
        [Test]
        public void get_null_when_employees_is_empty()
        {
            var employees = new List<Employee>();
            var actual = JoeyLastOrDefault(employees);
            Assert.IsNull(actual);
        }

        [Test]
        public void get_Last_when_employees_is_not_empty()
        {
            var employees = new List<Employee>()
            {
                new Employee(){FirstName = "Fish", LastName = "Chang"},
                new Employee(){FirstName = "Joey", LastName = "Chen"},
                new Employee(){FirstName = "David", LastName = "Ko"},
            };
            var expected = new Employee(){FirstName = "David", LastName = "Ko"};
            var actual = JoeyLastOrDefault(employees);
            expected.ToExpectedObject().Equals(actual);
            Assert.IsNotNull(actual);
        }
        private Employee JoeyLastOrDefault(IEnumerable<Employee> employees)
        {
            var queue = new Queue<Employee>(employees);
            return queue.Count>0 ? queue.Dequeue(): null;
        }
    }
}