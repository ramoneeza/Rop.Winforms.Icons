using System;
using System.ComponentModel;
using System.Drawing;

namespace Rop.Winforms.DuotoneIcons
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public struct DuoToneColor:IEquatable<DuoToneColor>
    {
        public static DuoToneColor Default=new DuoToneColor(Color.Black, Color.Gray);
        public static DuoToneColor DefaultOneTone=new DuoToneColor(Color.Black, Color.Transparent);
        public static readonly DuoToneColor Empty=new DuoToneColor(Color.Empty, Color.Empty);
        public static readonly DuoToneColor Disabled=new DuoToneColor(Color.Gray, Color.Transparent);
        public DuoToneColor(Color color1, Color color2)
        {
            Color1 = color1;
            Color2 = color2;
        }
        [Browsable(true)]
        [DisplayName("Color1")]
        [Description("The first color.")]
        public Color Color1 { get; set; }
        [Browsable(true)]
        [DisplayName("Color2")]
        [Description("The second color.")]
        public Color Color2 { get; set; }
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
        public static explicit operator DuoToneColor((Color, Color) tuple)
        {
            return new DuoToneColor(tuple.Item1, tuple.Item2);
        }

        public DuoToneColor FinalColor(Color foreColor)
        {
            return new DuoToneColor(Color1.IsEmpty ? foreColor : Color1,
                Color2.IsEmpty ? Color.Transparent : Color2);
        }

        public override string ToString()
        {
            return $"({Color1},{Color2})";
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
    }
}
