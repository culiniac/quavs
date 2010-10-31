using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.IO;

using WeifenLuo.WinFormsUI.Docking;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;

namespace QUAVS.GroundStation
{
    public partial class MapForm : DockContent
    {
        // marker
        GMapMarker currentMarker;
        GMapMarker center;

        // polygons
        GMapPolygon polygon;

        // layers
        GMapOverlay top;
        internal GMapOverlay objects;
        internal GMapOverlay routes;
        internal GMapOverlay polygons;

        bool isMouseDown = false;
        PointLatLng start;
        PointLatLng end;

        public MapForm()
        {
            InitializeComponent();
            //MainMap.CacheLocation = "." + Path.DirectorySeparatorChar;
            // set cache mode only if no internet avaible
            if (!DesignMode)
            {
                // set cache mode only if no internet avaible
                try
                {
                    System.Net.IPHostEntry e = System.Net.Dns.GetHostEntry("www.google.com");
                }
                catch
                {
                    MainMap.Manager.Mode = AccessMode.CacheOnly;
                    MessageBox.Show("No internet connection avaible, going to CacheOnly mode.", "GMap.NET - Demo.WindowsForms", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                
                // map events
                MainMap.OnCurrentPositionChanged += new CurrentPositionChanged(MainMap_OnCurrentPositionChanged);
                MainMap.OnTileLoadStart += new TileLoadStart(MainMap_OnTileLoadStart);
                MainMap.OnTileLoadComplete += new TileLoadComplete(MainMap_OnTileLoadComplete);
                MainMap.OnMarkerClick += new MarkerClick(MainMap_OnMarkerClick);
                MainMap.OnMapZoomChanged += new MapZoomChanged(MainMap_OnMapZoomChanged);
                MainMap.OnMapTypeChanged += new MapTypeChanged(MainMap_OnMapTypeChanged);
                MainMap.MouseMove += new MouseEventHandler(MainMap_MouseMove);
                MainMap.MouseDown += new MouseEventHandler(MainMap_MouseDown);
                MainMap.MouseUp += new MouseEventHandler(MainMap_MouseUp);
                //MainMap.OnMarkerEnter += new MarkerEnter(MainMap_OnMarkerEnter);
                //MainMap.OnMarkerLeave += new MarkerLeave(MainMap_OnMarkerLeave);

                // get map type
                comboBoxProviderType.DataSource = Enum.GetValues(typeof(MapType));
                comboBoxProviderType.SelectedItem = MainMap.MapType;

                // acccess mode
                comboBoxProviderMode.DataSource = Enum.GetValues(typeof(AccessMode));
                comboBoxProviderMode.SelectedItem = GMaps.Instance.Mode;

                // config map             
                MainMap.Zoom = MainMap.MinZoom + 6;
                MainMap.CurrentPosition = new PointLatLng(43.8528890087738, -79.457995891571);
                trackBarMapZoom.Value = (int) MainMap.Zoom;
                textBoxLat.Text = MainMap.CurrentPosition.Lat.ToString(CultureInfo.InvariantCulture);
                textBoxLng.Text = MainMap.CurrentPosition.Lng.ToString(CultureInfo.InvariantCulture);
                
                // add custom layers  
                {
                    routes = new GMapOverlay(MainMap, "routes");
                    MainMap.Overlays.Add(routes);

                    polygons = new GMapOverlay(MainMap, "polygons");
                    MainMap.Overlays.Add(polygons);

                    objects = new GMapOverlay(MainMap, "objects");
                    MainMap.Overlays.Add(objects);

                    top = new GMapOverlay(MainMap, "top");
                    MainMap.Overlays.Add(top);

                    //routes.Routes.CollectionChanged += new GMap.NET.ObjectModel.NotifyCollectionChangedEventHandler(Routes_CollectionChanged);
                    //objects.Markers.CollectionChanged += new GMap.NET.ObjectModel.NotifyCollectionChangedEventHandler(Markers_CollectionChanged);
                }

                // set current marker
                currentMarker = new GMapMarkerGoogleRed(MainMap.CurrentPosition);
                (currentMarker as GMapMarkerGoogleRed).Bearing = 127;

                top.Markers.Add(currentMarker);

                // map center
                center = new GMapMarkerCross(MainMap.CurrentPosition);
                top.Markers.Add(center);

                splitContainer1.Panel2Collapsed = true;
            }
        }

        private void trackBarMapZoom_Scroll(object sender, EventArgs e)
        {
            MainMap.Zoom = trackBarMapZoom.Value;
        }

        #region -- map events --
        //void MainMap_OnMarkerLeave(GMapMarker item)
        //{
        //    if (item is GMapMarkerRect)
        //    {
        //        CurentRectMarker = null;

        //        GMapMarkerRect rc = item as GMapMarkerRect;
        //        rc.Pen.Color = Color.Blue;
        //        MainMap.Invalidate(false);
        //    }
        //}

        //void MainMap_OnMarkerEnter(GMapMarker item)
        //{
        //    if (item is GMapMarkerRect)
        //    {
        //        GMapMarkerRect rc = item as GMapMarkerRect;
        //        rc.Pen.Color = Color.Red;
        //        MainMap.Invalidate(false);

        //        CurentRectMarker = rc;
        //    }
        //}

        void MainMap_OnMapTypeChanged(MapType type)
        {
            //comboBoxMapType.SelectedItem = MainMap.MapType;

            trackBarMapZoom.Minimum = MainMap.MinZoom;
            trackBarMapZoom.Maximum = MainMap.MaxZoom;

            if (routes.Routes.Count > 0)
            {
                MainMap.ZoomAndCenterRoutes(null);
            }
        }

        void MainMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        void MainMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;

                if (currentMarker.IsVisible)
                {
                    currentMarker.Position = MainMap.FromLocalToLatLng(e.X, e.Y);
                }

                //Placemark pos = GMaps.Instance.GetPlacemarkFromGeocoder(currentMarker.Position);
                //if (pos != null)
                //{
                //    currentMarker.ToolTipText = pos.Address;
                //    MainMap.Invalidate(false);
                //}
            }
        }

