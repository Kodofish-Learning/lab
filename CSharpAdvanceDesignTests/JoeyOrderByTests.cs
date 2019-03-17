using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    public class CombineKeyCompare<TKey> : IComparer<Employee>
    {
        public CombineKeyCompare(Func<Employee, TKey> keySelector, IComparer<TKey> keyComparer)
        {
            KeySelector = keySelector;
            KeyComparer = keyComparer;
        }

        private Func<Employee, TKey> KeySelector { get; set; }
        private IComparer<TKey> KeyComparer { get; set; }

        public int Compare(Employee element, Employee minElement)
        {
            return KeyComparer.Compare(KeySelector(element), KeySelector(minElement));
        }
    }

    public class ComboCompare : IComparer<Employee>
    {
        public ComboCompare(IComparer<Employee> firstCompare, IComparer<Employee> secondCompare)
        {
            FirstCompare = firstCompare;
            SecondCompare = secondCompare;
        }

        private IComparer<Employee> FirstCompare { get; set; }
        private IComparer<Employee> SecondCompare { get; set; }
        public int Compare(Employee x, Employee y)
        {
            var firstCompare = FirstCompare.Compare(x, y);
            var secondCompare = SecondCompare.Compare(x, y);

            return firstCompare == 0 ? secondCompare : firstCompare;
        }
    }

    [TestFixture]
    public class JoeyOrderByTests
    {
        private IEnumerable<Employee> JoeyOrderBy(IEnumerable<Employee> employees, IComparer<Employee> comboCompare)
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

            var firstKeyCompare = new CombineKeyCompare<string>(element => element.LastName, Comparer<string>.Default);
            var secondKeyCompare = new CombineKeyCompare<string>(element => element.FirstName, Comparer<string>.Default);

            var firstCombo = new ComboCompare(firstKeyCompare, secondKeyCompare);
            var actual = JoeyOrderBy(employees, firstCombo);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }
        
        
        [Test]
        public void lastName_firstName_Age()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 50},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 31},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 32},
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 33},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 20},
            };

            var firstComparer = new CombineKeyCompare<string>(element => element.LastName, Comparer<string>.Default);
            var secondComparer = new CombineKeyCompare<string>(element => element.FirstName, Comparer<string>.Default);

            var firstCombo = new ComboCompare(firstComparer, secondComparer);

            var thirdComparer = new CombineKeyCompare<int>(element => element.Age, Comparer<int>.Default);

            var finalCombo = new ComboCompare(firstCombo, thirdComparer);

            var actual = JoeyOrderBy(employees, finalCombo);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 33},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 32},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 31},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 20},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 50},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }
    }
}