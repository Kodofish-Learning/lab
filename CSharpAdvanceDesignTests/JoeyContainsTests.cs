using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Lab.EqualityComparer;

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

            var actual = JoeyContains(employees, joey);

            Assert.IsTrue(actual);
        }

        private static bool JoeyContains(IEnumerable<Employee> employees, Employee value)
        {
            var sourceEnumerator = employees.GetEnumerator();
            var comparer = new JoeyEmployeeWithPhoneEqualityComparer();
            
            while (sourceEnumerator.MoveNext())
            {
                var item = sourceEnumerator.Current;
                if (comparer.Equals(item, value))
                {
                    return true;
                }
            }

            return false;
        }
    }
}