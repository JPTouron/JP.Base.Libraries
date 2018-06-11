using JP.Base.DAL.EF6.Model;
using JP.Base.DAL.Model;

namespace Implementations.POC.Logic.ADO
{
    public class Client : BaseModelEf<int>

    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class OperatorWithClient : BaseModel<int>
    {
        public Client Client { get; set; }
        public string Document { get; set; }
        public int EmployeeNbr { get; set; }
        public string FirstName { get; set; }
        public bool IsActive { get; set; }
        public string LastName { get; set; }
    }
}