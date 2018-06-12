using JP.Base.DAL.Model.Concurrent;
using System.Collections.Generic;

namespace Implementations.POC.Logic.EF6
{
    public class Employee : BaseModelConcurrent<int>
    {
        public int Age { get; set; }
        public Employer Employer { get; set; }
        public int EmployerId { get; set; }
        public string Function { get; set; }
        public string Name { get; set; }
    }

    public class Employer : BaseModelConcurrent<int>
    {
        public string Area { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public string Name { get; set; }
    }
}