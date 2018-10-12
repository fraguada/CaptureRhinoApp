using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;
using Rhino.UI;

namespace CaptureRhinoApp
{
    public class CaptureRhinoAppCommand : Command
    {
        public CaptureRhinoAppCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static CaptureRhinoAppCommand Instance
        {
            get; private set;
        }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName
        {
            get { return "CaptureRhinoAppCommand"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // TODO: start here modifying the behaviour of your command.
            // ---

            var image = ScreenCapture.CaptureActiveWindow();

            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string datePath = Path.Combine(path, System.DateTime.Now.Year.ToString() + "-" + System.DateTime.Now.Month.ToString() + "-" + System.DateTime.Now.Day.ToString());

            if (!Directory.Exists(datePath))
            {
                Directory.CreateDirectory(datePath);
            }

            string imgName = System.DateTime.Now.Year.ToString() + "-" + System.DateTime.Now.Month.ToString() + "-" + System.DateTime.Now.Day.ToString() + "-" + System.DateTime.Now.Hour.ToString() + "-" + System.DateTime.Now.Minute.ToString() + "-" + System.DateTime.Now.Second.ToString();

            imgName += ".jpg";

            string pathImg = Path.Combine(datePath, imgName);

            image.Save(pathImg, ImageFormat.Jpeg);

            /*
            var ptr = RhinoApp.MainWindowHandle();
            
            Rectangle bounds = new Rectangle {
                X = RhinoEtoApp.MainWindow.Bounds.Left,
                Y = RhinoEtoApp.MainWindow.Bounds.Right,
                Width = RhinoEtoApp.MainWindow.Bounds.Width,
                Height = RhinoEtoApp.MainWindow.Bounds.Height,
                //Size = new Size(RhinoEtoApp.MainWindow.Size.Width, RhinoEtoApp.MainWindow.Size.Width)
            };// RhinoEtoApp.MainWindow.Bounds as Rectangle;

            

#if DEBUG
            RhinoApp.WriteLine("Location X: {0}", RhinoEtoApp.MainWindow.Location.X);
            RhinoApp.WriteLine("Bounds X: {0}", RhinoEtoApp.MainWindow.Bounds.X);
            RhinoApp.WriteLine("Bounds Left: {0}", RhinoEtoApp.MainWindow.Bounds.Left);

            RhinoApp.WriteLine("Location Y: {0}", RhinoEtoApp.MainWindow.Location.Y);
            RhinoApp.WriteLine("Bounds Y: {0}", RhinoEtoApp.MainWindow.Bounds.Y);
            RhinoApp.WriteLine("Bounds Top: {0}", RhinoEtoApp.MainWindow.Bounds.Top);

            RhinoApp.WriteLine("Width: {0}", RhinoEtoApp.MainWindow.Width);
            RhinoApp.WriteLine("Bounds Width: {0}", RhinoEtoApp.MainWindow.Bounds.Width);
            RhinoApp.WriteLine("Bounds Right: {0}", RhinoEtoApp.MainWindow.Bounds.Right);

            RhinoApp.WriteLine("Bounds Height: {0}", RhinoEtoApp.MainWindow.Bounds.Height);
            RhinoApp.WriteLine("Bounds Bottom: {0}", RhinoEtoApp.MainWindow.Bounds.Bottom);
#endif

            // check directory
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string datePath = Path.Combine(path, System.DateTime.Now.Year.ToString() + "-" + System.DateTime.Now.Month.ToString() + "-" + System.DateTime.Now.Day.ToString());

            if(!Directory.Exists(datePath))
            {
                Directory.CreateDirectory(datePath);
            }

            string pathImg = Path.Combine(datePath, "testImg.jpg");

            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    //g.CopyFromScreen(bounds.Left, bounds.Top, bounds.Right, bounds.Bottom, bounds.Size );
                    g.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top), System.Drawing.Point.Empty, bounds.Size);
                }
                bitmap.Save(pathImg, ImageFormat.Jpeg);
            }

            // ---

    */

            return Result.Success;
        }
    }
}
