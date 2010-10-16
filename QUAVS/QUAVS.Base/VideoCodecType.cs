using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Configuration;

namespace QUAVS.Base
{
    
    [TypeConverter(typeof(VideoCodecTypeConverter))]
    [SettingsSerializeAs(SettingsSerializeAs.String)]
    [EditorAttribute(typeof(VideoCodecUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
    [Category("QUAVS")]
    [Description("select video codec")]
    public class VideoCodecType
    {
        private string _vs;

        public string VS
        {
            get { return _vs; }
            set { _vs = value; }
        }

        public VideoCodecType()
        { }

        public VideoCodecType(string vs)
        {
            _vs = vs;
        }

        public override string ToString()
        {
            return _vs;
        }

        // User-defined conversion from Digit to double
        public static implicit operator string(VideoCodecType vs)
        {
            if (vs != null)
                return vs._vs;
            else
                return null;
        }
        //  User-defined conversion from double to Digit
        public static implicit operator VideoCodecType(string vs)
        {
            return new VideoCodecType(vs);
        }
    }

    public class VideoCodecTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string)) return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
            {
                VideoCodecType mst = new VideoCodecType();

                string str = value as string;
                if (str != null)
                {
                    mst.VS = str;
                }

                return mst;
            }
            else return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string str = string.Empty;

                VideoCodecType mst = value as VideoCodecType;
                if (mst != null)
                {
                    str = string.Format("{0}", mst.VS);
                }
                return str;
            }
            else return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

