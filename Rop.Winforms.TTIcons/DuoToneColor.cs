using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Rop.Winforms.DuotoneIcons
{
    [TypeConverter(typeof(DuoToneColorConverter))]
    public struct DuoToneColor:IEquatable<DuoToneColor>
    {
        public static readonly DuoToneColor Default=new DuoToneColor(Color.Black, Color.Gray);
        public static readonly DuoToneColor DefaultOneTone=new DuoToneColor(Color.Black, Color.Transparent);
        public static readonly DuoToneColor Empty=new DuoToneColor(Color.Empty, Color.Empty);
        public static readonly DuoToneColor Disabled=new DuoToneColor(Color.Gray, Color.Transparent);
        public DuoToneColor(Color color1, Color color2)
        {
            Color1 = color1;
            Color2 = color2;
        }
        public DuoToneColor()        {
            Color1 = Color.Empty;
            Color2 = Color.Empty;
        }
        [Browsable(true)]
        [DisplayName("Color1")]
        [Description("The first color.")]
       
        public Color Color1 { get; set; }
        [Browsable(true)]
        [DisplayName("Color2")]
        [Description("The second color.")]
       
        public Color Color2 { get; set; }
        [Browsable(false)]
        public bool IsEmpty=>Color1==Color.Empty && Color2==Color.Empty;
        
        public bool Equals(DuoToneColor other)
        {
            return Color1.Equals(other.Color1) && Color2.Equals(other.Color2);
        }

        public override bool Equals(object obj)
        {
            return obj is DuoToneColor other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Color1.GetHashCode()*65497+Color2.GetHashCode();
        }

        public void Deconstruct(out Color color1, out Color color2)
        {
            color1 = Color1;
            color2 = Color2;
        }

        public DuoToneColor WithColor1(Color color1)
        {
            return new DuoToneColor(color1, Color2);
        }
        public DuoToneColor WithColor2(Color color2)
        {
            return new DuoToneColor(Color1, color2);
        }

        public static explicit operator DuoToneColor((Color, Color) tuple)
        {
            return new DuoToneColor(tuple.Item1, tuple.Item2);
        }

        public DuoToneColor FinalColor(Color foreColor)
        {
            return new DuoToneColor(Color1.IsEmpty ? foreColor : Color1,
                Color2.IsEmpty ? Color.Transparent : Color2);
        }

        internal static string ColorToString(Color color)
        {
            if (color == Color.Empty) return "Empty";
            if (color == Color.Transparent) return "Transparent";
            if (color.IsNamedColor) return color.Name;
            if (color.A == 255)
                return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
            else
                return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        internal static Color ColorFromString(string s)
        {
            if (s == "Empty") return Color.Empty;
            if (s == "Transparent") return Color.Transparent;
            if (s.StartsWith("#"))
            {
                var c = s.Substring(1);
                var a = 0;
                var r= 0;
                var g = 0;
                var b = 0;

                if (c.Length == 6)
                {
                    a = 255;
                    r = int.Parse(c.Substring(0, 2), NumberStyles.HexNumber);
                    g = int.Parse(c.Substring(2, 2), NumberStyles.HexNumber);
                    b = int.Parse(c.Substring(4, 2), NumberStyles.HexNumber);
                    var isnamed=SearchNamedColor(r,g,b);
                    if (isnamed!=null)
                    {
                        return isnamed.Value;
                    }
                }
                else
                {
                    a=int.Parse(c.Substring(0, 2), NumberStyles.HexNumber);
                    r = int.Parse(c.Substring(2, 2), NumberStyles.HexNumber);
                    g = int.Parse(c.Substring(4, 2), NumberStyles.HexNumber);
                    b = int.Parse(c.Substring(6, 2), NumberStyles.HexNumber);
                }
                return Color.FromArgb(a,r,g,b);
            }
            return Color.FromName(s);
        }

        private static Color? SearchNamedColor(int r, int g, int b)
        {
            var props = typeof(Color).GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (var prop in props)
            {
                if (prop.PropertyType == typeof(Color))
                {
                    var color = (Color)prop.GetValue(null);
                    if (color.R == r && color.G == g && color.B == b)
                    {
                        return color;
                    }
                }
            }
            props=typeof(SystemColors).GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (var prop in props)
            {
                if (prop.PropertyType == typeof(Color))
                {
                    var color = (Color)prop.GetValue(null);
                    if (color.R == r && color.G == g && color.B == b)
                    {
                        return color;
                    }
                }
            }
            return null;
        }

        public override string ToString()
        {
            return $"({ColorToString(Color1)},{ColorToString(Color2)})";
        }
        public static bool operator ==(DuoToneColor a,DuoToneColor b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(DuoToneColor a, DuoToneColor b)
        {
            return !a.Equals(b);
        }

        public static DuoToneColor OneTone(Color color)
        {
            return new DuoToneColor(color, Color.Transparent);
        }

        public static DuoToneColor Parse(string s)
        {
            if (s.StartsWith("(") && s.EndsWith(")"))
            {
                var parts = s.Substring(1, s.Length - 2).Split(',');
                if (parts.Length == 2)
                {
                    return new DuoToneColor(ColorFromString(parts[0]), ColorFromString(parts[1]));
                }
            }
            return new DuoToneColor(ColorFromString(s), Color.Transparent);
        }
    }

  
    
    public class DuoToneColorConverter : ExpandableObjectConverter
    {
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is DuoToneColor color)
            {
                return color.ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string s)
            {
                return DuoToneColor.Parse(s);
            }
            return base.ConvertFrom(context, culture, value);
        }


        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (propertyValues == null) throw new ArgumentNullException("propertyValues");
            if (propertyValues.Count == 1)
            {
                var v = propertyValues.Values.Cast<object>().First();
                if (v is DuoToneColor color)
                {
                    return new DuoToneColor(color.Color1,color.Color2);
                }
                return null;
            }

            var color1=propertyValues["Color1"] as Color?;
            var color2=propertyValues["Color2"] as Color?;
            if (color1.HasValue && color2.HasValue)
            {
                return new DuoToneColor(color1.Value, color2.Value);
            }
            return default(DuoToneColor);
        }

        
    }
  

}
