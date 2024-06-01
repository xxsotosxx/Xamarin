using SkiaSharp;
using System.IO;

namespace Engine
{
    public class Showing : Something
    {
        private SKBitmap bitmap;
        public Showing(object Bitmap, Settings settingsHost, string Name) : base(settingsHost, Name)
        {
            if (Bitmap is byte[] bytes)
            {
                using (Stream stream = new MemoryStream(bytes)) { bitmap = SKBitmap.Decode(stream); }
            }
        }
        public override void Draw(SKCanvas canvas, object args)
        {
            SKRect rect1 = new SKRect(0, 0, bitmap.Width, bitmap.Height);
            SKRect rect2 = rect;
            canvas.DrawBitmap(bitmap, rect);
        }
    }
}
