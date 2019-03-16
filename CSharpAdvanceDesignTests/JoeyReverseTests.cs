using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyReverseTests
    {
        [Test]
        public void reverse_employees()
        {
            var employees = new List<Employee>
            {
                new Employee(){FirstName = "Joey",LastName = "Chen"},
                new Employee(){FirstName = "Tom",LastName = "Li"},
                new Employee(){FirstName = "David",LastName = "Wang"},
            };

            var actual = JoeyReverse2(employees);

            var expected = new List<Employee>
            {
                new Employee(){FirstName = "David",LastName = "Wang"},
                new Employee(){FirstName = "Tom",LastName = "Li"},
                new Employee(){FirstName = "Joey",LastName = "Chen"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyReverse(IEnumerable<Employee> employees)
        {
            return new Stack<Employee>(employees);
        }
        
        private IEnumerable<Employee> JoeyReverse1(IEnumerable<Employee> employees)
        {
            var stack = new Stack<Employee>(employees);
            var sourceEnumerator = stack.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                yield return sourceEnumerator.Current;
            }
        }
        
        private IEnumerable<Employee> JoeyReverse2(IEnumerable<Employee> employees)
        {
            var stack = new Stack<Employee>(employees);
            var sourceEnumerator = stack.GetEnumerator();
            while (stack.Any())
            {
                yield return stack.Pop();
            }
        }

    }
}