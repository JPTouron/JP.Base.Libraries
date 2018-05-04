using LRM.Casiraghi.DAL.Commands.Contracts.Base;

namespace LRM.Casiraghi.DAL.Commands.Contracts
{
    public interface ICreateUserCommand : ICommand
    {
        object User { get; }
    }
}