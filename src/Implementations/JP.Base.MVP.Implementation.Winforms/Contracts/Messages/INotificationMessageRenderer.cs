namespace JP.Base.MVP.Implementation.Winforms.Contracts.Messages
{
    public interface INotificationMessageRenderer
    {
        void DisplayNotificationMessage(string message, string title);
    }
}