using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using FluentAssertions;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyFirstOrDefaultTests
    {
        [Test]
        public void get_null_when_employees_is_empty()
        {
            var employees = new List<Employee>();

            var actual = employees.JoeyFirstOrDefault();

            Assert.IsNull(actual);
        }

        [Test]
        public void get_employee_when_employees_is_not_empty()
        {
                var employees = new List<string>()
                {"Fish"};

        var actual = employees.JoeyFirstOrDefault();

        Assert.IsNotNull(actual);
        actual.Should().Equals("Fish");
        }
    }
}