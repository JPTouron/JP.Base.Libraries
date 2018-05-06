using ADO.DAL.Commands.Contracts.Base;

namespace ADO.DAL.Commands.Base
{
    public abstract class BaseCommand : ICommand
    {
        public bool ExecutionResult { get; set; }

        public abstract void Execute();
    }
}