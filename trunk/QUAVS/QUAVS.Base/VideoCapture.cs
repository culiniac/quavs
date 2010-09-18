﻿/****************************************************************************
QuadUAV Video Capture Class
*****************************************************************************/

// type of bitmap to use
#define ARGB32

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using DirectShowLib;

namespace QUAVS.Base
{
    /// <summary> 
    /// VideoCapture class 
    /// </summary> 
    /// <remarks> 
    /// pre-defined graph for previewing and capturing
    /// </remarks>
    /// <example>
    /// VideoCapture cam = null;
    /// cam = new VideoCapture(strVideoSource, strVideoCompressor, "test.avi", 30, 640, 480, panel1.Handle); 
    /// cam.Start();
    /// </example>
 
    internal class VideoCapture : ISampleGrabberCB, IDisposable
    {

#region Member variables

        IGraphBuilder _graphBuilder = null;
        IMediaControl _mediaCtrl = null;
        
        private bool _bRunning = false;

        private string _strCapture;

        public string CaptureDevice
        {
            get { return _strCapture; }
            set { _strCapture = value; }
        }

        private string _strCompressor;

        public string CompressorCodec
        {
            get { return _strCompressor; }
            set { _strCompressor = value; }
        }

        private string _strFileName;

        public string VideoFile
        {
            get { return _strFileName; }
            set { _strFileName = value; }
        }
        
        private IntPtr _hOwner;

        public IntPtr Owner
        {
            get { return _hOwner; }
            set { _hOwner = value; }
        }

        private TelemetryDataObject _dataObject;

        public TelemetryDataObject DataObject
        {
            get { return _dataObject; }
            set { _dataObject = value; }
        }

        private int _videoWidth;
        public int VideoWidth
        {
            get { return _videoWidth; }
            set { _videoWidth = value; }
        }
        
        private int _videoHeight;
        public int VideoHeight
        {
            get { return _videoHeight; }
            set { _videoHeight = value; }
        }
        
        private int _stride;
        
        private int _fps;
        public int Fps
        {
            get { return _fps; }
            set { _fps = value; }
        }

        private HUD _hud;
        internal HUD HUD
        {
            get { return _hud; }
        }

#if DEBUG
        // Allow you to "Connect to remote graph" from GraphEdit
        DsROTEntry _rot = null;
#endif

#endregion

#region API

        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
        private static extern void CopyMemory(IntPtr Destination, IntPtr Source, [MarshalAs(UnmanagedType.U4)] uint Length);

#endregion
        
        public VideoCapture(IntPtr owner, TelemetryDataObject dataObject)
        {
            _hOwner = owner;
            _dataObject = dataObject;
        }

        public VideoCapture()
        {
            _strCapture = "";
            _strCompressor = "";
            _strFileName = "";
            _fps = 0;
            _videoWidth = 0;
            _videoHeight = 0;
            _hOwner = IntPtr.Zero;
            _dataObject = null;
        }

        public void InitializeCapture()
        {
            try
            {
                // Set up the capture graph
                _hud = new HUD(_videoWidth, _videoHeight, _dataObject);
                SetupGraph(_strCapture, _strCompressor, _strFileName, _fps, _videoWidth, _videoHeight, _hOwner);
            }
            catch(Exception e)
            {
                Trace.WriteLine(e);
                Dispose();
            }
        }

        /// <summary> release everything. </summary>
        public void Dispose()
        {
            CloseInterfaces();
        }
        
        // Destructor
        ~VideoCapture()
        {
            CloseInterfaces();
        }
        
        /// <summary> capture the next image </summary>
        public void Start()
        {
            if (!_bRunning)
            {
                int hr = _mediaCtrl.Run();
                checkHR(hr, "Cannot start capture graph");

                _bRunning = true;
            }
        }
        
        // Pause the capture graph.
        // Running the graph takes up a lot of resources.  Pause it when it
        // isn't needed.
        public void Pause()
        {
            if (_bRunning)
            {
                int hr = _mediaCtrl.Pause();
                checkHR(hr, "Cannot pause capture graph");

                _bRunning = false;
            }
        }

        public bool IsRunning()
        {
            return _bRunning;
        }

