using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyContainsTests
    {
        [Test]
        public void contains_joey_chen()
        {
            var employees = new List<Employee>
            {
                new Employee(){FirstName = "Joey", LastName = "Wang"},
                new Employee(){FirstName = "Tom", LastName = "Li"},
                new Employee(){FirstName = "Joey", LastName = "Chen"},
            };

            var joey = new Employee() { FirstName = "Joey", LastName = "Chen" };

            var actual = employees.JoeyContains(joey);

            Assert.IsTrue(actual);
        }
    }
}