using System;
using System.Collections.Generic;
using Lab.Entities;

namespace Lab.EqualityComparer
{
    public class JoeyEmployeeWithPhoneEqualityComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.FirstName == x.FirstName && x.LastName == y.LastName;
        }

        public int GetHashCode(Employee obj)
        {
            throw new NotImplementedException();
        }
    }
}