        /// <summary> build the capture graph for grabber. </summary>
        private void SetupGraph(string strCapture, string strCompressor, string strFileName, int iFrameRate, int iWidth, int iHeight, IntPtr owner)
        {
            ICaptureGraphBuilder2 captureGraphBuilder = null;
            ISampleGrabber sampGrabber = null;
            IBaseFilter theIPinTee = null;
            IBaseFilter mux = null;
            IFileSinkFilter sink = null;
            IBaseFilter captureDevice = null;
            IBaseFilter captureCompressor = null;

            try
            {
                
                //Create the filter for the selected video input
                captureDevice = CreateFilter(FilterCategory.VideoInputDevice, strCapture);

                //Create the filter for the selected video compressor
                captureCompressor = CreateFilter(FilterCategory.VideoCompressorCategory, strCompressor);

                //Create the Graph
                _graphBuilder = (IGraphBuilder)new FilterGraph();

                //Create the Capture Graph Builder

                captureGraphBuilder = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();

                // Attach the filter graph to the capture graph
                int hr = captureGraphBuilder.SetFiltergraph(this._graphBuilder);
                checkHR(hr, "Error attaching filter graph to capture graph");

                //Create the media control for controlling the graph
                _mediaCtrl = (IMediaControl)this._graphBuilder;

                //Add the Video input device to the graph
                hr = _graphBuilder.AddFilter(captureDevice, "QUAVS input filter");
                checkHR(hr, "Error attaching video input");

                //setup cature device 
                //### TO DO #### create properties for fps, width, height
                SetConfigParms(captureGraphBuilder, captureDevice, iFrameRate, iWidth, iHeight);

                //Add a sample grabber
                sampGrabber = (ISampleGrabber)new SampleGrabber();
                ConfigureSampleGrabber(sampGrabber);
                hr = _graphBuilder.AddFilter((IBaseFilter)sampGrabber, "QUAVS SampleGrabber");
                checkHR(hr, "Error adding sample grabber");

                //connect capture device to SampleGrabber
                hr = _graphBuilder.Connect(GetPin(captureDevice, "Capture"), GetPin((IBaseFilter)sampGrabber, "Input"));
                checkHR(hr, "Error attaching sample grabber to capture pin");

                //Add Ininite Pin Tee
                theIPinTee = (IBaseFilter)new InfTee();
                hr = _graphBuilder.AddFilter(theIPinTee, "QUAVS Pin Tee");
                checkHR(hr, "Error adding infinite tee pin");

                //connect capture SampleGrabber to IPinTee
                hr = _graphBuilder.Connect(GetPin((IBaseFilter)sampGrabber, "Output"), GetPin(theIPinTee, "Input"));
                checkHR(hr, "Error adding infinite tee pin");

                //Add the Video compressor filter to the graph
                hr = _graphBuilder.AddFilter(captureCompressor, "QUAVS compressor filter");
                checkHR(hr, "Error adding infinite tee pin");

                //connect capture IPinTee output1 to compressor
                hr = _graphBuilder.Connect(GetPin(theIPinTee, "Output1"), GetPin(captureCompressor, "Input"));
                DsError.ThrowExceptionForHR(hr);


                //Create the file writer part of the graph. SetOutputFileName does this for us, and returns the mux and sink
                hr = captureGraphBuilder.SetOutputFileName(MediaSubType.Avi, strFileName, out mux, out sink);
                DsError.ThrowExceptionForHR(hr);

                //connect capture IPinTee output1 to compressor
                hr = _graphBuilder.Connect(GetPin(captureCompressor, "Output"), GetPin(mux, "Input 01"));
                DsError.ThrowExceptionForHR(hr);

                // Get the default video renderer
                IBaseFilter theRenderer = new VideoRendererDefault() as IBaseFilter;
                hr = _graphBuilder.AddFilter(theRenderer, "Renderer");
                DsError.ThrowExceptionForHR(hr);

                //connect capture IPinTee output1 to compressor
                hr = _graphBuilder.Connect(GetPin(theIPinTee, "Output2"), GetPin(theRenderer, "VMR Input0"));
                DsError.ThrowExceptionForHR(hr);

                SaveSizeInfo(sampGrabber);
#if DEBUG
                _rot = new DsROTEntry(_graphBuilder);
#endif

                if (owner != IntPtr.Zero)
                {
                    //get the video window from the graph
                    IVideoWindow videoWindow = null;
                    videoWindow = (IVideoWindow)_graphBuilder;

                    //Set the owener of the videoWindow to an IntPtr of some sort (the Handle of any control - could be a form / button etc.)
                    hr = videoWindow.put_Owner(owner);
                    DsError.ThrowExceptionForHR(hr);

                    //Set the style of the video window
                    hr = videoWindow.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipChildren);
                    DsError.ThrowExceptionForHR(hr);

                    hr = videoWindow.SetWindowPosition(0, 0, iWidth, iHeight);
                    DsError.ThrowExceptionForHR(hr);

                    // Make the video window visible
                    hr = videoWindow.put_Visible(OABool.True);
                    DsError.ThrowExceptionForHR(hr);
                }

            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
            finally
            {
                if (sink != null)
                {
                    Marshal.ReleaseComObject(sink);
                    sink = null;
                }
                if (mux != null)
                {
                    Marshal.ReleaseComObject(mux);
                    mux = null;
                }
                if (captureGraphBuilder != null)
                {
                    Marshal.ReleaseComObject(captureGraphBuilder);
                    captureGraphBuilder = null;
                }
                if (sampGrabber != null)
                {
                    Marshal.ReleaseComObject(sampGrabber);
                    sampGrabber = null;
                }
                if (theIPinTee != null)
                {
                    Marshal.ReleaseComObject(theIPinTee);
                    theIPinTee = null;
                }
            }
        }

