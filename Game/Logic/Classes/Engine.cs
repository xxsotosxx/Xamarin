using SkiaSharp;
using System.Collections.Generic;
using System.IO;


using Engine;


namespace Game.Logic.Classes
{
    public static class GamePrepare
    {
        //public static Engine eng;
        public const int SpriteWidth = 64;
        public const int SpriteHeight = 64;

        public static object[,] GameField = new object[100,100];  

        public static Dictionary<string, SKBitmap> AllImages = new Dictionary<string, SKBitmap>();
        public static List<Something> SearchEnemy(Something seeker)
        {
            List<Something> result = new List<Something>();

            return result;
        } 
        static GamePrepare()
        {
            SKBitmap bitmap;

            //Загружаем картинки котов
            //TODO: Переделать! Так делать нельзя!!! 
            foreach (var item in new List<string> 
             { "CatLeftStep1","CatLeftStep2","CatLeftStep3"
              ,"CatRightStep1","CatRightStep2","CatRightStep3"
              ,"CatUpStep1","CatUpStep2"
              ,"CatDownStep1","CatDownStep2"

              ,"DogLeftStep1","DogLeftStep2"
              ,"DogRightStep1","DogRightStep2"
              ,"DogUpStep1","DogUpStep2"
              ,"DogDownStep1","DogDownStep2"
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
