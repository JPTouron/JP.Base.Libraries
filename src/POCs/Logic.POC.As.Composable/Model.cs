using JP.Base.Implementations.DAL.Model;

namespace Logic.POC.As.Composable
{
    public class Operator : BaseModel<int>
    {
        public User Client { get; set; }
        public string Document { get; set; }
        public int EmployeeNbr { get; set; }
        public string FirstName { get; set; }
        public bool IsActive { get; set; }
        public string LastName { get; set; }
    }

    public class User : BaseModel<int>

    {
        public int Role { get; set; }
        public string UserName { get; set; }
    }
}