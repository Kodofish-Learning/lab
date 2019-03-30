using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyAggregateTests
    {
        [Test]
        public void drawling_money_that_balance_have_to_be_positive()
        {
            var balance = 100.91m;

            var drawlingList = new List<int>
            {
                30, 80, 20, 40, 25
            };

            var actual = JoeyAggregate(drawlingList, balance, (seed, drawing) => seed - ( seed >= drawing ? drawing : 0));

            var expected = 10.91m;

            Assert.AreEqual(expected, actual);
        }

        private decimal JoeyAggregate(IEnumerable<int> drawlingList, decimal balance, Func<decimal, int, decimal> func)
        {
            var seed = balance;
            var sourceEnumerator = drawlingList.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                
                var drawing = sourceEnumerator.Current;
                seed = func(seed, drawing);
            }

            return seed;
        }
    }
}