using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyZipTests
    {
        [Test]
        public void pair_girls_and_keys()
        {
            var girls = new List<Girl>
            {
                new Girl() {Name = "Mary"},
                new Girl() {Name = "Jessica"},
            };

            var keys = new List<Key>
            {
                new Key() {Type = CardType.BMW, Owner = "Joey"},
                new Key() {Type = CardType.TOYOTA, Owner = "David"},
                new Key() {Type = CardType.Benz, Owner = "Tom"},
            };

            var pairs = JoeyZip(girls, keys, (girlsEnumeratorCurrent, keysEnumeratorCurrent) => $"{girlsEnumeratorCurrent.Name}-{keysEnumeratorCurrent.Owner}");

            var expected = new[]
            {
                "Mary-Joey",
                "Jessica-David",
            };

            expected.ToExpectedObject().ShouldMatch(pairs);
        }

        [Test]
        public void pair_girls_and_keys_CardType()
        {
            var girls = new List<Girl>
            {
                new Girl() {Name = "Mary"},
                new Girl() {Name = "Jessica"},
            };

            var keys = new List<Key>
            {
                new Key() {Type = CardType.BMW, Owner = "Joey"},
                new Key() {Type = CardType.TOYOTA, Owner = "David"},
                new Key() {Type = CardType.Benz, Owner = "Tom"},
            };

            var pairs = JoeyZip(girls, keys, (girlsEnumeratorCurrent, keysEnumeratorCurrent) => $"{girlsEnumeratorCurrent.Name}-{keysEnumeratorCurrent.Type}");

            var expected = new[]
            {
                "Mary-BMW",
                "Jessica-TOYOTA",
            };

            expected.ToExpectedObject().ShouldMatch(pairs);
        }

        private IEnumerable<string> JoeyZip<TSource, TSource1>(IEnumerable<TSource> girls, IEnumerable<TSource1> keys, Func<TSource, TSource1, string> selector)
        {
            var girlsEnumerator = girls.GetEnumerator();
            var keysEnumerator = keys.GetEnumerator();

            while (girlsEnumerator.MoveNext() && keysEnumerator.MoveNext())
            {
                var girlsEnumeratorCurrent = girlsEnumerator.Current;
                var keysEnumeratorCurrent = keysEnumerator.Current;
                yield return selector(girlsEnumeratorCurrent, keysEnumeratorCurrent);
            }
            
        }
    }
}