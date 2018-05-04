using System;
using LRM.Casiraghi.DAL.Commands.Contracts.Users;
using LRM.Casiraghi.DataAccess.Factories.Contracts;
using LRM.Casiraghi.DataAccess.Contracts;
using LRM.Casiraghi.MVP.Model.Contracts;
using System.Data.SqlClient;

namespace LRM.Casiraghi.DAL.Commands
{
    public class UpdateUserCommand : BaseCommand, IUpdateUserCommand
    {

        public UpdateUserCommand(IUser user, IDBAccessFactory dbFactory)
            : base(dbFactory)
        {
            User = user;
        }

        public override void Execute()
        {

            if (User != null)
            {
                IDBAccess db = DBFactory.GetDBAccess();
                ExecutionResult = db.UpdateUser(User);
            }
        }

        public IUser User { get; private set; }

    }
}
