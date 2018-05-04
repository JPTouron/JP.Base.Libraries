using System;
using System.Collections.Generic;
using System.Data;
using LRM.Casiraghi.DAL.Commands.Contracts.Users;
using LRM.Casiraghi.DataAccess.Factories.Contracts;
using LRM.Casiraghi.DataAccess.Contracts;
using LRM.Casiraghi.MVP.Model.Factories;
using LRM.Casiraghi.MVP.Model.Contracts;
using System.Data.SqlClient;

namespace LRM.Casiraghi.DAL.Commands
{
	public class GetAllUsersCommand : BaseCommand, IGetAllUsersCommand
	{

		public GetAllUsersCommand(IDBAccessFactory dbFactory) : base(dbFactory) { }

		public override void Execute()
		{
			IDBAccess db = DBFactory.GetDBAccess();
			DataTable dt = db.GetAllUsers();

			if (dt.Rows.Count > 0)
			{
				ReturnedUsers = new List<IUser>();
				foreach (DataRow row in dt.Rows)
				{
					IUser usr = UsersFactory.GetUser();
					usr.Login = row["Login"].ToString();
					usr.Password = row["Password"].ToString();
					usr.Name = row["Nombre"].ToString();
					usr.LastName = row["Apellido"].ToString();
					usr.UserId = (int)row["IdUsuario"];
					usr.Rights = (Permission)row["IdPermiso"];

					ReturnedUsers.Add(usr);
				}
				ExecutionResult = true;
			}

		}

		public IList<IUser> ReturnedUsers { get; private set; }

	}
}
