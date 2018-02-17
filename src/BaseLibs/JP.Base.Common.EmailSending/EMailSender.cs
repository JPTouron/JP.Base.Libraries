using System.Collections.Generic;
using System.Net.Mail;
using JP.Base.Common.Extensions.Lists;

namespace JP.Base.Common.EmailSending
{
    public class EMailSender
    {
        private MailMessage message;

        public EMailSender()
        {
            BuildClass(string.Empty, new List<string>(), string.Empty, string.Empty, false);
        }

        public EMailSender(string fromAddress, string toAddress)
        {
            BuildClass(fromAddress, new List<string>() { toAddress }, string.Empty, string.Empty, false);
        }

        public EMailSender(string fromAddress, IList<string> toAddresses, string subject, string body, bool isHtml)
        {
            BuildClass(fromAddress, toAddresses, subject, body, isHtml);
        }

        public IList<string> BCCAddresses { get; set; }
        public string BodyContent { get; set; }
        public IList<string> CCAddresses { get; set; }
        public string FromAddress { get; set; }
        public bool IsBodyHTML { get; set; }
        public string Subject { get; set; }
        public IList<string> ToAddresses { get; set; }

        public MailMessage GetMessage()
        {
            if (message == null)
                CreateMailMessage();
            return message;
        }

        public bool Send()
        {
            bool result = false;
            SmtpClient smtpClient = new SmtpClient();

                if (CreateMailMessage())
                {
                    smtpClient.Send(message);
                    result = true;
                }

            return result;
        }

        private void BuildClass(string fromAddress, IList<string> toAddresses, string subject, string body, bool isHtml)
        {
            FromAddress = fromAddress;
            ToAddresses = toAddresses;
            Subject = subject;
            BodyContent = body;
            IsBodyHTML = isHtml;
            CCAddresses = new List<string>();
            BCCAddresses = new List<string>();
        }

        private bool CreateMailMessage()
        {
            bool result = false;

            message = new MailMessage();

                MailAddress fromAddress = new MailAddress(FromAddress);

                message.From = fromAddress;
                message.Subject = Subject;
                message.IsBodyHtml = IsBodyHTML;
                message.Body = BodyContent;

                if (ToAddresses.Count > 0)
                    message.To.Add(ToAddresses.ToSeparatedValues<string>(","));

                if (CCAddresses.Count > 0)
                    message.CC.Add(CCAddresses.ToSeparatedValues<string>(","));

                if (BCCAddresses.Count > 0)
                    message.Bcc.Add(BCCAddresses.ToSeparatedValues<string>(","));

                result = true;

            return result;
        }
    }
}