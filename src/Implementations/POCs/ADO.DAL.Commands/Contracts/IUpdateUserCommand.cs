using LRM.Casiraghi.DAL.Commands.Contracts.Base;
using LRM.Casiraghi.DAL.EF6.Model;

namespace LRM.Casiraghi.DAL.Commands.Contracts
{
    public interface IUpdateUserCommand : ICommand
    {
        User User { get; }
    }
}