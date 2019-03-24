using System;
using System.Collections;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeySelectTests
    {
        [Test]
        public void append_fish_to_url()
        {
            var urls = GetUrls();

            var actual = urls.JoeySelect(current => $"{current}/fish");
            var expected = new List<string>
            {
                "http://tw.yahoo.com/fish",
                "https://facebook.com/fish",
                "https://twitter.com/fish",
                "http://github.com/fish",
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Test]
        public void print_employee_name()
        {
            var employees = new List<Employee>()
                        {
                            new Employee(){FirstName = "Chang", LastName = "Fish"},
                            new Employee(){FirstName = "Chen", LastName = "Joey"},
                            new Employee(){FirstName = "Lee", LastName = "Tony"},
                        };
            var actual = employees.JoeySelect(s => $"{s.FirstName} {s.LastName}");
            var expected = new List<string>()
            {
                "Chang Fish",
                "Chen Joey",
                "Lee Tony"
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }
    
        [Test]
        public void print_employee_name_with_Index()
        {
            var employees = new List<Employee>()
            {
                new Employee(){FirstName = "Chang", LastName = "Fish"},
                new Employee(){FirstName = "Chen", LastName = "Joey"},
                new Employee(){FirstName = "Lee", LastName = "Tony"},
            };
            var actual = employees.JoeySelect((index, current) => $"{index}. {current.FirstName} {current.LastName}") ;
            var expected = new List<string>()
            {
                "1. Chang Fish",
                "2. Chen Joey",
                "3. Lee Tony"
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }
        
        [Test]
        public void replace_http_to_https()
        {
            var urls = GetUrls();

            var actual = urls.JoeySelect(current => current.Replace("http:", "https:"));
            var expected = new List<string>
            {
                "https://tw.yahoo.com",
                "https://facebook.com",
                "https://twitter.com",
                "https://github.com",
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        private static IEnumerable<string> GetUrls()
        {
            yield return "http://tw.yahoo.com";
            yield return "https://facebook.com";
            yield return "https://twitter.com";
            yield return "http://github.com";
        }

        private static IEnumerable GetEmployees()
        {
            return new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"}
            };
        }
    }
}