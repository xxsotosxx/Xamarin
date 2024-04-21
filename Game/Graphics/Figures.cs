using SkiaSharp;
using SkiaSharp.Views.Forms;


namespace Game.Graphics
{
    internal static class Figures
    {
        /// <summary>
        /// Рисует градиентную сферу в координатах xy цветом color
        /// </summary>
        internal static void GradientSphere(SKCanvas canvas, in SKRect rect, in SKColor color)
        {

            SKShaderTileMode tileMode = SKShaderTileMode.Decal;

            using (SKPaint paint = new SKPaint())
            {
                paint.Shader = SKShader.CreateRadialGradient(
                    new SKPoint(rect.MidX, rect.MidY),
                    rect.Width/2,
                    new SKColor[] { color, SKColor.FromHsl(360f / 7, 100, 50, 0) },
                    null,
                    tileMode);

                //paint.IsAntialias = true;
                canvas.DrawRect(rect, paint);
            }
        }
    }
}