        /// <summary> Read and store the properties </summary>
        private void SaveSizeInfo(ISampleGrabber sampGrabber)
        {
            int hr;

            // Get the media type from the SampleGrabber
            AMMediaType media = new AMMediaType();
            hr = sampGrabber.GetConnectedMediaType(media);
            DsError.ThrowExceptionForHR(hr);

            if ((media.formatType != FormatType.VideoInfo) || (media.formatPtr == IntPtr.Zero))
            {
                throw new NotSupportedException("Unknown Grabber Media Format");
            }

            // Grab the size info
            VideoInfoHeader videoInfoHeader = (VideoInfoHeader)Marshal.PtrToStructure(media.formatPtr, typeof(VideoInfoHeader));
            _videoWidth = videoInfoHeader.BmiHeader.Width;
            _videoHeight = videoInfoHeader.BmiHeader.Height;
            _stride = _videoWidth * (videoInfoHeader.BmiHeader.BitCount / 8);

            DsUtils.FreeAMMediaType(media);
            media = null;
        }

        /// <summary> Set the options on the sample grabber </summary>
        private void ConfigureSampleGrabber(ISampleGrabber sampGrabber)
        {
            int hr;
            AMMediaType media = new AMMediaType();

            // Set the media type to Video/RBG24
            media.majorType = MediaType.Video;

#if ARGB32
            media.subType = MediaSubType.ARGB32;
#else
            media.subType = MediaSubType.RGB24;
#endif

            media.formatType = FormatType.VideoInfo;
            hr = sampGrabber.SetMediaType(media);
            DsError.ThrowExceptionForHR(hr);

            DsUtils.FreeAMMediaType(media);
            media = null;

            // Configure the samplegrabber callback
            hr = sampGrabber.SetCallback(this, 1);
            DsError.ThrowExceptionForHR(hr);
        }

        // Set the Framerate, and video size
        private void SetConfigParms(ICaptureGraphBuilder2 capGraph, IBaseFilter capFilter, int iFrameRate, int iWidth, int iHeight)
        {
            int hr;
            object o;
            AMMediaType media;
            IAMStreamConfig videoStreamConfig;
            IAMVideoControl videoControl = capFilter as IAMVideoControl;

            // Find the stream config interface
            hr = capGraph.FindInterface(PinCategory.Capture, MediaType.Video, capFilter, typeof(IAMStreamConfig).GUID, out o);

            videoStreamConfig = o as IAMStreamConfig;
            try
            {
                if (videoStreamConfig == null)
                {
                    throw new Exception("Failed to get IAMStreamConfig");
                }

                hr = videoStreamConfig.GetFormat(out media);
                DsError.ThrowExceptionForHR(hr);

                // copy out the videoinfoheader
                VideoInfoHeader v = new VideoInfoHeader();
                Marshal.PtrToStructure(media.formatPtr, v);

                // if overriding the framerate, set the frame rate
                if (iFrameRate > 0)
                {
                    v.AvgTimePerFrame = 10000000 / iFrameRate;
                }

                // if overriding the width, set the width
                if (iWidth > 0)
                {
                    v.BmiHeader.Width = iWidth;
                }

                // if overriding the Height, set the Height
                if (iHeight > 0)
                {
                    v.BmiHeader.Height = iHeight;
                }

                // Copy the media structure back
                Marshal.StructureToPtr(v, media.formatPtr, false);

                // Set the new format
                hr = videoStreamConfig.SetFormat(media);
                DsError.ThrowExceptionForHR(hr);

                DsUtils.FreeAMMediaType(media);
                media = null;

                // Fix upsidedown video
                if (videoControl != null)
                {
                    VideoControlFlags pCapsFlags;

                    IPin pPin = DsFindPin.ByCategory(capFilter, PinCategory.Capture, 0);
                    hr = videoControl.GetCaps(pPin, out pCapsFlags);
                    DsError.ThrowExceptionForHR(hr);

                    if ((pCapsFlags & VideoControlFlags.FlipVertical) > 0)
                    {
                        hr = videoControl.GetMode(pPin, out pCapsFlags);
                        DsError.ThrowExceptionForHR(hr);

                        hr = videoControl.SetMode(pPin, 0);
                    }
                }
            }
            finally
            {
                Marshal.ReleaseComObject(videoStreamConfig);
            }
        }

