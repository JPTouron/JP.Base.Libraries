using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JP.Base.Common.EmailSending;
using JP.Base.Common.Exceptions.Parsing;
using JP.Base.Common.Extensions.Strings;
using JP.Base.Errors.Logging;
using JP.Base.Errors.Managing.Support;

namespace JP.Base.Errors.Managing
{
    internal partial class ErrorReporter : Form
    {
        internal Button cmdClose;
        internal Button cmdSendEmail;
        internal ToolStripStatusLabel lblStatus;
        private const string DISPLAY_MSG_FOR_COPY_TO_CLIPBOARD = "La información del error se ha copiado en el portapapeles";
        private const string DISPLAY_MSG_FOR_KILL_APP = "La aplicación se cerrará al cerrar esta ventana";
        private const string ERROR_LOCATION_MSG = "[Ubicación de errores]";
        private const string FILE_LABEL_MSG = "Archivo:";
        private const string LINE_LABEL_MSG = "Línea:";
        private const string LOG_END_MSG = "Fin de reporte de error";
        private const string LOG_START_MSG = "Inicio de reporte de error";
        private const string MAIL_NOT_SENT_CONFIRMATION_MSG = "El email no pudo ser enviado";
        private const string MAIL_SENT_CONFIRMATION_MSG = "El email se envió correctamente";
        private const string METHOD_LABEL_MSG = "Método:";
        private Button cmdCopyClipboard;
        private ExceptionData currentError;
        private Exception emailSendingError;
        private bool enableEmailReporting;
        private GroupBox groupBox1;
        private bool killApp;
        private ToolStripStatusLabel lblInformation;
        private string savedLogFileName;
        private int windowHeight = 600;
        private int windowWidth = 600;

        public ErrorReporter(ExceptionData ErrObject, bool shouldKillApp, bool enableEmailSending, string savedLog)
        {
            this.InitializeComponent();
            base.Size = new Size(this.windowWidth, this.windowHeight);
            this.currentError = ErrObject;
            this.killApp = shouldKillApp;
            this.enableEmailReporting = enableEmailSending;
            savedLogFileName = savedLog;
        }

        private void btnAdditionalInfo_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.richtextMessage.Text.Replace("\n", Environment.NewLine));
            this.lblInformation.Text = DISPLAY_MSG_FOR_COPY_TO_CLIPBOARD.ToCurrentCulture();
        }

        private void btnBug_Click(object sender, EventArgs e)
        {
            bool mailSent = false;
            try
            {
                if (!(string.IsNullOrEmpty(ConfigReader.FromAddress) || string.IsNullOrEmpty(ConfigReader.ToAddress)))
                {
                    EMailSender mailSender = new EMailSender();
                    mailSender.FromAddress = ConfigReader.FromAddress;
                    List<string> toAddresses = new List<string>() { ConfigReader.ToAddress };
                    mailSender.ToAddresses = toAddresses;
                    mailSender.IsBodyHTML = false;
                    mailSender.Subject = this.GetMailErrorSubject();
                    mailSender.BodyContent = this.GetMailErrorBody();
                    mailSent = mailSender.Send();
                }
                if (mailSent)
                    lblInformation.Text = MAIL_SENT_CONFIRMATION_MSG.ToCurrentCulture();
                else
                    lblInformation.Text = MAIL_NOT_SENT_CONFIRMATION_MSG.ToCurrentCulture();
            }
            catch (Exception ex)
            {
                lblInformation.Text = string.Format("{0}-{1}", MAIL_NOT_SENT_CONFIRMATION_MSG.ToCurrentCulture(), ex.Message);
                new ErrorLogger(new ExceptionData(ex)).LogException();
                emailSendingError = ex;
                lblInformation.IsLink = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void ErrorReporter_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.killApp)
            {
                Application.ExitThread();
                Application.Exit();
            }
        }

        private void ErrorReporter_Load(object sender, EventArgs e)
        {
            this.lblStatus.Text = string.Empty;
            this.lblInformation.Text = string.Empty;
            this.Text = this.Text + " " + this.currentError.CurrenException.GetType().ToString();
            StringBuilder errorInformation = new StringBuilder();
            errorInformation.Append(this.currentError.ExceptionText);
            errorInformation.Append(this.currentError.EnvironmentInfo);
            errorInformation.Append(this.currentError.AssemblyInfo);
            errorInformation.AppendLine(ERROR_LOCATION_MSG);
            foreach (ExceptionLocationData Locations in this.currentError.ErrorLocations)
            {
                errorInformation.AppendLine(string.Format("{0} {1}", FILE_LABEL_MSG, Locations.FileName));
                errorInformation.AppendLine(string.Format("{0} {1}", METHOD_LABEL_MSG, Locations.Method));
                errorInformation.AppendLine(string.Format("{0} {1}", LINE_LABEL_MSG, Locations.Line));
            }
            errorInformation.Append(this.currentError.StackTrace);

            if (!string.IsNullOrEmpty(savedLogFileName))
            {
                var savedLogInformation = new StringBuilder();
                savedLogInformation.AppendLine("Log File Saved At: ");
                savedLogInformation.AppendLine(savedLogFileName);
                savedLogInformation.AppendLine(string.Empty);
                richtextMessage.Text = savedLogInformation.ToString();
            }

            this.richtextMessage.Text += errorInformation.ToString().ToCurrentCulture();

            if (this.killApp)
            {
                this.lblStatus.Text = DISPLAY_MSG_FOR_KILL_APP.ToCurrentCulture();
            }
            this.cmdSendEmail.Visible = this.enableEmailReporting;
        }

        private string GetMailErrorBody()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(ConfigReader.BodyPreface);
            sb.AppendLine(new string('_', 30));
            sb.AppendLine("Inicio de reporte de error");
            sb.AppendLine(new string('_', 30));
            sb.AppendLine(string.Empty);
            sb.AppendLine(this.richtextMessage.Text);
            sb.AppendLine(new string('_', 30));
            sb.AppendLine("Fin de reporte de error");
            sb.AppendLine(new string('_', 30));
            return sb.ToString().ToCurrentCulture();
        }

        private string GetMailErrorSubject()
        {
            return ConfigReader.SubjectPreface;
        }

        private void lblInformation_Click(object sender, EventArgs e)
        {
            if (this.emailSendingError != null)
            {
                ExceptionData errObj = new ExceptionData(this.emailSendingError);
                new ErrorReporter(errObj, false, false, string.Empty).Show();
            }
        }
    }
}