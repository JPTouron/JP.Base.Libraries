using System.Windows.Forms;
using JP.Base.MVP.Implementation.Winforms.Contracts.Messages;

namespace JP.Base.MVP.Implementation.Winforms.Implementation.Messages
{
    public class MsgBoxMessageRenderer : IMessageRenderer
    {
        public bool DisplayConfirmDeleteMessage(string message, string title)
        {
            DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return result.Equals(DialogResult.Yes);
        }

        public void DisplayNotificationMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ocurrió un error de sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}