using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace QUAVS.Types
{
    public class VideoSourceUITypeEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            VideoSourceType videoSource = new VideoSourceType();
            videoSource = value as VideoSourceType;
            if (svc != null)
            {
                using (VideoSourceForm form = new VideoSourceForm())
                {
                    form.Value = videoSource;
                    if (svc.ShowDialog(form) == DialogResult.OK)
                    {
                        videoSource = form.Value; // update object
                    }
                }
            }

            return videoSource; // can also replace the wrapper object here
        }
    }
}