        /// <summary> Shut down capture </summary>
        private void CloseInterfaces()
        {
            int hr;

            try
            {
                if (_mediaCtrl != null)
                {
                    // Stop the graph
                    hr = _mediaCtrl.Stop();
                    _mediaCtrl = null;
                    _bRunning = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

#if DEBUG
            if (_rot != null)
            {
                _rot.Dispose();
            }
#endif

            if (_graphBuilder != null)
            {
                Marshal.ReleaseComObject(_graphBuilder);
                _graphBuilder = null;
            }

            if(_hud != null)
                _hud.Dispose();

            GC.Collect();
        }
        
        /// <summary> sample callback, NOT USED. </summary>
        int ISampleGrabberCB.SampleCB(double SampleTime, IMediaSample pSample)
        {
            Marshal.ReleaseComObject(pSample);
            return 0;
        }

        /// <summary> buffer callback, COULD BE FROM FOREIGN THREAD. </summary>
        int ISampleGrabberCB.BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
        {
            // Avoid the possibility that someone is calling SetLogo() at this instant
            lock (this)
            {
                DrawHUD(pBuffer);
            }

            return 0;

        }

        static void checkHR(int hr, string msg)
        {
            if (hr < 0)
            {
                Trace.WriteLine(msg);
                DsError.ThrowExceptionForHR(hr);
            }
        }

        static IPin GetPin(IBaseFilter filter, string pinname)
        {
            IEnumPins epins;
            int hr = filter.EnumPins(out epins);
            checkHR(hr, "Can’t enumerate pins");
            IntPtr fetched = Marshal.AllocCoTaskMem(4);
            IPin[] pins = new IPin[1];
            while (epins.Next(1, pins, fetched) == 0)
            {
                PinInfo pinfo;
                pins[0].QueryPinInfo(out pinfo);
                bool found = (pinfo.name == pinname);
                DsUtils.FreePinInfo(pinfo);
                if (found)
                    return pins[0];
            }
            checkHR(-1, "Pin not found");
            return null;
        }

        /// <summary>
        /// Enumerates all filters of the selected category and returns the IBaseFilter for the 
        /// filter described in friendlyname
        /// </summary>
        /// <param name="category">Category of the filter</param>
        /// <param name="friendlyname">Friendly name of the filter</param>
        /// <returns>IBaseFilter for the device</returns>
        private IBaseFilter CreateFilter(Guid category, string friendlyname)
        {
            object source = null;
            Guid iid = typeof(IBaseFilter).GUID;
            foreach (DsDevice device in DsDevice.GetDevicesOfCat(category))
            {
                if (device.Name.CompareTo(friendlyname) == 0)
                {
                    device.Mon.BindToObject(null, null, ref iid, out source);
                    break;
                }
            }

            return (IBaseFilter)source;
        }

        

        public void DrawHUD(IntPtr pBuffer)
        {
            Bitmap dst;
            Bitmap src;

            src = new Bitmap(_videoWidth, _videoHeight, PixelFormat.Format32bppArgb);
            dst = new Bitmap(_videoWidth, _videoHeight, _stride, PixelFormat.Format32bppArgb, pBuffer);
            
            
            _hud.DrawHUD(src, dst);
            // dispose of the various objects
            src.Dispose();
            dst.Dispose();
        }
    }
}
