using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using WeifenLuo.WinFormsUI.Docking;
using QUAVS.Base;
using FC.GEPluginCtrls;
using FC.GEPluginCtrls.HttpServer;

namespace QUAVS.GS.Forms
{
    struct DragInfo
    {
        public dynamic Placemark;

        public bool Dragging;

        public DragInfo(dynamic placemark, bool dragged)
        {
            Placemark = placemark;
            Dragging = dragged;
        }
    }

    public partial class GEForm : DockContent
    {
        // The server waits for connetions on the localhost (loopback) address of 127.0.0.1
        // The listener works on port 8080
        // The server looks for the file "defualt.kml" if none is specified
        // All of these things can be altered either via the constructor or properties
        private Server server = new Server(Application.StartupPath + "\\webroot\\");

        // the plug-in instance (IGEPluginCoClass)
        private dynamic ge = null;

        // for the dragable placemark
        private DragInfo dragInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="GEForm"/> class.
        /// </summary>
        public GEForm()
        {
            InitializeComponent();

            // start the local server
            server.Start();

            // load the embeded plug-in
            geWebBrowser1.LoadEmbededPlugin();

            // set various event handlers 
            geWebBrowser1.PluginReady += new GEWebBrowserEventHandler(geWebBrowser1_PluginReady);
            geWebBrowser1.KmlLoaded += new GEWebBrowserEventHandler(geWebBrowser1_KmlLoaded);
            geWebBrowser1.ScriptError += new GEWebBrowserEventHandler(geWebBrowser1_ScriptError);
            geWebBrowser1.KmlEvent += new GEWebBrowserEventHandler(geWebBrowser1_KmlEvent);
            geWebBrowser1.ViewEvent += new GEWebBrowserEventHandler(geWebBrowser1_ViewEvent);

            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.Stop();
        }

        void geWebBrowser1_PluginReady(object sender, GEEventArgs e)
        {
            GEWebBrowser browser = sender as GEWebBrowser;
            ge = browser.GetPlugin();
            // with the default setting the same as loading http://localhost:8080/defualt.kml
            geWebBrowser1.FetchKml("http://localhost:8080/");
        }

        void geWebBrowser1_ViewEvent(object sender, GEEventArgs e)
        {
            ////Debug.WriteLine(string.Format("GEView: {0}", e.Message), "Form1");
        }

        void geWebBrowser1_KmlEvent(object sender, GEEventArgs e)
        {
            dynamic mouseEvent = sender;

            //process action cases...
            switch (e.Data)
            {
                case "click":
                    break;
                case "dblclick":
                    break;
                case "mouseover":
                    break;
                case "mousedown":
                    if (mouseEvent.getTarget().getType() == "KmlPlacemark" &&
                        !dragInfo.Dragging)
                    {
                        // test for placemark by id...  
                        // if this is removed then all placemarks would be dragable
                        if (mouseEvent.getTarget().getId() != "dpm") { return; }

                        dragInfo.Placemark = mouseEvent.getTarget();
                        dragInfo.Dragging = true;
                        Debug.WriteLine("Pick-up", "Form1");
                    }

                    break;
                case "mouseup":
                    if (dragInfo.Dragging)
                    {
                        mouseEvent.preventDefault();
                        Debug.WriteLine("Drop", "Form1");
                    }

                    dragInfo.Dragging = false;
                    break;
                case "mouseout":
                    break;
                case "mousemove":
                    if (dragInfo.Dragging)
                    {
                        mouseEvent.preventDefault();
                        dynamic point = dragInfo.Placemark.getGeometry();
                        point.setLatitude(mouseEvent.getLatitude());
                        point.setLongitude(mouseEvent.getLongitude());
                    }

                    break;
                default:
                    break;
            }
        }

        void geWebBrowser1_KmlLoaded(object sender, GEEventArgs e)
        {
            dynamic kml = sender;

            if (null != kml)
            {
                // add any kml to the plug-in and tree
                ge.getFeatures().appendChild(kml);
                kmlTreeView1.ParseKmlObject(kml);
            }
        }

        void geWebBrowser1_ScriptError(object sender, GEEventArgs e)
        {
            MessageBox.Show(e.Data, e.Message);
        }
    }
}
