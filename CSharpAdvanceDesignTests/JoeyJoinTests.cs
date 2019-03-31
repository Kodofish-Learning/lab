using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyJoinTests
    {
        [Test]
        public void all_pets_and_owner()
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

            var actual = JoeyJoin(employees, pets, (employee, pet) => new Tuple<string, string>(employee.FirstName, pet.Name));

            var expected = new[]
            {
                Tuple.Create("David", "Didi"),
                Tuple.Create("Joey", "Lala"),
                Tuple.Create("Joey", "QQ"),
                Tuple.Create("Tom", "Fufu"),
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Tuple<string, string>> JoeyJoin(IEnumerable<Employee> employees, 
            IEnumerable<Pet> pets, 
            Func<Employee, Pet, 
                Tuple<string, string>> selector)
        {
            var sourceEnumerator = employees.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                var employee = sourceEnumerator.Current;
                var petEnumerator = pets.GetEnumerator();
                while (petEnumerator.MoveNext())
                {
                    var pet = petEnumerator.Current;
                    if (employee.Equals(pet.Owner))
                    {
                        yield return selector(employee, pet);
                    }
                }
                
            }
        }
    }
}