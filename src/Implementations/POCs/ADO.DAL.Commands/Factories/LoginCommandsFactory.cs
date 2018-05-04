using LRM.Casiraghi.Commands.Login;

namespace LRM.Casiraghi.Commands.Factories
{
    public static class LoginCommandsFactory
    {
        public static ILoginUserCommand GetLoginUserCommand(string login, string password)
        {
            return new LoginUserCommand(login, password, new DBAccessFactory());
        }
    }
}