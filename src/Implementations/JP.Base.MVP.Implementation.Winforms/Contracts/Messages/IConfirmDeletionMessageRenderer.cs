namespace JP.Base.MVP.Implementation.Winforms.Contracts.Messages
{
    public interface IConfirmDeletionMessageRenderer
    {
        bool DisplayConfirmDeleteMessage(string message, string title);
    }
}