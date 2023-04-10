using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;

namespace Rop.Winforms.Icons.Helpers;

public static class EmbeddedFontHelper
{
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
    public static SizeF MeasureIconString(this Graphics g, string text, Font font, Font iconfont, IEmbeddedIcons iconbank)
    {
        float w = 0;
        float h = 0;
        foreach (var (u, cad) in text.SplitIconString())
        {
            if (u)
            {
                var cad2 = iconbank?.GetChar(cad) ?? cad;
                var m = g.MeasureString(cad2, iconfont, PointF.Empty, StringFormat.GenericTypographic);
                w += m.Width;
                if (h < m.Height) h = m.Height;
            }
            else
            {
                var m = g.MeasureString(cad, font, PointF.Empty, StringFormat.GenericTypographic);
                w += m.Width;
                if (h < m.Height) h = m.Height;
            }
        }
        return new SizeF(w, h);
    }

    public static MeasuredIconString MeasureIconString(this Graphics gr, List<IconStringToken> text, IIconStringSizeArgs args)
    {
        float x = 0;
        float y = 0;
        var offset1 = gr.GetOffset(args.Font);
        var offset2 = gr.GetOffset(args.Iconfont);
        var dif = offset1 - offset2;
        if (offset2 > offset1) y -= dif;
        var res = new List<(RectangleF, IconStringToken)>();
        foreach (var t in text)
        {
            var (u, cad) = t;
            if (u)
            {
                var cad2 = args.IconBank?.GetChar(cad) ?? cad;
                var m = gr.MeasureString(cad2, args.Iconfont, PointF.Empty, StringFormat.GenericTypographic);
                res.Add((new RectangleF(x, y + dif + args.OffsetIcon, m.Width, m.Height), t));
                x += m.Width;
            }
            else
            {
                var m = gr.MeasureString(cad, args.Font, PointF.Empty, StringFormat.GenericTypographic);
                res.Add((new RectangleF(x, y + args.OffsetText, m.Width, m.Height), t));
                x += m.Width;
            }
        }
        return new MeasuredIconString(res);
    }
    public static void DrawStringWithIcons(this Graphics gr, float x, float y, List<IconStringToken> text, IconStringArgs args)
    {
        var measured = gr.MeasureIconString(text, args);
        gr.DrawStringWithIcons(x,y,measured,args);
    }
    public static void DrawStringWithIcons(this Graphics gr, float x, float y, MeasuredIconString measured, IconStringArgs args)
    {
        var br = new SolidBrush(args.ForeColor);
        var iconbr = args.IconColor == Color.Empty ? br : new SolidBrush(args.IconColor);
        var oldtr = gr.TextRenderingHint;
        gr.TextRenderingHint = args.TextRenderingHint;
        foreach (var m in measured.MeasuredToken)
        {
            var r = m.Item1;
            var (u, cad) = m.Item2;
            if (u)
            {
                var cad2 = args.IconBank?.GetChar(cad) ?? cad;
                gr.DrawString(cad2, args.Iconfont, iconbr, r.X + x, r.Y + y);
            }
            else
            {
                gr.DrawString(cad, args.Font, br, r.X + x, r.Y + y);
            }
        }
        gr.TextRenderingHint = oldtr;
    }



    public static void DrawStringWithIcons(this Graphics gr, float x, float y, string text, Font font, Font iconfont, IEmbeddedIcons iconbank, Color forecolor, Color iconcolor, TextRenderingHint textRenderingHint, int offseticon, int offsettext)
    {
        var args = new IconStringArgs
        {
            Font = font,
            Iconfont = iconfont,
            IconBank = iconbank,
            ForeColor = forecolor,
            IconColor = iconcolor,
            TextRenderingHint = textRenderingHint,
            OffsetIcon = offseticon,
            OffsetText = offsettext
        };
        gr.DrawStringWithIcons(x, y, text.SplitIconString(), args);
    }
}