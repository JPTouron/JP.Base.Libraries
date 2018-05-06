using ADO.DAL.Commands.Contracts;

namespace ADO.DAL.Commands
{
    public class CreateUserCommand : Base.BaseCommand, ICreateUserCommand
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