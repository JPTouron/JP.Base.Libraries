using JP.Base.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.POC.Logic.ADO
{
    public class ClientVM : BaseViewModel<int>
    {

        public string Code { get; set; }
        public string Name { get; set; }

    }


    public class OperatorWithClientVM : BaseViewModel<int>
    {
        public string Document { get; set; }
        public int EmployeeNbr { get; set; }
        public string FirstName { get; set; }
        public bool IsActive { get; set; }
        public string LastName { get; set; }

        public ClientVM Client { get; set; }
    }

}
