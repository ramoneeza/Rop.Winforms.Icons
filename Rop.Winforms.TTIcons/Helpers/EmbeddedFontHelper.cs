using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Rop.Winforms.DuotoneIcons.Helpers;

public static class EmbeddedFontHelper
{
    public static int PointsToPixels(float points, float dpi) => (int)(points * dpi / 72f);

    public static int PointsToPixels(this Graphics g, float points) => PointsToPixels(points, (int)g.DpiY);

    public static float GetAscentPoints(this Font f)
    {
        var a = GetAscentUnit(f);
        return a * f.SizeInPoints;
    }

    public static float GetAscentUnit(this Font f)
    {
        float heightEm = f.FontFamily.GetEmHeight(f.Style);
        float ascent = f.FontFamily.GetCellAscent(f.Style);
        return ascent / heightEm;
    }

    public static int GetAscentPixels(this Font f, float dpi)
    {
        var a = GetAscentPoints(f);
        return (int)(a * PointsToPixels(f.SizeInPoints, dpi));
    }

    public static SizeF MeasureBaseIconPoints(this Font font)
    {
        var s = font.GetAscentPoints();
        return new SizeF(s, s);
    }

    public static SizeF MeasureBaseIcon(this Graphics graphics, Font font)
    {
        var sz = MeasureBaseIconPoints(font);
        return new SizeF(graphics.PointsToPixels(sz.Width), graphics.PointsToPixels(sz.Height));
    }

    public static RectangleF MeasureBaseIconPoints(this Font font, float scale)
    {
        var sz = MeasureBaseIconPoints(font);
        var newh = sz.Height * scale;
        return new RectangleF(0, sz.Height - newh, sz.Width * scale, newh);
    }

    public static RectangleF MeasureBaseIcon(this Graphics g, Font font, float scale)
    {
        var r = MeasureBaseIconPoints(font, scale);
        return new Rectangle(g.PointsToPixels(r.Left), g.PointsToPixels(r.Top), g.PointsToPixels(r.Width), g.PointsToPixels(r.Height));
    }

    public static List<IconStringToken> SplitIconString(this string text)
    {
        text ??= "";
        var lst = new Queue<char>(text.ToCharArray());
        var prev = "";
        var res = new List<IconStringToken>();
        while (lst.Count > 0)
        {
            char c = lst.Dequeue();
            if (char.IsHighSurrogate(c))
            {
                if (prev != "") res.Add(IconStringToken.FactoryText(prev));
                prev = "";
                var c2 = lst.Dequeue();
                var uni = new string(new[] { c, c2 });
                res.Add(IconStringToken.FactoryIcon(uni));
            }
            else
            {
                prev = prev + c;
            }
        }
        if (prev != "") res.Add(IconStringToken.FactoryText(prev));
        return res;
    }

    public static float GetOffset(this Graphics g, Font font)
    {
        float offset = font.SizeInPoints / font.FontFamily.GetEmHeight(font.Style) * font.FontFamily.GetCellAscent(font.Style);
        float pixels = g.DpiY / 72f * offset;
        return pixels;
    }

    public static float GetOffsetInPoints(this Font font)
    {
        float ascent = font.FontFamily.GetCellAscent(font.Style);
        float emheight = font.FontFamily.GetEmHeight(font.Style);
        // Obtener el valor de baseline en píxeles
        return (ascent / emheight) * font.SizeInPoints;
    }
    
    public static MeasuredIconString MeasureIconString(this Graphics gr, List<IconStringToken> text, IIconStringSizeArgs args,IEmbeddedIcons bank)
    {
        float x = 0;
        float y = 0;
        float scale = args.Scale / 100f;
        var baseicon = gr.MeasureBaseIcon(args.Font, scale);
        var res = new List<(RectangleF, IconStringToken)>();
        var dif = (baseicon.Top >= 0) ? 0 : -baseicon.Top;
        foreach (var t in text)
        {
            var (u, cad) = t;
            if (u)
            {
                var sz=bank.MeasureIcon(cad, baseicon.Height);
                var m = baseicon;
                var w= sz.Width;
                res.Add((new RectangleF(x, y + args.OffsetIcon + dif + m.Top,w, m.Height), t));
                x += w;
            }
            else
            {
                var m = gr.MeasureString(cad, args.Font, PointF.Empty, StringFormat.GenericTypographic);
                res.Add((new RectangleF(x, y + args.OffsetText + dif, m.Width, m.Height), t));
                x += m.Width;
            }
        }
        return new MeasuredIconString(res);
    }

    public static void DrawStringWithIcons(this Graphics gr, float x, float y, List<IconStringToken> text, IconStringArgs args,IEmbeddedIcons bank)
    {
        var measured = gr.MeasureIconString(text, args,bank);
        gr.DrawStringWithIcons(x, y, measured, args,bank);
    }
    
    public static void DrawStringWithIcons(this Graphics gr, float x, float y, MeasuredIconString measured, IconStringArgs args,IEmbeddedIcons bank)
    {
        var oldtr = gr.TextRenderingHint;
        gr.TextRenderingHint = args.TextRenderingHint;
        var br = new SolidBrush(args.ForeColor);
        var iconcolor = args.IconColor.FinalColor(args.ForeColor);
        foreach (var m in measured.MeasuredToken)
        {
            var r = m.Item1;
            var (u, cad) = m.Item2;
            if (u)
            {
                bank.DrawTTIcon(gr, cad, iconcolor, r.X + x, r.Y + y, r.Height);
            }
            else
            {
                gr.DrawString(cad, args.Font, br, r.X + x, r.Y + y);
            }
        }
        gr.TextRenderingHint = oldtr;
    }

    public static void DrawStringWithIcons(this Graphics gr, float x, float y, string text, Font font, int scale, IEmbeddedIcons iconbank, Color forecolor, DuoToneColor iconcolor, TextRenderingHint textRenderingHint, int offseticon, int offsettext)
    {
        var args = new IconStringArgs
        {
            Font = font,
            Scale = scale,
            IconBank = iconbank,
            ForeColor = forecolor,
            IconColor = iconcolor,
            TextRenderingHint = textRenderingHint,
            OffsetIcon = offseticon,
            OffsetText = offsettext
        };
        gr.DrawStringWithIcons(x, y, text.SplitIconString(), args,iconbank);
    }

    public static PointF AlignOffset(this Control c,ContentAlignment alignment,  RectangleF textbounds)
    {
        
        var controlbounds = new RectangleF(c.Padding.Left, c.Padding.Top, c.Width - c.Padding.Horizontal, c.Height - c.Padding.Vertical);
        var r = textbounds.AlignOffset(alignment, controlbounds);
        return r;
    }
    
}