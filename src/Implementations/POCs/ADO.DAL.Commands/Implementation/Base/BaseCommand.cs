using LRM.Casiraghi.DAL.Commands.Contracts.Base;

namespace LRM.Casiraghi.DAL.Commands.Base
{
    public abstract class BaseCommand : ICommand
    {
        public bool ExecutionResult { get; set; }

        public abstract void Execute();
    }
}