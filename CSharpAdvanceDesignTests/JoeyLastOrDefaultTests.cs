using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyLastOrDefaultTests
    {
        [Test]
        public void get_null_when_employees_is_empty()
        {
            var employees = new List<Employee>();
            var actual = JoeyLastOrDefault(employees);
            Assert.IsNull(actual);
        }

        
        private Employee JoeyLastOrDefault(IEnumerable<Employee> employees)
        {
            var queue = new Queue<Employee>(employees);
            return queue.Count>0 ? queue.Dequeue(): null;
        }
    }
}