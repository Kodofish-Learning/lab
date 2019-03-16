using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    public class CombineKeyCompare : IComparer<Employee>
    {
        public CombineKeyCompare(Func<Employee, string> keySelector, IComparer<string> keyComparer)
        {
            KeySelector = keySelector;
            KeyComparer = keyComparer;
        }

        private Func<Employee, string> KeySelector { get; set; }
        private IComparer<string> KeyComparer { get; set; }

        public int Compare(Employee element, Employee minElement)
        {
            return KeyComparer.Compare(KeySelector(element), KeySelector(minElement));
        }
    }

    public class ComboCompare : IComparer<Employee>
    {
        public ComboCompare(CombineKeyCompare combineKeyCompare, CombineKeyCompare secondKeyCompare)
        {
            CombineKeyCompare = combineKeyCompare;
            SecondKeyCompare = secondKeyCompare;
        }

        public CombineKeyCompare CombineKeyCompare { get; private set; }
        public CombineKeyCompare SecondKeyCompare { get; private set; }
        public int Compare(Employee x, Employee y)
        {
            var firstCompare = CombineKeyCompare.Compare(x, y);
            var secondCompare = SecondKeyCompare.Compare(x, y);

            return firstCompare == 0 ? secondCompare : firstCompare;
        }
    }

    [TestFixture]
    public class JoeyOrderByTests
    {
        private IEnumerable<Employee> JoeyOrderByLastName(IEnumerable<Employee> employees, ComboCompare comboCompare)
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

                    if (comboCompare.Compare(element, minElement) < 0)
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

            var firstKeyCompare = new CombineKeyCompare(element => element.LastName, StringComparer.Create(CultureInfo.CurrentCulture, true));
            var secondKeyCompare = new CombineKeyCompare(element => element.FirstName, StringComparer.Create(CultureInfo.CurrentCulture, true));
            
            var actual = JoeyOrderByLastName(employees, new ComboCompare(firstKeyCompare, secondKeyCompare));

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