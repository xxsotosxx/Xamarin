﻿using SkiaSharp;
using System;

namespace Engine
{
    public class Settings
    {
        public int КоличествоСпрайтовНаГлавнойОси = 50;
        
        private SKSize _spriteSize = SKSize.Empty;

        public int ОсновнойОттенокФона = 120;
        public int ОсновнойОттенокСвета = 60;
        public SKColor backgroundColour => SKColor.FromHsl(ОсновнойОттенокФона, 50, 30, 255);
        public SKColor lightColour => SKColor.FromHsl(ОсновнойОттенокСвета, 100, 90, 255);

        //Размеры игровой сцены
        public readonly double Width;
        public readonly double Height;

        public Settings(double Width, double Height)
        {
            this.Width = Width; this.Height = Height;
        }


        /// <summary>
        /// Функция/свойство, которая расчитывает средний размер спрайта (единичного объекта игрового мира) в зависимости от размера канвы/окна программы
        /// </summary>
        public SKSize SpriteSize { 
            get {
                if (_spriteSize == SKSize.Empty)
                {
                    var lwidh = Width;
                    var lheight = Width;
                    _spriteSize = new SKSize();
                    _spriteSize.Width = lwidh >= lheight
                        ? Convert.ToInt64(lwidh) / КоличествоСпрайтовНаГлавнойОси
                        : Convert.ToInt64(lheight) / КоличествоСпрайтовНаГлавнойОси;
                    _spriteSize.Height = _spriteSize.Width;
                }
                return _spriteSize;
            }
        }

        //Шрифт, которым будуть выводиться акцентирующие надписи и заголовки
        public SKPaint textHead = new SKPaint()
        {
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            TextSize = 24,
            TextAlign = SKTextAlign.Center,
            Typeface = SKTypeface.FromFamilyName( "Arial", 100, 50, SKFontStyleSlant.Upright)
        };
    }
}