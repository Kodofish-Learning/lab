﻿using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeySelectTests
    {
        [Test]
        public void replace_http_to_https()
        {
            var urls = GetUrls();

            var actual = JoeySelect(urls, url => url.Replace("http:", "https:"));
            var expected = new List<string>
            {
                "https://tw.yahoo.com",
                "https://facebook.com",
                "https://twitter.com",
                "https://github.com",
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        
        [Test]
        public void replace_http_to_https_and_append_joey()
        {
            var urls = GetUrls().ToList();

            var actual = JoeySelect(urls, url => url.Replace("http:", "https:") + "/joey");
            var expected = new List<string>
            {
                "https://tw.yahoo.com/joey",
                "https://facebook.com/joey",
                "https://twitter.com/joey",
                "https://github.com/joey",
            };

            expected.ToExpectedObject().ShouldMatch(actual.ToList());
        }

        [Test]
        public void replace_http_to_https_url_mask()
        {
            var urls = GetUrls().ToList();

            var actual = JoeySelect(urls, url => url.Replace("http","****") + "/joey");
            var expected = new List<string>
            {
                "****://tw.yahoo.com/joey",
                "****s://facebook.com/joey",
                "****s://twitter.com/joey",
                "****://github.com/joey",
            };

            expected.ToExpectedObject().ShouldMatch(actual.ToList());
        }
        
        
        [Test]
        public void get_full_name()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"}
            };
            var expected = new[]
            {
                "Joey-Chen",
                "Tom-Li",
                "David-Chen",
            };

            var actual = JoeySelect(employees, e => $"{e.FirstName}-{e.LastName}");
            expected.ToExpectedObject().ShouldMatch(actual); 
        }

        private IEnumerable<string> JoeySelect<T>(IEnumerable<T> urls, Func<T, string> mapper)
        {
            var list = new List<string>();
            foreach (var url in urls)
            {
                
               list.Add(mapper(url));
            }

            return list;
        }

        private static IEnumerable<string> GetUrls()
        {
            yield return "http://tw.yahoo.com";
            yield return "https://facebook.com";
            yield return "https://twitter.com";
            yield return "http://github.com";
        }

        private static List<Employee> GetEmployees()
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