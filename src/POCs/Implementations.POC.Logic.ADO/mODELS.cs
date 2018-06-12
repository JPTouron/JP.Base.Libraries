using JP.Base.Implementations.DAL.Model.Concurrent;
using JP.Base.Implementations.DAL.Model;

namespace Implementations.POC.Logic.ADO
{
    public class Client : BaseModelConcurrent<int>

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