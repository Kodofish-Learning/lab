﻿using System;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyAverageTests
    {
        [Test]
        public void average_with_null_value()
        {
            var numbers = new int?[] { 2, 4, null, 6 };

            var actual = JoeyAverage(numbers);

            actual.Should().Be(4);
        }

        private double? JoeyAverage(IEnumerable<int?> numbers)
        {
            var sum = numbers.Sum();
            var count = numbers.Count(it=>it!=null);
            return sum / count;
        }
    }
}