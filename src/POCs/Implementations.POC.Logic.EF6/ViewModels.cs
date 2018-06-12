using JP.Base.Logic.ViewModel;
using System.Collections.Generic;

namespace Implementations.POC.Logic.EF6
{
    public class EmployeeVM : BaseViewModelConcurrent<int>
    {
        public int Age { get; set; }

        public string EmployerArea { get; set; }
        public int EmployerId { get; set; }
        public string EmployerName { get; set; }
        public string Function { get; set; }
        public string Name { get; set; }
    }

    public class EmployerVM : BaseViewModelConcurrent<int>
    {
        public string Area { get; set; }
        public IEnumerable<EmployeeVM> Employees { get; set; }
        public string Name { get; set; }
    }
}