using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyAggregateTests
    {
        private string JoeyAggregate
        (IEnumerable<int> drawlingList, 
            decimal balance, 
            Func<decimal, decimal, decimal> func, 
            Func<decimal, string> selector)
        {
            var seed = balance;
            var drawlingListEnumerator = drawlingList.GetEnumerator();
            while (drawlingListEnumerator.MoveNext())
            {
                var drawling = drawlingListEnumerator.Current;
                seed = func(seed, drawling);
            }

            return selector(seed);
        }

//        [Test]
//        public void drawling_money_that_balance_have_to_be_positive()
//        {
//            var balance = 100.91m;
//
//            var drawlingList = new List<int>
//            {
//                30, 80, 20, 40, 25
//            };
//
//            var actual = JoeyAggregate(drawlingList, balance,
//                (seed, drawling) => seed - (seed >= drawling ? drawling : 0));
//
//            var expected = 10.91m;
//
//            Assert.AreEqual(expected, actual);
//        }
        
        [Test]
        public void drawling_money_that_balance_have_to_be_positive2()
        {
            var balance = 100.91m;

            var drawlingList = new List<int>
            {
                30, 80, 20, 40, 25
            };

            var actual = JoeyAggregate(drawlingList, balance,
                (seed, drawling) => seed - (seed >= drawling ? drawling : 0), seed1 => seed1.ToString());

            var expected = "10.91";

            Assert.AreEqual(expected, actual);
        }
    }
}