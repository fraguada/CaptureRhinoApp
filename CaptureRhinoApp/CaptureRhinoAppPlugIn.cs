using Rhino.PlugIns;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace CaptureRhinoApp
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class CaptureRhinoAppPlugIn : Rhino.PlugIns.PlugIn

    {
        public CaptureRhinoAppPlugIn()
        {
            Instance = this;
        }

        ///<summary>Gets the only instance of the CaptureRhinoAppPlugIn plug-in.</summary>
        public static CaptureRhinoAppPlugIn Instance
        {
            get; private set;
        }

        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and maintain plug-in wide options in a document.

        public override Rhino.PlugIns.PlugInLoadTime LoadTime => Rhino.PlugIns.PlugInLoadTime.AtStartup;

        /// <summary>
        /// Need to store timer somewhere, else it be collected by GC.
        /// </summary>
        Timer Timer { get; set; }

        protected override LoadReturnCode OnLoad(ref string errorMessage)
        {
            
            TimerCallback timerCallback = OnTimer;
            Timer = new Timer(timerCallback, "test", 1000, 1000);
           
            return base.OnLoad(ref errorMessage);
        }

        static void OnTimer(object obj)
        {

            var image = ScreenCapture.CaptureRhinoWindow();

            if (image != null)
            {

                string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                string datePath = Path.Combine(path, System.DateTime.Now.Year.ToString() + "-" + System.DateTime.Now.Month.ToString() + "-" + System.DateTime.Now.Day.ToString());

                if (!Directory.Exists(datePath))
                {
                    Directory.CreateDirectory(datePath);
                }

                string imgName =    System.DateTime.Now.Year.ToString();
                imgName += "-";
                imgName +=          System.DateTime.Now.Month.ToString();
                imgName += "-";
                imgName +=          System.DateTime.Now.Day.ToString();
                imgName += "-";
                imgName +=          System.DateTime.Now.Hour.ToString();
                imgName += "-";
                imgName +=          System.DateTime.Now.Minute.ToString();
                imgName += "-";
                imgName +=          System.DateTime.Now.Second.ToString();

                imgName += ".jpg";

                string pathImg = Path.Combine(datePath, imgName);

                image.Save(pathImg, ImageFormat.Jpeg);
            }

        }
    }
}