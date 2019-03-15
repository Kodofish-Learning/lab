using System;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using Lab;
using Lab.Extensions;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyWhereTests
    {
        [Test]
        public void find_products_that_price_between_200_and_500()
        {
            var products = new List<TSource>
            {
                new TSource {Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e"},
                new TSource {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new TSource {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new TSource {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
                new TSource {Id = 5, Cost = 51, Price = 510, Supplier = "Momo"},
                new TSource {Id = 6, Cost = 61, Price = 610, Supplier = "Momo"},
                new TSource {Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo"},
                new TSource {Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo"}
            };

            var actual = products.FishWhere(product => product.Price >200 && product.Price < 500);

            var expected = new List<TSource>
            {
                new TSource {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new TSource {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new TSource {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void group_and_sum()
        {
            var products = new List<TSource>
            {
                new TSource {Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e"},
                new TSource {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new TSource {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new TSource {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
                new TSource {Id = 5, Cost = 51, Price = 510, Supplier = "Momo"},
                new TSource {Id = 6, Cost = 61, Price = 610, Supplier = "Momo"},
                new TSource {Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo"},
                new TSource {Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo"}
            };

            var actual = FishGroupSum(products, 3, it => it.Cost);

            var expected = new[] { 63, 153, 89};
            expected.ToExpectedObject().ShouldMatch(actual); 
        }

        private IEnumerable<int> FishGroupSum(IEnumerable<TSource> products, int groupSize, Func<TSource, int> selector)
        {
            var count = groupSize;
            var index = 0;
            var i = products.Count();
            while (count*index < i)
            {
                yield return products.Skip(index*count).Take(count).Sum(selector);
                index++;
            }
        }
        
        

        [Test]
        public void find_products_that_price_between_200_and_500_and_cost_more_than_30()
        {
            var products = new List<TSource>
            {
                new TSource {Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e"},
                new TSource {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new TSource {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new TSource {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
                new TSource {Id = 5, Cost = 51, Price = 510, Supplier = "Momo"},
                new TSource {Id = 6, Cost = 61, Price = 610, Supplier = "Momo"},
                new TSource {Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo"},
                new TSource {Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo"}
            };

            var actual = products.FishWhere(product => product.Price>200 && product.Price<500 && product.Cost > 30);

            var expected = new List<TSource>
            {
                new TSource {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new TSource {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }
               
        [Test]
        public void find_products_that_price_between_200_and_500_and_cost_more_than_30_use_IEnumerable()
        {
            var products = new List<TSource>
            {
                new TSource {Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e"},
                new TSource {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new TSource {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new TSource {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
                new TSource {Id = 5, Cost = 51, Price = 510, Supplier = "Momo"},
                new TSource {Id = 6, Cost = 61, Price = 610, Supplier = "Momo"},
                new TSource {Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo"},
                new TSource {Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo"}
            };

            var actual = products
                .FishWhere(product => product.Price>200 && product.Price<500 && product.Cost > 30)
                .JoeySelect(p=>p.Price);

            var expected = new []
            {
                310,
                410
            };
            
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        
        [Test]
        public void find_odd_names()
        {
            var names = new List<string> {"Joey", "Cash", "William", "Sam", "Brian", "Jessica"};
            var actual = names.FishWhere((n, i) => i % 2 == 0);
            var expected = new[]
            {
                "Joey", "William", "Brian"
            };
            expected.ToExpectedObject().ShouldMatch(actual);
        }
    }
}