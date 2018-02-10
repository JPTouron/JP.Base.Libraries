using System;
using System.Threading;
using System.Windows.Forms;
using JP.Base.Common.Errors;
using JP.Base.Errors.Logging;
using JP.Base.Errors.Managing.Support;

namespace JP.Base.Errors.Managing
{
    public class UnHandledExcListener
    {
        private static bool enableErrorEmailSending = true;
        private static bool killApp = false;
        private static bool showErrorForm = false;

        public static event EventHandler UnhandledExceptionOcurred;

        public static void StartListening(bool shouldKillApp, bool showErrForm, bool enableEmailSending)
        {
            killApp = shouldKillApp;
            showErrorForm = showErrForm;
            enableErrorEmailSending = enableEmailSending;
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnHandledExcListener.UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
        }

        internal static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandleUnHandledException(e.Exception);
        }

        internal static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleUnHandledException((Exception)e.ExceptionObject);
        }

        private static void HandleUnHandledException(Exception ex)
        {
            if (UnhandledExceptionOcurred != null)
            {
                UnhandledExceptionOcurred(ex, new EventArgs());
            }
            ExceptionData ErrObj = new ExceptionData(ex, true);

            var logger = new ErrorLogger(ErrObj);

            logger.TakeScreenShot = true;
            logger.SaveMiniDump = true;
            logger.LogException();

            if (showErrorForm)
            {
                new ErrorReporter(ErrObj, killApp, enableErrorEmailSending, logger.SavedLogFileName).ShowDialog();
            }
            else
            {
                MessageBox.Show(ex.GetMessageForError(), "Ocurrió un error inesperado de sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}