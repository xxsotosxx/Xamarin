using SkiaSharp;
using System;

namespace Game
{
    public static class Settings
    {
        public const int КоличествоСпрайтовНаГлавнойОси = 50;
        private static SKSize _spriteSize = SKSize.Empty;
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
            TextSize = 22,
            TextAlign = SKTextAlign.Center,
            Typeface = SKTypeface.FromFamilyName( "Arial", 50, 10, SKFontStyleSlant.Upright)
        };
    }
}
