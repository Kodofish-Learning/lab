using Lab;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyOfTypeTests
    {
        [Test]
        public void get_special_type_value_from_arguments_2()
        {
            //ActionExecutingContext.ActionArguments: Dictionary<string,object>

            var arguments = new Dictionary<string, object>
            {
                {"model", new Product {Price = 100, Cost = 111}},
                {"validator", new ProductValidator()},
            };

            var validators = JoeyOfType<IValidator<Product>>(arguments.Values);

            var products = JoeyOfType<Product>(arguments.Values).Single();
            var isValid = validators.All(x => x.Validate(products));
            Assert.IsFalse(isValid);
        }
        [Test]
        public void get_special_type_value_from_arguments()
        {
            //ActionExecutingContext.ActionArguments: Dictionary<string,object>

            var arguments = new Dictionary<string, object>
            {
                {"model", new Product {Price = 100, Cost = 111}},
                {"validator", new ProductValidator()},
            };

            var validators = JoeyOfType<IValidator<Product>>(arguments.Values);

            Assert.AreEqual(1, validators.Count());
        }
        
        [Test]
        public void get_special_type_value_from_arguments_1()
        {
            //ActionExecutingContext.ActionArguments: Dictionary<string,object>

            var arguments = new Dictionary<string, object>
            {
                {"model", new Product {Price = 100, Cost = 111}},
                {"validator", new ProductValidator()},
                {"validator2", new ProductValidator()},
            };

            var validators = JoeyOfType<IValidator<Product>>(arguments.Values);

            Assert.AreEqual(2, validators.Count());
        }

        private IEnumerable<TResult> JoeyOfType<TResult>(Dictionary<string, object>.ValueCollection arguments)
        {
            var sourceEnumerator = arguments.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                if (current is TResult item)
                    yield return item;
            }
        }
    }
}