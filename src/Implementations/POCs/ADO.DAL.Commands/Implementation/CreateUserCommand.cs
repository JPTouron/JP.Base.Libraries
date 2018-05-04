using ADO.DAL.Commands.Base;
using ADO.DAL.Commands.Contracts;

namespace ADO.DAL.Commands
{
    public class CreateUserCommand : BaseCommand, ICreateUserCommand
    {
        public CreateUserCommand(object user)
        {
            User = user;
        }

        public object User { get; private set; }

        public override void Execute()
        {
            if (User != null)
            {
                var db = DBFactory.GetDBAccess();
                ExecutionResult = db.CreateUser(User);
            }
        }
    }
}