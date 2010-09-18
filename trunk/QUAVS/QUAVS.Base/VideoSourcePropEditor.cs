using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.Drawing.Design;

namespace QUAVS.Base
{
    internal class VideoSourcePropEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue( ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                editorService =  provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            }

            if (editorService != null)
            {
                LightShapeSelectionControl selectionControl =
                    new LightShapeSelectionControl(
                    (MarqueeLightShape)value,
                    editorService);

                editorService.DropDownControl(selectionControl);

                value = selectionControl.LightShape;
            }

            return value;
        }

    }
}
