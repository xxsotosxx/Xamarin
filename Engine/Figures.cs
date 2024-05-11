using SkiaSharp;


namespace Engine.Graphics
{
    public static class Figures
    {
        /// <summary>
        /// Рисует градиентную сферу в координатах xy цветом color
        /// </summary>
        public static void GradientSphere(SKCanvas canvas, in SKRect rect, in SKColor color, int light)
        {

            SKShaderTileMode tileMode = SKShaderTileMode.Decal;

            using (SKPaint paint = new SKPaint())
            {
                color.ToHsl(out var h, out var s, out var l);
                paint.Shader = SKShader.CreateRadialGradient(
                    new SKPoint(rect.MidX, rect.MidY),
                    rect.Width/2,
                    new SKColor[] { SKColor.FromHsl(h, 100, light, 255), color},
                    null,
                    tileMode);

                //paint.IsAntialias = true;
                canvas.DrawRect(rect, paint);
            }
        }
    }
}
