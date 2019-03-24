using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using FluentAssertions;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyFirstOrDefaultTests
    {
        [Test]
        public void get_null_when_employees_is_empty()
        {
            var employees = new List<Employee>();

            var actual = JoeyFirstOrDefault(employees);

            Assert.IsNull(actual);
        }

        [Test]
        public void get_employee_when_employees_is_not_empty()
        {
                var employees = new List<string>()
                {"Fish"};

        var actual = JoeyFirstOrDefault(employees);

        Assert.IsNotNull(actual);
        actual.Should().Equals("Fish");
        }

        private string JoeyFirstOrDefault(List<string> employees)
        {
            var sourceEnumerator = employees.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                if (current != null)
                {
                    return current;
                }
            }

            return null;
        }

        private Employee JoeyFirstOrDefault(IEnumerable<Employee> employees)
        {
            var sourceEnumerator = employees.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                if (current != null)
                {
                    return current;
                }
            }

            return null;
        }
    }
}