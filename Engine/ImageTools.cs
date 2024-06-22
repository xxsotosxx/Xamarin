
using SkiaSharp;
using System;

namespace Engine
{
    public class AllRotatedBitmaps
    {
        private SKBitmap[] allRotated = Array.Empty<SKBitmap>();

        public SKBitmap this[MoveDirection key]
        {
            get { return allRotated[(int)key]; }
        }
        public AllRotatedBitmaps(SKBitmap src)
        {
            int count = (int)MoveDirection.Влево + 1;
            allRotated = new SKBitmap[count];
            for (int i = 0; i< count; i++)
            {
                var img = src.Rotate((MoveDirection)i);
                allRotated[i] = img;
            }
        }
    }

    public static class ImageTools
    {
        public static AllRotatedBitmaps AllRotated(this SKBitmap source) => new AllRotatedBitmaps(source);

        public static SKBitmap Rotate(this SKBitmap source, float angle)
        {
            double radians = Math.PI * angle / 180;
            float sine = (float)Math.Abs(Math.Sin(radians));
            float cosine = (float)Math.Abs(Math.Cos(radians));
            int originalWidth = source.Width;
            int originalHeight = source.Height;
            int rotatedWidth = (int)(cosine * originalWidth + sine * originalHeight);
            int rotatedHeight = (int)(cosine * originalHeight + sine * originalWidth);

            var rotatedBitmap = new SKBitmap(rotatedWidth, rotatedHeight);

            using (var surface = new SKCanvas(rotatedBitmap))
            {
                surface.Clear();
                surface.Translate(rotatedWidth / 2, rotatedHeight / 2);
                surface.RotateDegrees((float)angle);
                surface.Translate(-originalWidth / 2, -originalHeight / 2);
                surface.DrawBitmap(source, new SKPoint());
            }
            return rotatedBitmap;
        }
        public static SKBitmap Rotate(this SKBitmap source, MoveDirection angle)
        {
            float anglef = 0.0f;
            switch (angle)
            {
                case MoveDirection.Вверх: break;
                case MoveDirection.Вниз: anglef = 180.0f; break;
                case MoveDirection.Влево: anglef = 270.0f; break;
                case MoveDirection.Вправо: anglef = 90.0f; break;
            }
            return source.Rotate(anglef);
        }
    }
}
