using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using JP.Base.Errors.Logging.Support;

namespace JP.Base.Errors.Logging
{
    internal class ScreenShotManager
    {
        private string screenshotFileName = "ErrorScreen";

        private string screenshotFullPath = string.Empty;

        private ImageFormat screenshotImageFormat = ImageFormat.Png;

        private string ticks;

        public ScreenShotManager()
        {
            screenshotFileName = ConfigReader.ScreenshotFileName;
        }

        public string ScreenshotFileName
        {
            get { return screenshotFileName; }
            set { screenshotFileName = value; }
        }

        public string ScreenshotFullName
        {
            get { return screenshotFullPath; }
            set { screenshotFullPath = value; }
        }

        public string Ticks { get { return ticks; } }

        public bool TakeScreenshot()
        {
            bool screenShotTaken = false;

            // note that screenshotname does not include the file type extension
            try
            {
                Rectangle screenRect = Screen.PrimaryScreen.Bounds;
                Bitmap screenBitmap = new Bitmap(screenRect.Width, screenRect.Height);
                Graphics screenGraphics = Graphics.FromImage(screenBitmap);

                // get a device context to the windows desktop and our destination  bitmaps
                int hdcSrc = GetDC(0);
                IntPtr hdcDest = screenGraphics.GetHdc();

                // copy what is on the desktop to the bitmap
                const int SRCCOPY = 0xCC0020;
                BitBlt(hdcDest.ToInt32(), 0, 0, screenRect.Width, screenRect.Height, hdcSrc, 0, 0, SRCCOPY);
                // release device contexts
                screenGraphics.ReleaseHdc(hdcDest);
                ReleaseDC(0, hdcSrc);

                string extension = "." + screenshotImageFormat.ToString().ToLower();

                // Find a name that isn't used (decorated by time)
                do
                {
                    ticks = string.Format("[{0}]", DateTime.Now.Ticks.ToString());

                    screenshotFullPath = Path.Combine(AppContext.ApplicationPath, string.Format("{0}[{1}]", screenshotFileName, ticks));

                    if (Path.GetExtension(screenshotFullPath) != extension)
                        screenshotFullPath += extension;
                } while (File.Exists(screenshotFullPath));

                screenBitmap.Save(screenshotFullPath, screenshotImageFormat);

                screenShotTaken = true;
            }
            catch (Exception ex)
            {
                screenshotFullPath = "* There was an error while capturing the screen shot: " + ex.Message;
            }

            return screenShotTaken;
        }

        // Windows API calls necessary to support screen capture
        [DllImport("gdi32.dll")]
        private static extern int BitBlt(int hDestDC, int x, int y, int nWidth, int nHeight, int hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport("user32.dll")]
        private static extern int GetDC(int hwnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(int hwnd, int hdc);
    }
}