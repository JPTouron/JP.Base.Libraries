using ADO.DAL.Commands.Contracts.Base;

namespace ADO.DAL.Commands.Contracts
{
    public interface ICreateUserCommand : ICommand
    {
        object User { get; }
    }
}