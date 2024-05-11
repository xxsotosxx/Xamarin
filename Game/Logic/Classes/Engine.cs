using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Game.Logic.Classes
{
    public static class Engine
    {
        //public static Engine eng;
        public const int SpriteWidth = 64;
        public const int SpriteHeight = 64;

        public static object[,] GameField = new object[100,100];  

        public static Dictionary<string, SKBitmap> AllImages = new Dictionary<string, SKBitmap>();
        public static List<Something> SearchEnamy(Something seeker)
        {
            List<Something> result = new List<Something>();

            return result;
        } 
        static Engine()
        {
            SKBitmap bitmap;

            //Загружаем картинки котов
            foreach (var item in new List<string> 
             { "CatLeftStep1","CatLeftStep2","CatLeftStep3"
              ,"CatRightStep1","CatRightStep2","CatRightStep3"
            })
            {
                if (Properties.Resources.ResourceManager.GetObject(item) is byte[] bytes)
                {
                    using (Stream stream = new MemoryStream(bytes)) { bitmap = SKBitmap.Decode(stream); }
                    AllImages[item] = bitmap;
                }

            }

        }

    }

}
