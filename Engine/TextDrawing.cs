using Engine;
using SkiaSharp;


namespace Game
{
    public static class TextDrawing
    {
        public static void TextStrokeHead(this SKCanvas canvas, Settings settings, string text, SKColor? color = null)
        {
            var f1 = settings.textHead;
            f1.Color = settings.lightColour;

            SKPaint f2 = f1.Clone();
            canvas.DrawText(text, canvas.DeviceClipBounds.Width / 2, 26, f1);
            f2.Style = SKPaintStyle.Stroke;
            f2.StrokeWidth = 1f;
            f2.Color = settings.backgroundColour;
            canvas.DrawText(text, canvas.DeviceClipBounds.Width / 2, 26, f2);
        }
    }
}
