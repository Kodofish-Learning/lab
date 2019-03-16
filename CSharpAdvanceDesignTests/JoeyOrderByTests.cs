using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    public class CombineKeyCompare
    {
        public CombineKeyCompare(Func<Employee, string> keySelector, IComparer<string> keyComparer)
        {
            KeySelector = keySelector;
            KeyComparer = keyComparer;
        }

        public Func<Employee, string> KeySelector { get; private set; }
        public IComparer<string> KeyComparer { get; private set; }
    }

    [TestFixture]
    public class JoeyOrderByTests
    {
        private IEnumerable<Employee> JoeyOrderByLastName(IEnumerable<Employee> employees, CombineKeyCompare combineKeyCompare,
            Func<Employee, string> secondKeySelector, IComparer<string> secondKeyComparer)
        {
            //bubble sort
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (var i = 1; i < elements.Count; i++)
                {
                    var element = elements[i];

                    if (combineKeyCompare.KeyComparer.Compare(combineKeyCompare.KeySelector(element), combineKeyCompare.KeySelector(minElement)) == 0
                        && secondKeyComparer
                            .Compare(secondKeySelector(element), secondKeySelector(minElement)) < 0 ||
                        combineKeyCompare.KeyComparer.Compare(combineKeyCompare.KeySelector(element), combineKeyCompare.KeySelector(minElement)) < 0)
                    {
                        minElement = elements[i];
                        index = i;
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }
//        [Ignore("temp")]
//        public void orderBy_lastName()
//        {
//            var employees = new[]
//            {
//                new Employee {FirstName = "Joey", LastName = "Wang"},
//                new Employee {FirstName = "Tom", LastName = "Li"},
//                new Employee {FirstName = "Joseph", LastName = "Chen"},
//                new Employee {FirstName = "Joey", LastName = "Chen"},
//            };
//
//            var actual = JoeyOrderByLastName(employees, element => element.LastName);
//
//            var expected = new[]
//            {
//                new Employee {FirstName = "Joseph", LastName = "Chen"},
//                new Employee {FirstName = "Joey", LastName = "Chen"},
//                new Employee {FirstName = "Tom", LastName = "Li"},
//                new Employee {FirstName = "Joey", LastName = "Wang"},
//            };
//
//            expected.ToExpectedObject().ShouldMatch(actual);
//        }

        [Test]
        public void orderBy_lastName_and_firstName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"}
            };

            var actual = JoeyOrderByLastName(employees, 
                new CombineKeyCompare(element => element.LastName, StringComparer.Create(CultureInfo.CurrentCulture, true)), 
                element => element.FirstName, StringComparer.Create(CultureInfo.CurrentCulture, true));

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }
    }
}