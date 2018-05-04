using LRM.Casiraghi.DAL.Commands.Contracts.Base;
using LRM.Casiraghi.DAL.EF6.Model;

namespace LRM.Casiraghi.DAL.Commands.Contracts
{
    public interface ILoginUserCommand : ICommand
    {
        User LoggedInUser { get; }
        string Login { get; }
        string Password { get; }
    }
}