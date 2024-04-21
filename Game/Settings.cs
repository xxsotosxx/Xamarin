using SkiaSharp;
using System;

namespace Game
{
    public static class Settings
    {
        public const int КоличествоСпрайтовНаГлавнойОси = 50;
        private static SKSize _spriteSize = SKSize.Empty;

        public static int ОсновнойОттенокФона = 120;
        public static int ОсновнойОттенокСвета = 60;
        public static SKColor backroundColour = SKColor.FromHsl(ОсновнойОттенокФона, 50, 30, 255);
        public static SKColor lightColour = SKColor.FromHsl(ОсновнойОттенокСвета, 100, 90, 255);

        /// <summary>
        /// Функция/свойство, которая расчитывает средний размер спрайта (единичного объекта игрового мира) в зависимости от размера канвы/окна программы
        /// </summary>
        public static SKSize SpriteSize { 
            get {
                if (_spriteSize == SKSize.Empty)
                {
                    var widh = Scene.mainCanvasView.Width;
                    var height = Scene.mainCanvasView.Width;
                    _spriteSize = new SKSize();
                    _spriteSize.Width = widh >= height
                        ? Convert.ToInt64(widh) / КоличествоСпрайтовНаГлавнойОси
                        : Convert.ToInt64(height) / КоличествоСпрайтовНаГлавнойОси;
                    _spriteSize.Height = _spriteSize.Width;
                }
                return _spriteSize;
            }
        }

        //Шрифт, которым будуть выводиться акцентирующие надписи и заголовки
        public static SKPaint textHead = new SKPaint()
        {
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            TextSize = 24,
            TextAlign = SKTextAlign.Center,
            Typeface = SKTypeface.FromFamilyName( "Arial", 100, 50, SKFontStyleSlant.Upright)
        };
    }
}
