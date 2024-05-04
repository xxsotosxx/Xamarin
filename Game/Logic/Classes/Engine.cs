using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Logic.Classes
{
    public static class Engine
    {
        public const int SpriteWidth = 64;
        public const int SpriteHeight = 64;

        public static object[,] GameField = new object[100,100];  
        public static List<Something> SearchEnamy(Something seeker)
        {
            List<Something> result = new List<Something>();

            return result;
        } 
    }
}
