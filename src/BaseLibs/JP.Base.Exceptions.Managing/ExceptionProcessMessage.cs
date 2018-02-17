using System.Windows.Forms;

namespace JP.Base.Errors.Managing
{
    public class ErrorProcessMessage
    {
        private MessageBoxButtons buttons;
        private MessageBoxDefaultButton defaultButton;
        private MessageBoxIcon icon;
        private string messageTitle;
        private ProcessValidator validator;

        public ErrorProcessMessage()
        {
            BuildClass(MessageBoxButtons.OK, MessageBoxDefaultButton.Button1, MessageBoxIcon.Asterisk, string.Empty, new ProcessValidator());
        }

        public ErrorProcessMessage(string messageTitle, ProcessValidator validator)
        {
            BuildClass(MessageBoxButtons.OK, MessageBoxDefaultButton.Button1, MessageBoxIcon.Asterisk, messageTitle, validator);
        }

        public ErrorProcessMessage(string messageTitle, ProcessValidator validator, MessageBoxButtons buttons)
        {
            BuildClass(buttons, MessageBoxDefaultButton.Button1, MessageBoxIcon.Asterisk, messageTitle, validator);
        }

        public ErrorProcessMessage(MessageBoxButtons msgButtons, MessageBoxDefaultButton defButton, MessageBoxIcon msgIcon, string msgTitle, ProcessValidator procValidator)
        {
            BuildClass(msgButtons, defButton, msgIcon, msgTitle, procValidator);
        }

        public MessageBoxButtons Buttons
        {
            get
            {
                return this.buttons;
            }
            set
            {
                this.buttons = value;
            }
        }

        public MessageBoxDefaultButton DefaultButton
        {
            get
            {
                return this.defaultButton;
            }
            set
            {
                this.defaultButton = value;
            }
        }

        public MessageBoxIcon Icon
        {
            get
            {
                return this.icon;
            }
            set
            {
                this.icon = value;
            }
        }

        public string MessageTitle
        {
            get
            {
                return this.messageTitle;
            }
            set
            {
                this.messageTitle = value;
                validator.IsValid = false;
            }
        }

        public ProcessValidator Validator
        {
            get { return validator; }
            set { validator = value; }
        }

        public void BuildClass(MessageBoxButtons msgButtons, MessageBoxDefaultButton defButton, MessageBoxIcon msgIcon, string msgTitle, ProcessValidator procValidator)
        {
            buttons = msgButtons;
            defaultButton = defButton;
            icon = msgIcon;
            messageTitle = msgTitle;
            validator = procValidator;
        }
    }
}