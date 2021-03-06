﻿using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyJoinTests
    {
        [Test]
        public void all_pets_and_owner()
        {
            var david = new Employee { FirstName = "David", LastName = "Li" };
            var joey = new Employee { FirstName = "Joey", LastName = "Chen" };
            var tom = new Employee { FirstName = "Tom", LastName = "Wang" };

            var employees = new[]
            {
                david,
                joey,
                tom
            };

            var pets = new Pet[]
            {
                new Pet() {Name = "Lala", Owner = joey},
                new Pet() {Name = "Didi", Owner = david},
                new Pet() {Name = "Fufu", Owner = tom},
                new Pet() {Name = "QQ", Owner = joey},
            };

            var actual = JoeyJoin(employees, pets, 
                e => e, 
                p => p.Owner, 
                (employee, pet) => $"{pet.Name} {employee.LastName}");

            var expected = new[]
            {
                "Didi Li",
                "Lala Chen",
                "QQ Chen",
                "Fufu Wang"
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void all_pets_and_owner_and_get_result()
        {
            var david = new Employee { FirstName = "David", LastName = "Chen" };
            var joey = new Employee { FirstName = "Joey", LastName = "Chen" };
            var tom = new Employee { FirstName = "Tom", LastName = "Chen" };

            var employees = new[]
            {
                david,
                joey,
                tom
            };

            var pets = new Pet[]
            {
                new Pet() {Name = "Lala", Owner = joey},
                new Pet() {Name = "Didi", Owner = david},
                new Pet() {Name = "Fufu", Owner = tom},
                new Pet() {Name = "QQ", Owner = joey},
            };

            var actual = JoeyJoin(employees, pets, 
                employee => employee, 
                pet => pet.Owner, 
                (employee, pet) => Tuple.Create(employee.FirstName, pet.Name));

            var expected = new[]
            {
                Tuple.Create("David", "Didi"),
                Tuple.Create("Joey", "Lala"),
                Tuple.Create("Joey", "QQ"),
                Tuple.Create("Tom", "Fufu"),
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TResult> JoeyJoin<TFirst, TSecond, TKey, TResult>(
            IEnumerable<TFirst> inner,
            IEnumerable<TSecond> outer, 
            Func<TFirst, TKey> innerKeySelector, 
            Func<TSecond, TKey> outerKeySelector,
            Func<TFirst, TSecond, TResult> resultSelector)
        {
            var innerEnumerator = inner.GetEnumerator();
                var outerEnumerator = outer.GetEnumerator();
            while (innerEnumerator.MoveNext())
            {
                var innerElement = innerEnumerator.Current;

                while (outerEnumerator.MoveNext())
                {
                    var outerElement = outerEnumerator.Current;
                    if (outerKeySelector(outerElement).Equals(innerKeySelector(innerElement)))
                    {
                        yield return resultSelector(innerElement, outerElement);
                    }
                }
                outerEnumerator.Reset();
            }
        }
    }
}