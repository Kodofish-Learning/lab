using Lab;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyOfTypeTests
    {
        [Test]
        public void get_special_type_value_from_arguments()
        {
            //ActionExecutingContext.ActionArguments: Dictionary<string,object>

            var arguments = new Dictionary<string, object>
            {
                {"model", new Product {Price = 100, Cost = 111}},
                {"validator", new ProductValidator()},
            };

            var validators = JoeyOfType<IValidator<Product>>(arguments);

            Assert.AreEqual(1, validators.Count());
        }

        private IEnumerable<TResult> JoeyOfType<TResult>(Dictionary<string, object> arguments)
        {
            var sourceEnumerator = arguments.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                if (current.Value is TResult type)
                {
                    yield return type;
                }
            }
        }
    }
}