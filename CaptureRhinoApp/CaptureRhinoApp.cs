using System;
using System.Runtime.InteropServices;
using System.Drawing;
using Rhino;

namespace CaptureRhinoApp
{
    /// <summary>
    /// From https://stackoverflow.com/a/24879511/2179399
    /// </summary>
    public class ScreenCapture
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        public static Image CaptureDesktop()
        {
            return CaptureWindow(GetDesktopWindow());
        }

        public static Bitmap CaptureActiveWindow()
        {
            return CaptureWindow(GetForegroundWindow());
        }

        public static Bitmap CaptureRhinoWindow()
        {
            // This still seems to capture whatever is in the foreground
            // Needed to add a IntPtr.Zero check for times when Rhino is opening a new file or doing something 
            // where it can't return the MainWindow.
            if (RhinoApp.MainWindowHandle() != IntPtr.Zero)
                return CaptureWindow(RhinoApp.MainWindowHandle());
            else
                return null;
        }

        public static Bitmap CaptureWindow(IntPtr handle)
        {
            var rect = new Rect();
            GetWindowRect(handle, ref rect);
            var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);

            if (bounds.Width > 0 && bounds.Height > 0)
            {
                var result = new Bitmap(bounds.Width, bounds.Height);

                using (var graphics = Graphics.FromImage(result))
                {
                    graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }

                return result;

            }

            return null;
        }
    }
}
