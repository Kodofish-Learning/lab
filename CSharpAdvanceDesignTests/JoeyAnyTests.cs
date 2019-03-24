using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyAnyTests
    {
        [Test]
        public void three_employees()
        {
            var emptyEmployees = new Employee[]
            {
                new Employee(),
                new Employee(),
                new Employee(),
            };

            var actual = JoeyAny(emptyEmployees);
            Assert.IsTrue(actual);
        }

        [Test]
        public void empty_employees()
        {
            var emptyEmployees = new Employee[]
            {
            };

            var actual = JoeyAny(emptyEmployees);
            Assert.IsFalse(actual);
        }

        private bool JoeyAny(IEnumerable<Employee> employees)
        {
            var sourceEnumerator = employees.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                if (current != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}