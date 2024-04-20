using SkiaSharp;
using System;

namespace Game
{
    public static class Settings
    {
        public static SKPaint textHead = new SKPaint()
        {
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            TextSize = 22,
            TextAlign = SKTextAlign.Center,
            Typeface = SKTypeface.FromFamilyName( "Arial", 50, 10, SKFontStyleSlant.Upright)
        };
    }
}
