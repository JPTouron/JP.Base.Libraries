namespace JP.Base.Exceptions.Winforms
{
    public class ProcessValidator : IProcessValidator
    {
        private object errorId;
        private bool isValid;
        private string message;

        public ProcessValidator()
        {
            BuildClass(true, string.Empty, null);
        }

        public ProcessValidator(bool isProcessValid, string errorMessage)
        {
            BuildClass(isProcessValid, errorMessage, null);
        }

        public ProcessValidator(bool isProcessValid, string errorMessage, object errorId)
        {
            BuildClass(isProcessValid, errorMessage, errorId);
        }

        public object ErrorId
        {
            get
            {
                return this.errorId;
            }
            set
            {
                this.errorId = value;
                this.isValid = false;
            }
        }

        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            internal set
            {
                isValid = value;
            }
        }

        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
                this.isValid = false;
            }
        }

        private void BuildClass(bool isProcessValid, string errorMessage, object processErrorId)
        {
            isValid = isProcessValid;
            message = errorMessage;
            errorId = processErrorId;
        }
    }
}