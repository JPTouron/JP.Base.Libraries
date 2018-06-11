using JP.Base.ViewModel;

namespace Implementations.POC.Logic.ADO
{
    public class ClientVM : BaseVersionViewModel<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class OperatorWithClientVM : BaseViewModel<int>
    {
        public ClientVM Client { get; set; }
        public string Document { get; set; }
        public int EmployeeNbr { get; set; }
        public string FirstName { get; set; }
        public bool IsActive { get; set; }
        public string LastName { get; set; }
    }
}