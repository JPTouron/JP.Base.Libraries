using LRM.Casiraghi.Commands.Users;
using LRM.Casiraghi.Commands.Users.Interfaces;
using LRM.Casiraghi.Contracts.Model;
using LRM.Casiraghi.DataAccess;

namespace LRM.Casiraghi.Commands.Factories
{
	public static class UsersCommandsFactory
	{
		public static ICreateUserCommand GetCreateUserCommand(IUser user)
		{
			return new CreateUserCommand(user,new DBAccessFactory());
		}

		public static IDeleteUserCommand GetDeleteUserCommand(IUser user)
		{
			return new DeleteUserCommand(user, new DBAccessFactory());
		}

		public static IGetAllUsersCommand GetGetAllUsersCommand()
		{
			return new GetAllUsersCommand(new DBAccessFactory());
		}

		public static IUpdateUserCommand GetUpdateUserCommand(IUser user)
		{
			return new UpdateUserCommand(user, new DBAccessFactory());
		}

	}
}
