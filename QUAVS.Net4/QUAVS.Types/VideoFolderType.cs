using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Configuration;

namespace QUAVS.Types
{

    /// <summary>
    /// 
    /// </summary>
    [TypeConverter(typeof(VideoFolderTypeConverter))]
    [SettingsSerializeAs(SettingsSerializeAs.String)]
    [EditorAttribute(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
    [Category("QUAVS")]
    [Description("select video folder")]
    public class VideoFolderType
    {
        private string _vs;

        /// <summary>
        /// Gets or sets the VS.
        /// </summary>
        /// <value>The VS.</value>
        public string VS
        {
            get { return _vs; }
            set { _vs = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoFolderType"/> class.
        /// </summary>
        public VideoFolderType()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoFolderType"/> class.
        /// </summary>
        /// <param name="vs">The vs.</param>
        public VideoFolderType(string vs)
        {
            _vs = vs;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return _vs;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="QUAVS.Types.VideoFolderType"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="vs">The vs.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(VideoFolderType vs)
        {
            if (vs != null)
                return vs._vs;
            else
                return null;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="QUAVS.Types.VideoFolderType"/>.
        /// </summary>
        /// <param name="vs">The vs.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator VideoFolderType(string vs)
        {
            return new VideoFolderType(vs);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoFolderTypeConverter : TypeConverter
    {
        /// <summary>
        /// Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="sourceType">A <see cref="T:System.Type"/> that represents the type you want to convert from.</param>
        /// <returns>
        /// true if this converter can perform the conversion; otherwise, false.
        /// </returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string)) return true;
            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Converts the given object to the type of this converter, using the specified context and culture information.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo"/> to use as the current culture.</param>
        /// <param name="value">The <see cref="T:System.Object"/> to convert.</param>
        /// <returns>
        /// An <see cref="T:System.Object"/> that represents the converted value.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The conversion cannot be performed.
        /// </exception>
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
            {
                VideoFolderType mst = new VideoFolderType();

                string str = value as string;
                if (str != null)
                {
                    mst.VS = str;
                }

                return mst;
            }
            else return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Converts the given value object to the specified type, using the specified context and culture information.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
        /// <param name="value">The <see cref="T:System.Object"/> to convert.</param>
        /// <param name="destinationType">The <see cref="T:System.Type"/> to convert the <paramref name="value"/> parameter to.</param>
        /// <returns>
        /// An <see cref="T:System.Object"/> that represents the converted value.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="destinationType"/> parameter is null.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The conversion cannot be performed.
        /// </exception>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string str = string.Empty;

                VideoFolderType mst = value as VideoFolderType;
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