        // move current marker with left holding
        void MainMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isMouseDown)
            {
                //if (CurentRectMarker == null)
                //{
                //    if (currentMarker.IsVisible)
                //    {
                //        currentMarker.Position = MainMap.FromLocalToLatLng(e.X, e.Y);
                //    }
                //}
                //else // move rect marker
                //{
                //    PointLatLng pnew = MainMap.FromLocalToLatLng(e.X, e.Y);

                //    int? pIndex = (int?)CurentRectMarker.Tag;
                //    if (pIndex.HasValue)
                //    {
                //        if (pIndex < polygon.Points.Count)
                //        {
                //            polygon.Points[pIndex.Value] = pnew;
                //            MainMap.UpdatePolygonLocalPosition(polygon);
                //        }
                //    }

                //    if (currentMarker.IsVisible)
                //    {
                //        currentMarker.Position = pnew;
                //    }

                //}
            }
        }

        // MapZoomChanged
        void MainMap_OnMapZoomChanged()
        {
            trackBarMapZoom.Value = (int)(MainMap.Zoom);
            center.Position = MainMap.CurrentPosition;
        }

        // click on some marker
        void MainMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (item is GMapMarker)
                {
                    Placemark pos = GMaps.Instance.GetPlacemarkFromGeocoder(item.Position);
                    if (pos != null)
                    {
                        GMapMarker v = item as GMapMarker;
                        {
                            v.ToolTipText = pos.Address;
                        }
                        MainMap.Invalidate(false);
                    }
                }
            }
        }

        // loader start loading tiles
        void MainMap_OnTileLoadStart()
        {
            MethodInvoker m = delegate()
            {
                //panelMenu.Text = "Menu: loading tiles...";
            };
            try
            {
                BeginInvoke(m);
            }
            catch
            {
            }
        }

        // loader end loading tiles
        void MainMap_OnTileLoadComplete(long ElapsedMilliseconds)
        {
            //MainMap.ElapsedMilliseconds = ElapsedMilliseconds;

            //MethodInvoker m = delegate()
            //{
            //    panelMenu.Text = "Menu, last load in " + MainMap.ElapsedMilliseconds + "ms";

            //    textBoxMemory.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.00}MB of {1:0.00}MB", MainMap.Manager.MemoryCacheSize, MainMap.Manager.MemoryCacheCapacity);
            //};
            //try
            //{
            //    BeginInvoke(m);
            //}
            //catch
            //{
            //}
        }

        // current point changed
        void MainMap_OnCurrentPositionChanged(PointLatLng point)
        {
            center.Position = point;
            textBoxLat.Text = point.Lat.ToString(CultureInfo.InvariantCulture);
            textBoxLng.Text = point.Lng.ToString(CultureInfo.InvariantCulture);
        }

        // center markers on start
        private void MainForm_Load(object sender, EventArgs e)
        {
            MainMap.ZoomAndCenterMarkers(null);
            trackBarMapZoom.Value = (int)MainMap.Zoom;
        }

        // ensure focus on map, trackbar can have it too
        private void MainMap_MouseEnter(object sender, EventArgs e)
        {
            MainMap.Focus();
        }
        #endregion

        private void comboBoxProviderType_DropDownClosed(object sender, EventArgs e)
        {
            MainMap.MapType = (MapType)comboBoxProviderType.SelectedValue;
        }

        private void comboBoxProviderMode_DropDownClosed(object sender, EventArgs e)
        {
            GMaps.Instance.Mode = (AccessMode)comboBoxProviderMode.SelectedValue;
            MainMap.ReloadMap();
        }
    }
}
