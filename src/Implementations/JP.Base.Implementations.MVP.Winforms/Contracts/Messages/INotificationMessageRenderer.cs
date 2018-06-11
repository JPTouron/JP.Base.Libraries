namespace JP.Base.Implementations.MVP.Winforms.Contracts.Messages
{
    public interface INotificationMessageRenderer
    {
        void DisplayNotificationMessage(string message, string title);
    }
}