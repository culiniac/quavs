using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Diagnostics;

using DirectShowLib;

namespace QUAVS.Types
{
    /// <summary>
    /// 
    /// </summary>
    public partial class VideoCodecForm : Form
    {
        //A (modified) definition of OleCreatePropertyFrame found here: http://groups.google.no/group/microsoft.public.dotnet.languages.csharp/browse_thread/thread/db794e9779144a46/55dbed2bab4cd772?lnk=st&q=[DllImport(%22olepro32.dll%22)]&rnum=1&hl=no#55dbed2bab4cd772
        /// <summary>
        /// OLEs the create property frame.
        /// </summary>
        /// <param name="hwndOwner">The HWND owner.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="lpszCaption">The LPSZ caption.</param>
        /// <param name="cObjects">The c objects.</param>
        /// <param name="ppUnk">The pp unk.</param>
        /// <param name="cPages">The c pages.</param>
        /// <param name="lpPageClsID">The lp page CLS ID.</param>
        /// <param name="lcid">The lcid.</param>
        /// <param name="dwReserved">The dw reserved.</param>
        /// <param name="lpvReserved">The LPV reserved.</param>
        /// <returns></returns>
        [DllImport(@"oleaut32.dll")]
        public static extern int OleCreatePropertyFrame(
            IntPtr hwndOwner,
            int x,
            int y,
            [MarshalAs(UnmanagedType.LPWStr)] string lpszCaption,
            int cObjects,
            [MarshalAs(UnmanagedType.Interface, ArraySubType = UnmanagedType.IUnknown)] 
			ref object ppUnk,
            int cPages,
            IntPtr lpPageClsID,
            int lcid,
            int dwReserved,
            IntPtr lpvReserved);

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get { return listBoxVideoCodec.Text; }
            set 
            { 
                listBoxVideoCodec.Text = value;
                // Ensure we have a proper string to search for.
                if (value != string.Empty)
                {
                    // Find the item in the list and store the index to the item.
                    int index = listBoxVideoCodec.FindStringExact(value);
                    // Determine if a valid index is returned. Select the item if it is valid.
                    if (index != ListBox.NoMatches)
                        listBoxVideoCodec.SetSelected(index, true);
                    else
                        Trace.WriteLine("Video codec not found");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoCodecForm"/> class.
        /// </summary>
        public VideoCodecForm()
        {
            InitializeComponent();
            //enumerate Video Input filters
            foreach (DsDevice ds in DsDevice.GetDevicesOfCat(FilterCategory.VideoCompressorCategory))
            {
                listBoxVideoCodec.Items.Add(ds.Name);
            }
        }

        /// <summary>
        /// Handles the Load event of the VideoCodecForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void VideoCodecForm_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Handles the Click event of the buttonConfig control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonConfig_Click(object sender, EventArgs e)
        {
            IBaseFilter theDevice = theDevice = CreateFilter(FilterCategory.VideoCompressorCategory, listBoxVideoCodec.Text);
            DisplayPropertyPage(theDevice);
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
        /// Displays a property page for a filter
        /// </summary>
        /// <param name="dev">The filter for which to display a property page</param>
        private void DisplayPropertyPage(IBaseFilter dev)
        {
            //Get the ISpecifyPropertyPages for the filter
            ISpecifyPropertyPages pProp = dev as ISpecifyPropertyPages;
            int hr = 0;

            if (pProp == null)
            {
                //If the filter doesn't implement ISpecifyPropertyPages, try displaying IAMVfwCompressDialogs instead!
                IAMVfwCompressDialogs compressDialog = dev as IAMVfwCompressDialogs;
                if (compressDialog != null)
                {

                    hr = compressDialog.ShowDialog(VfwCompressDialogs.Config, IntPtr.Zero);
                    DsError.ThrowExceptionForHR(hr);
                }
                return;
            }

            //Get the name of the filter from the FilterInfo struct
            FilterInfo filterInfo;
            hr = dev.QueryFilterInfo(out filterInfo);
            DsError.ThrowExceptionForHR(hr);

            // Get the propertypages from the property bag
            DsCAUUID caGUID;
            hr = pProp.GetPages(out caGUID);
            DsError.ThrowExceptionForHR(hr);

            // Check for property pages on the output pin
            IPin pPin = DsFindPin.ByDirection(dev, PinDirection.Output, 0);
            ISpecifyPropertyPages pProp2 = pPin as ISpecifyPropertyPages;
            if (pProp2 != null)
            {
                DsCAUUID caGUID2;
                hr = pProp2.GetPages(out caGUID2);
                DsError.ThrowExceptionForHR(hr);

                if (caGUID2.cElems > 0)
                {
                    int soGuid = Marshal.SizeOf(typeof(Guid));

                    // Create a new buffer to hold all the GUIDs
                    IntPtr p1 = Marshal.AllocCoTaskMem((caGUID.cElems + caGUID2.cElems) * soGuid);

                    // Copy over the pages from the Filter
                    for (int x = 0; x < caGUID.cElems * soGuid; x++)
                    {
                        Marshal.WriteByte(p1, x, Marshal.ReadByte(caGUID.pElems, x));
                    }

                    // Add the pages from the pin
                    //for (int x = 0; x < caGUID2.cElems * soGuid; x++)
                    //{
                    //    Marshal.WriteByte(p1, x + (caGUID.cElems * soGuid), Marshal.ReadByte(caGUID2.pElems, x));
                    //}

                    // Release the old memory
                    Marshal.FreeCoTaskMem(caGUID.pElems);
                    Marshal.FreeCoTaskMem(caGUID2.pElems);

                    // Reset caGUID to include both
                    caGUID.pElems = p1;
                    caGUID.cElems += caGUID2.cElems;
                }
            }

            // Create and display the OlePropertyFrame
            object oDevice = (object)dev;
            hr = OleCreatePropertyFrame(this.Handle, 0, 0, filterInfo.achName, 1, ref oDevice, caGUID.cElems, caGUID.pElems, 0, 0, IntPtr.Zero);
            DsError.ThrowExceptionForHR(hr);

            // Release COM objects
            Marshal.FreeCoTaskMem(caGUID.pElems);
            Marshal.ReleaseComObject(pProp);
            if (filterInfo.pGraph != null)
            {
                Marshal.ReleaseComObject(filterInfo.pGraph);
            }
        }
    }
}
