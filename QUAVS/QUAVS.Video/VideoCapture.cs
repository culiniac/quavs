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
using System.IO;
using DirectShowLib;
using QUAVS.Types;
using QUAVS.Base;

namespace QUAVS.Video
{
    /// <summary>
    /// 
    /// </summary>
    public enum VideoCaptureState
    {
        /// <summary>
        /// 
        /// </summary>
        UNINITIALIZED,
        /// <summary>
        /// 
        /// </summary>
        STOPPED,
        /// <summary>
        /// 
        /// </summary>
        PAUSED,
        /// <summary>
        /// 
        /// </summary>
        RECORDING_PAUSED,
        /// <summary>
        /// 
        /// </summary>
        RUNNING,
        /// <summary>
        /// 
        /// </summary>
        RECORDING
    }

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

        private VideoCaptureState _state;
        private VideoSourceType _strCapture;
        private VideoCodecType _strCompressor;
        private string _strFolderName;
        private string _strFileName;
        private IntPtr _hOwner;

        private int _videoWidth;
        private int _videoHeight;

        private int _SGvideoWidth;
        private int _SGvideoHeight;
        private int _SGstride;

        private int _fps;

        private bool _hud_enabled;
        private HUD _hud;

       

        private bool _canPlay;
        private bool _canStop;
        private bool _canRecord;
        private bool _canPause;
        
        public HUD HUD
        {
            get { return _hud; }
        }

