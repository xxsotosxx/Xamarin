using SkiaSharp;
using System;
using System.Collections.Generic;


namespace Engine
{
    public class JsonMap
    {
        public class ShowingObj
        {
            public int x { get; set; }
            public int y { get; set; }
            public string blockType { get; set; }
            public Showing obj;
            //public ShowingObj(Showing obj)
            //{
            //    this.obj = obj;
            //}
        }

        public SKSizeI BiomSize
        {
            get; set;
        }
        public List<ShowingObj> Biom { get; set; }
        public void AddToBiom(Showing shObj)
        {
            Biom.Add(new ShowingObj() { obj = shObj });
        }
    }

    public static class GameWorld
    {
        //public static ConcurrentBag<Something> objects = new ConcurrentBag<Something>();
        public static List<Something> objects = new List<Something>();
        public static JsonMap gameMap = new JsonMap();

        public static Dictionary<string, SKBitmap> ShowingImageLibrary= new Dictionary<string, SKBitmap>();
    }

    public class Settings
    {
        //ublic static Settings instance = new Settings();

        public const string worldMapFileName = "worldmap.json";
        public const string SaveLoadFileName = "saveload.json";

        public int КоличествоСпрайтовНаГлавнойОси = 60;
        
        private SKSize _spriteSize = SKSize.Empty;
        public SKSize spriteSize { get => _spriteSize; }
        public SKSizeI spriteSizeI { get => new SKSizeI((int)SpriteSize.Width, (int)SpriteSize.Height); }

        public int ОсновнойОттенокФона = 120;
        public int ОсновнойОттенокСвета = 60;
        public SKColor backgroundColour => SKColor.FromHsl(ОсновнойОттенокФона, 50, 30, 255);
        public SKColor lightColour => SKColor.FromHsl(ОсновнойОттенокСвета, 100, 90, 255);

        public readonly SKRect bounds = SKRect.Empty;

        public Settings(SKRect bounds)
        {
            //this.Width = Width; this.Height = Height;
            this.bounds = bounds;
        }

        public Settings() { }

        /// <summary>
        /// Функция/свойство, которая расчитывает средний размер спрайта (единичного объекта игрового мира) в зависимости от размера канвы/окна программы
        /// </summary>
        public SKSize SpriteSize { 
            get {
                _spriteSize = new SKSize(32,32);
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
