using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Rop.Winforms.DuotoneIcons.Helpers;

public class MeasuredIconString
{
    public IReadOnlyList<(RectangleF, IconStringToken)> MeasuredToken { get; }
    public RectangleF Bounds { get; }

    public RectangleF BoundsZero()
    {
        var b=Bounds;
        return new RectangleF(PointF.Empty, new SizeF(b.Width+b.Left, b.Height+b.Top));
    }
    public MeasuredIconString(List<(RectangleF, IconStringToken)> measuredToken)
    {
        MeasuredToken = measuredToken;
        if (measuredToken.Count > 0)
        {
            var maxb = measuredToken.Max(x => x.Item1.Bottom);
            var minb = measuredToken.Min(x => x.Item1.Top);
            var maxr = measuredToken.Max(x => x.Item1.Right);
            var minr = measuredToken.Min(x => x.Item1.Left);
            Bounds = new RectangleF(minr, minb, maxr - minr, maxb - minb);
        }
    }
}