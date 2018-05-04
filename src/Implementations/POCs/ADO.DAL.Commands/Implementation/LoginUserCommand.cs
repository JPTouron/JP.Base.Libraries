using System;
using System.Data;
using LRM.Casiraghi.DAL.Commands.Contracts.Login;
using LRM.Casiraghi.DataAccess.Factories.Contracts;
using LRM.Casiraghi.DataAccess.Contracts;
using LRM.Casiraghi.MVP.Model.Factories;
using LRM.Casiraghi.MVP.Model.Contracts;
using System.Data.SqlClient;

namespace LRM.Casiraghi.DAL.Commands
{
	public class LoginUserCommand : BaseCommand, ILoginUserCommand
	{

		public LoginUserCommand(string login, string password, IDBAccessFactory dbFactory)
			: base(dbFactory)
		{
			Login = login;
			Password = password;
		}

		public override void Execute()
		{
			IDBAccess db = DBFactory.GetDBAccess();
			DataTable dt = db.ValidateUser(Login, Password);

			if (dt.Rows.Count > 0)
			{
				IUser usr = UsersFactory.GetUser();
				usr.Login = dt.Rows[0]["Login"].ToString();
				usr.Password = dt.Rows[0]["Password"].ToString();
				usr.Name = dt.Rows[0]["Nombre"].ToString();
				usr.LastName = dt.Rows[0]["Apellido"].ToString();

				LoggedInUser = usr;
				ExecutionResult = true;
			}

		}

		public string Login { get; private set; }
		public string Password { get; private set; }
		public IUser LoggedInUser { get; private set; }

	}
}
