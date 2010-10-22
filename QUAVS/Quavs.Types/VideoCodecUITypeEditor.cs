using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace QUAVS.Types
{
    public class VideoCodecUITypeEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            VideoCodecType videoCodec = new VideoCodecType();
            videoCodec = value as VideoCodecType;
            if (svc != null)
            {
                using (VideoCodecForm form = new VideoCodecForm())
                {
                    form.Value = videoCodec;
                    if (svc.ShowDialog(form) == DialogResult.OK)
                    {
                        videoCodec = form.Value; // update object
                    }
                }
            }

            return videoCodec; // can also replace the wrapper object here
        }
    }
}
