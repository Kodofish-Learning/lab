using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class EmployeeComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.FirstName == x.FirstName && x.LastName == y.LastName;
        }

        public int GetHashCode(Employee obj)
        {
            return new {obj.FirstName, obj.LastName}.GetHashCode();
        }
    }
}