using System.Collections.Generic;
using ExpectedObjects;
using Lab.Entities;
using Lab.Extensions;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyLastOrDefaultTests
    {
        [Test]
        public void get_Last_when_employees_is_not_empty()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Fish", LastName = "Chang"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "David", LastName = "Ko"}
            };
            var expected = new Employee {FirstName = "David", LastName = "Ko"};
            var actual = employees.JoeyLastOrDefault();
            expected.ToExpectedObject().Equals(actual);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void get_null_when_employees_is_empty()
        {
            var employees = new List<Employee>();
            var actual = employees.JoeyLastOrDefault();
            Assert.IsNull(actual);
        }
    }
}