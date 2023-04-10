using System.Drawing;

namespace Rop.Winforms.DuotoneIcons.Helpers
{
    public static class RectangleHelper
    {
        public static RectangleF SetLeft(this RectangleF r, float left)
        {
            r.X = left;
            return r;
        }

        public static RectangleF SetTop(this RectangleF r, float top)
        {
            r.Y = top;
            return r;
        }

        public static RectangleF SetRight(this RectangleF r, float right)
        {
            r.X = right - r.Width;
            return r;
        }

        public static RectangleF SetBottom(this RectangleF r, float bottom)
        {
            r.Y = bottom - r.Height;
            return r;
        }

        public static RectangleF SetCenterX(this RectangleF r, float center)
        {

            r.X = r.X + center - r.Width / 2;
            return r;
        }

        public static RectangleF SetCenterY(this RectangleF r, float center)
        {
            r.Y = r.Y + center - r.Height / 2;
            return r;
        }

        public static float GetCenterX(this RectangleF r)
        {
            return r.X + r.Width / 2;
        }

        public static float GetCenterY(this RectangleF r)
        {
            return r.Y + r.Height / 2;
        }
        public static PointF AlignOffset(this RectangleF textbounds,ContentAlignment aligment,RectangleF externalbounds)
        {
            RectangleF r = new RectangleF();
            switch (aligment)
            {
                case ContentAlignment.BottomCenter:
                    r = textbounds.SetBottom(externalbounds.Bottom);
                    r = r.SetCenterX(externalbounds.GetCenterX());
                    break;
                case ContentAlignment.BottomLeft:
                    r = textbounds.SetBottom(externalbounds.Bottom);
                    r = r.SetLeft(externalbounds.Left);
                    break;
                case ContentAlignment.BottomRight:
                    r = textbounds.SetBottom(externalbounds.Bottom);
                    r = r.SetRight(externalbounds.Right);
                    break;
                case ContentAlignment.MiddleCenter:
                    r = textbounds.SetCenterX(externalbounds.GetCenterX());
                    r = r.SetCenterY(externalbounds.GetCenterY());
                    break;
                case ContentAlignment.MiddleLeft:
                    r = textbounds.SetCenterY(externalbounds.GetCenterY());
                    r = r.SetLeft(externalbounds.Left);
                    break;
                case ContentAlignment.MiddleRight:
                    r = textbounds.SetCenterY(externalbounds.GetCenterY());
                    r = r.SetRight(externalbounds.Right);
                    break;
                case ContentAlignment.TopCenter:
                    r = textbounds.SetTop(externalbounds.Top);
                    r = r.SetCenterX(externalbounds.GetCenterX());
                    break;
                case ContentAlignment.TopLeft:
                    r = textbounds.SetTop(externalbounds.Top);
                    r = r.SetLeft(externalbounds.Left);
                    break;
                case ContentAlignment.TopRight:
                    r = textbounds.SetTop(externalbounds.Top);
                    r = r.SetRight(externalbounds.Right);
                    break;
            }
            return r.Location;
        }
    }
}
