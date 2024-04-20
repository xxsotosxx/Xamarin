using SkiaSharp;
using SkiaSharp.Views.Forms;


namespace Game.Graphics
{
    internal static class Figures
    {
        /// <summary>
        /// Рисует градиентную сферу в координатах xy цветом color
        /// </summary>
        internal static void GradientSphere(SKCanvas canvas, SKPaintSurfaceEventArgs args, SKPoint xy, SKColor color)
        {
            SKImageInfo info = args.Info;

            SKShaderTileMode tileMode = SKShaderTileMode.Decal;

            using (SKPaint paint = new SKPaint())
            {
                SKRect r = new SKRect { Left = xy.X, Top = xy.Y, Size = new SKSizeI { Width = 100, Height = 100 } };

                paint.Shader = SKShader.CreateRadialGradient(
                    new SKPoint(r.MidX, r.MidY),
                    30,
                    new SKColor[] { color, SKColor.FromHsl(360f / 7, 100, 50, 0) },
                    null,
                    tileMode);

                paint.IsAntialias = true;
                canvas.DrawRect(r, paint);
            }
        }
    }
}