        public bool CanPlay
        {
            get { return _canPlay; }
        }
        public bool CanStop
        {
            get { return _canStop; }
        }
        public bool CanRecord
        {
            get { return _canRecord; }
        }
        public bool CanPause
        {
            get { return _canPause; }
        }
        public VideoCaptureState State
        {
            get { return _state; }
        }
        public VideoSourceType CaptureDevice
        {
            get { return _strCapture; }
            set { _strCapture = value; }
        }
        public VideoCodecType CompressorCodec
        {
            get { return _strCompressor; }
            set { _strCompressor = value; }
        }
        public string VideoFolder
        {
            get { return _strFolderName; }
            set { _strFolderName = value; }
        }
        public IntPtr Owner
        {
            get { return _hOwner; }
            set { _hOwner = value; }
        }
        public int VideoWidth
        {
            get { return _videoWidth; }
            set { _videoWidth = value;}
        }
        public int VideoHeight
        {
            get { return _videoHeight; }
            set { _videoHeight = value;}
        }
        public int Fps
        {
            get { return _fps; }
            set { _fps = value; }
        }
        public bool HUD_Enabled
        {
            get { return _hud_enabled; }
            set { _hud_enabled = value; }
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
        
        public VideoCapture(IntPtr owner)
        {
            try 
	        {	        
		        _hOwner = owner;

                    ChangeState(VideoCaptureState.UNINITIALIZED);

                    _hud_enabled = false;
                    _hud = new HUD();
                    _hud_enabled = true;
	        }
	        catch (Exception e)
	        {
                TraceException.WriteLine(e);
	        };
        }

        public void InitializeCapture(bool recording)
        {
            try
            {
#if DEBUG
                Stopwatch benchmark = Stopwatch.StartNew();
#endif
                CloseInterfaces();
                if (!Directory.Exists(_strFolderName))
                {
                    // Create the directory it does not exist.
                    Directory.CreateDirectory(_strFolderName);
                }
                string tmp = DateTime.Now.ToString("yyymmddHHmmssfff");
                _strFileName = _strFolderName + Path.DirectorySeparatorChar + tmp + ".avi";
                // Set up the capture graph
                SetupGraph(_strCapture, _strCompressor, _strFileName, _fps, _videoWidth, _videoHeight, _hOwner, recording);

#if DEBUG
                benchmark.Stop();
                Debug.WriteLine("SetupGraph: " + ((double)(benchmark.Elapsed.TotalMilliseconds)).ToString("0.00 ms"));
#endif
                ChangeState(VideoCaptureState.STOPPED);
            }
            catch(Exception e)
            {
                TraceException.WriteLine(e);
                Dispose();
            }
        }

        /// <summary>
        /// release everything.
        /// </summary>
        public void Dispose()
        {
            CloseInterfaces();
            if (_hud != null)
                _hud.Dispose();
        }
        
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="VideoCapture"/> is reclaimed by garbage collection.
        /// </summary>
        ~VideoCapture()
        {
            Dispose();
        }

        /// <summary>
        /// Plays this instance.
        /// </summary>
        public void Play()
        {
            if (_canPlay)
            {
                if (_state == VideoCaptureState.PAUSED)
                {
                    if (_mediaCtrl != null)
                    {
                        int hr = _mediaCtrl.Run();
                        checkHR(hr, "Cannot start capture graph");
                        ChangeState(VideoCaptureState.RUNNING);
                    }
                    else
                    {
                        ChangeState(VideoCaptureState.UNINITIALIZED);
                    }

                }
                else
                {
                    InitializeCapture(false);
                    if (_mediaCtrl != null)
                    {
                        int hr = _mediaCtrl.Run();
                        checkHR(hr, "Cannot start capture graph");
                        ChangeState(VideoCaptureState.RUNNING);
                    }
                    else
                    {
                       ChangeState(VideoCaptureState.UNINITIALIZED);
                    }
                }
            }

        }
        
        /// <summary>
        /// Pause the capture graph.
        /// Running the graph takes up a lot of resources.  Pause it when it
        /// isn't needed.
        /// </summary>
        public void Pause()
        {
            if (_canPause)
            {
                if (_mediaCtrl != null)
                {
                    if (_state == VideoCaptureState.RUNNING)
                    {
                        if (_mediaCtrl != null)
                        {
                            int hr = _mediaCtrl.Pause();
                            checkHR(hr, "Cannot pause capture graph");
                            ChangeState(VideoCaptureState.PAUSED);
                        }
                        else
                        {
                            ChangeState(VideoCaptureState.UNINITIALIZED);
                        }

                    }
                    if (_state == VideoCaptureState.RECORDING)
                    {
                        if (_mediaCtrl != null)
                        {
                            int hr = _mediaCtrl.Pause();
                            checkHR(hr, "Cannot pause capture graph");
                            ChangeState(VideoCaptureState.RECORDING_PAUSED);
                        }
                        else
                        {
                            ChangeState(VideoCaptureState.UNINITIALIZED);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Stops the capture graph.
        /// </summary>
        public void Stop()
        {
            if (_canStop)
            {
                if (_state != VideoCaptureState.UNINITIALIZED)
                {
                    if (_mediaCtrl != null)
                    {
                        int hr = _mediaCtrl.StopWhenReady();
                        checkHR(hr, "Cannot pause capture graph");
                        ChangeState(VideoCaptureState.STOPPED);
                    }
                    else
                    {
                        ChangeState(VideoCaptureState.UNINITIALIZED);
                    }
                }
            }
        }

        /// <summary>
        /// Recordings this instance.
        /// </summary>
        public void Record()
        {
            if(_canRecord)
            {
                if (_state == VideoCaptureState.RECORDING_PAUSED)
                {
                    if (_mediaCtrl != null)
                    {
                        int hr = _mediaCtrl.Run();
                        checkHR(hr, "Cannot start capture graph");
                        ChangeState(VideoCaptureState.RECORDING);
                    }
                    else
                    {
                        ChangeState(VideoCaptureState.UNINITIALIZED);
                    }
                }
                else
                {
                    InitializeCapture(true);
                    if (_mediaCtrl != null)
                    {
                        int hr = _mediaCtrl.Run();
                        checkHR(hr, "Cannot start capture graph");
                        ChangeState(VideoCaptureState.RECORDING);
                    }
                    else
                    {
                        ChangeState(VideoCaptureState.UNINITIALIZED);
                    }
                }
            }
        }

        /// <summary>
        /// build the capture graph for grabber.
        /// </summary>
        /// <param name="strCapture">The STR capture.</param>
        /// <param name="strCompressor">The STR compressor.</param>
        /// <param name="strFileName">Name of the STR file.</param>
        /// <param name="iFrameRate">The i frame rate.</param>
        /// <param name="iWidth">Width of the i.</param>
        /// <param name="iHeight">Height of the i.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="record">if set to <c>true</c> [record].</param>
        private void SetupGraph(string strCapture, string strCompressor, string strFileName, int iFrameRate, int iWidth, int iHeight, IntPtr owner, bool record)
        {
            ICaptureGraphBuilder2 captureGraphBuilder = null;
            ISampleGrabber sampGrabber = null;
            IBaseFilter theIPinTee = null;
            IBaseFilter mux = null;
            IFileSinkFilter sink = null;
            IBaseFilter captureDevice = null;
            IBaseFilter captureCompressor = null;
            IBaseFilter theRenderer = null;
            int hr = 0;

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
                hr = captureGraphBuilder.SetFiltergraph(this._graphBuilder);
                checkHR(hr, "Error attaching filter graph to capture graph");

                //Add the Video input device to the graph
                hr = _graphBuilder.AddFilter(captureDevice, "QUAVS input filter");
                checkHR(hr, "Error attaching video input");

                //setup cature device 
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
                checkHR(hr, "Error adding SampleGrabber");

                if (record)
                {
                    //Add the Video compressor filter to the graph
                    hr = _graphBuilder.AddFilter(captureCompressor, "QUAVS compressor filter");
                    checkHR(hr, "Error adding compressor filter");

                    //connect capture IPinTee output1 to compressor
                    hr = _graphBuilder.Connect(GetPin(theIPinTee, "Output1"), GetPin(captureCompressor, "Input"));
                    checkHR(hr, "Error adding TO DO");


                    //Create the file writer part of the graph. SetOutputFileName does this for us, and returns the mux and sink
                    hr = captureGraphBuilder.SetOutputFileName(MediaSubType.Avi, strFileName, out mux, out sink);
                    checkHR(hr, "Error adding mux filter or setting output file name");
                    
                    //connect compressor to mux output
                    hr = _graphBuilder.Connect(GetPin(captureCompressor, "Output"), GetPin(mux, "Input 01"));
                    checkHR(hr, "Error connecting the compressor to mux");

                    // Get the default video renderer
                    theRenderer = new VideoRendererDefault() as IBaseFilter;
                    hr = _graphBuilder.AddFilter(theRenderer, "Renderer");
                    checkHR(hr, "Error adding screen renderer");

                    //connect capture TO DO
                    hr = _graphBuilder.Connect(GetPin(theIPinTee, "Output2"), GetPin(theRenderer, "VMR Input0"));
                    checkHR(hr, "Error connecting screen renderer");
                }
                else
                {
                    // Get the default video renderer
                    theRenderer = new VideoRendererDefault() as IBaseFilter;
                    hr = _graphBuilder.AddFilter(theRenderer, "Renderer");
                    checkHR(hr, "Error adding screen renderer");

                    //connect capture TO DO
                    hr = _graphBuilder.Connect(GetPin(theIPinTee, "Output1"), GetPin(theRenderer, "VMR Input0"));
                    checkHR(hr, "Error connecting screen renderer");
                }

                

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

                //Create the media control for controlling the graph
                _mediaCtrl = (IMediaControl)this._graphBuilder;

            }
            catch (Exception e)
            {
                TraceException.WriteLine(e); 
                //Trace.WriteLine(e.Message);
                CloseInterfaces();
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
                if (captureDevice != null)
                {
                    Marshal.ReleaseComObject(captureDevice);
                    captureDevice = null;
                }
                if (captureCompressor != null)
                {
                    Marshal.ReleaseComObject(captureCompressor);
                    captureCompressor = null;
                }
                if (theRenderer != null)
                {
                    Marshal.ReleaseComObject(theRenderer);
                    theRenderer = null;
                }
            }
        }

        /// <summary>
        /// Read and store the properties
        /// </summary>
        /// <param name="sampGrabber">The samp grabber.</param>
        private void SaveSizeInfo(ISampleGrabber sampGrabber)
        {
            int hr;

            // Get the media type from the SampleGrabber
            AMMediaType media = new AMMediaType();
            hr = sampGrabber.GetConnectedMediaType(media);
            checkHR(hr,"SaveSizeInfo");

            if ((media.formatType != FormatType.VideoInfo) || (media.formatPtr == IntPtr.Zero))
            {
                throw new NotSupportedException("Unknown Grabber Media Format");
            }

            // Grab the size info
            VideoInfoHeader videoInfoHeader = (VideoInfoHeader)Marshal.PtrToStructure(media.formatPtr, typeof(VideoInfoHeader));
            _SGvideoWidth = videoInfoHeader.BmiHeader.Width;
            _SGvideoHeight = videoInfoHeader.BmiHeader.Height;
            _SGstride = _SGvideoWidth * (videoInfoHeader.BmiHeader.BitCount / 8);
            
            _hud.VideoHeight = _SGvideoHeight;
            _hud.VideoWidth = _SGvideoWidth;

            DsUtils.FreeAMMediaType(media);
            media = null;
        }

        /// <summary>
        /// Set the options on the sample grabber
        /// </summary>
        /// <param name="sampGrabber">The samp grabber.</param>
        private void ConfigureSampleGrabber(ISampleGrabber sampGrabber)
        {
            int hr;
            AMMediaType media = new AMMediaType();

            // Set the media type to Video/ARBG32
            media.majorType = MediaType.Video;
            media.subType = MediaSubType.ARGB32;
            //obsolete:
            //media.subType = MediaSubType.RGB24;

            media.formatType = FormatType.VideoInfo;
            hr = sampGrabber.SetMediaType(media);
            DsError.ThrowExceptionForHR(hr);

            DsUtils.FreeAMMediaType(media);
            media = null;

            // Configure the samplegrabber callback
            hr = sampGrabber.SetCallback(this, 1);
            DsError.ThrowExceptionForHR(hr);
        }

        /// <summary>
        /// Set the Framerate, and video size
        /// </summary>
        /// <param name="capGraph">The cap graph.</param>
        /// <param name="capFilter">The cap filter.</param>
        /// <param name="iFrameRate">The i frame rate.</param>
        /// <param name="iWidth">Width of the i.</param>
        /// <param name="iHeight">Height of the i.</param>
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

        /// <summary>
        /// Shut down capture
        /// </summary>
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

                _state = VideoCaptureState.UNINITIALIZED;

                GC.Collect();
            
            }
            catch (Exception e)
            {
                TraceException.WriteLine(e);
            }
        }

        /// <summary>
        /// sample callback, NOT USED.
        /// </summary>
        /// <param name="SampleTime">The sample time.</param>
        /// <param name="pSample">The p sample.</param>
        /// <returns></returns>
        int ISampleGrabberCB.SampleCB(double SampleTime, IMediaSample pSample)
        {
            Marshal.ReleaseComObject(pSample);
            return 0;
        }

        /// <summary>
        /// buffer callback, COULD BE FROM FOREIGN THREAD.
        /// </summary>
        /// <param name="SampleTime">The sample time.</param>
        /// <param name="pBuffer">The p buffer.</param>
        /// <param name="BufferLen">The buffer len.</param>
        /// <returns></returns>
        int ISampleGrabberCB.BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
        {
            if (_hud_enabled)
            {
                lock (this)
                {
                    Bitmap dst;
                    Bitmap src;

                    src = new Bitmap(_SGvideoWidth, _SGvideoHeight, PixelFormat.Format32bppArgb);
                    dst = new Bitmap(_SGvideoWidth, _SGvideoHeight, _SGstride, PixelFormat.Format32bppArgb, pBuffer);

                    _hud.DrawHUD(src, dst);
                    // dispose of the various objects
                    src.Dispose();
                    dst.Dispose();
                }
            }
            return 0;
        }

        /// <summary>
        /// Checks the HR.
        /// </summary>
        /// <param name="hr">The hr.</param>
        /// <param name="msg">The MSG.</param>
        static void checkHR(int hr, string msg)
        {
            try
            {
                if (hr < 0)
                {
                    Trace.WriteLine(msg);
                    DsError.ThrowExceptionForHR(hr);
                }
            }
            catch (Exception e)
            {
                TraceException.WriteLine(e);
            }
        }

        /// <summary>
        /// Gets the pin.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="pinname">The pinname.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Changes the state.
        /// </summary>
        /// <param name="cState">State of the c.</param>
        private void ChangeState(VideoCaptureState cState)
        {
            _state = cState;

            switch (_state)
            {
                case VideoCaptureState.UNINITIALIZED:
                    _canPlay = true;
                    _canStop = false;
                    _canRecord = true;
                    _canPause = false;
                    break;
                case VideoCaptureState.STOPPED:
                    _canPlay = true;
                    _canStop = false;
                    _canRecord = true;
                    _canPause = false;
                    break;
                case VideoCaptureState.RUNNING:
                    _canPlay = false;
                    _canStop = true;
                    _canRecord = false;
                    _canPause = true;
                    break;
                case VideoCaptureState.RECORDING:
                    _canPlay = false;
                    _canStop = true;
                    _canRecord = false;
                    _canPause = true;
                    break;
                case VideoCaptureState.PAUSED:
                    _canPlay = true;
                    _canStop = true;
                    _canRecord = false;
                    _canPause = false;
                    break;
                case VideoCaptureState.RECORDING_PAUSED:
                    _canPlay = false;
                    _canStop = true;
                    _canRecord = true;
                    _canPause = false;
                    break;
                default:
                    _canPlay = true;
                    _canStop = false;
                    _canRecord = true;
                    _canPause = false;
                    break;
            }
        }
    }
}
