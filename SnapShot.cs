using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoRunSnapShot
{

    class SnapShot
    {
        public string Url { get; set; }
        public string FileName { get; set; }
        public string Dir { get; set; }
       

        internal void Save()
        {

            StartBrowser(Url);


        }

        private void StartBrowser(string url)
        {
            var th = new Thread(() =>
             {

                 var webBrowser = new WebBrowser();
                 webBrowser.ScrollBarsEnabled = false;
                 webBrowser.DocumentCompleted +=
                     webBrowser_DocumentCompleted;
                 webBrowser.Width = 960;
                 webBrowser.Height = 2500;
                 webBrowser.ScriptErrorsSuppressed = true;
                 webBrowser.Url = new Uri(Url);
                 Application.Run();
                 Console.WriteLine("開始執行");
             });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
           
        }

        private void  webBrowser_DocumentCompleted(object sender,WebBrowserDocumentCompletedEventArgs e)
        {
            var webBrowser = (WebBrowser)sender;
            webBrowser.Width = (webBrowser.Document.Body.ScrollRectangle.Size.Width == 0) ? webBrowser.Width : webBrowser.Document.Body.ScrollRectangle.Size.Width;
            webBrowser.Height = (webBrowser.Document.Body.ScrollRectangle.Size.Height == 0) ? webBrowser.Height : webBrowser.Document.Body.ScrollRectangle.Size.Height;

            using (Bitmap bitmap = new Bitmap(
                    webBrowser.Width,
                    webBrowser.Height))
            {
                webBrowser
                    .DrawToBitmap(
                    bitmap,
                    new System.Drawing
                        .Rectangle(0, 0, bitmap.Width, bitmap.Height));
                bitmap.Save(Dir + @"\" + FileName,
                    System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
    }
}
