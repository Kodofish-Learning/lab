﻿using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyFirstTests
    {
        [Test]
        public void get_first_girl()
        {
            var girls = new[]
            {
                new Girl(){Age = 10},
                new Girl(){Age = 20},
                new Girl(){Age = 30},
            };

            var girl = JoeyFirst(girls);
            var expected = new Girl { Age = 10 };

            expected.ToExpectedObject().ShouldEqual(girl);
        }

        private Girl JoeyFirst(IEnumerable<Girl> girls)
        {
            var firstElement = default(Girl);
            var sourceEnumerator = girls.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                firstElement = current;
                break;
            }

            return firstElement;
        }
    }
}