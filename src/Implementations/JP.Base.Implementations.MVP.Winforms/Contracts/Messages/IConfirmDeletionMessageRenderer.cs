namespace JP.Base.Implementations.MVP.Winforms.Contracts.Messages
{
    public interface IConfirmDeletionMessageRenderer
    {
        bool DisplayConfirmDeleteMessage(string message, string title);
    }
}