
using Engine;
using SkiaSharp;
using System.IO;

namespace Game.Logic.Classes
{
    public class Showing : Something
    {
        private SKBitmap bitmap;
        public Showing(Settings settingsHost, string Name) : base(settingsHost, Name) {
            if (Properties.Resources.ResourceManager.GetObject(Name) is byte[] bytes)
            {
                using (Stream stream = new MemoryStream(bytes)) { bitmap = SKBitmap.Decode(stream); }
            }
        }
        public override void Draw(SKCanvas canvas, object args)
        {
            SKRect rect1 = new SKRect(0,0,bitmap.Width, bitmap.Height);
            SKRect rect2 = rect;
            canvas.DrawBitmap(bitmap, PosXY);
        }
    }
}
