using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
//using System.Text.Json;

using SkiaSharp;
using System.Collections.Generic;


namespace Engine
{
    public static class BitmapExtender
    {
        public static readonly SKBitmap Empty = new SKBitmap(0, 0, SKColorType.Unknown, 0);
        public static SKBitmap LoadFromFile(this SKBitmap source, string FileName)
        {
            SKBitmap bitmap;
            using (FileStream stream = new FileStream(FileName, FileMode.Open)) bitmap = SKBitmap.Decode(stream);
            return bitmap;
        }

        public static Dictionary<string, SKBitmap> Cutter(this SKBitmap source, string libAlias, int blockWidth = 8, int blockHeight = 8)
        {
            int bWidth = source.Width / blockWidth;
            int bHeight = source.Height / blockHeight;

            var res = new Dictionary<string, SKBitmap>();

            for (int row = 0; row < blockHeight; row++)
            {
                for (int col = 0; col < blockWidth; col++)
                {
                    SKRectI rect = SKRectI.Create(bWidth, bHeight);
                    rect.Offset(col * bWidth, row * bHeight);
                    SKBitmap spriteOrigin = new SKBitmap(bWidth, bHeight);
                    if (!source.ExtractSubset(spriteOrigin, rect)) throw new Exception("Не удалось порезать картинку на части.");
                    //TODO: Протестировать на корректность.
                    var sprite = spriteOrigin.Resize(source.Info, SKFilterQuality.High);
                    res.Add($"{libAlias}_{col}_{row}", sprite);
                }
            }
            return res;
        }
    }

    public static class Scene
    {
        public static SKColor backgroundColor = SKColors.Pink;
        public static void Paint(SKCanvas canvas)
        {
            // Заливаем весь фон канвы заданным цветом (иначе говоря - очищаем фон)
            canvas.Clear(backgroundColor);

            //Отрисовываем фон игрового мира(карту)
            for (int i = 0; i < GameWorld.gameMap.Biom?.Count; i++)
            {
                GameWorld.gameMap.Biom[i].obj.Draw(canvas);
#if DEBUG_GRAPHICS
                SKPaint p = new SKPaint()
                {
                    Color = SKColors.Yellow,
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 3
                };
                canvas.DrawRect(GameWorld.gameMap.Biom[i].obj.rect, p);
#endif
            }

            //Отрисовываем все объекты игрового мира
            //var LiveObjects = GameWorld.objects.Where(i => i.isInGame = true);
            foreach (var item in GameWorld.objects)
            {
                if (item.isInGame)
                {

                    if (item is IAnimated animatedItem) animatedItem.doAnimate(canvas);
                    else item.Draw(canvas);
                }

#if DEBUG_GRAPHICS
                SKPaint p = new SKPaint()
                {
                    Color = SKColors.Black,
                    Style = SKPaintStyle.Stroke,
                };
                canvas.DrawRect(item.rect, p);
                //canvas.TextStrokeHead(globalSettings, $"Популяция существ: {GameWorld.objects.Count}");
#endif
            }
            // Выводим статистику по игре
        }

        /// <summary>
        /// Главный цикл анимации игрового мира.
        /// </summary>

        public static void AnimatiomLoopTimer()
        {
            foreach (var item in GameWorld.objects)
                if (item.isInGame)
                    item.Animate();
        }


        //        private static void AnimatiomLoopThread()
        //        {
        //            double maxFPS = 60;
        //            double minFramePeriodMsec = 1000.0 / maxFPS;

        //            Stopwatch stopwatch = Stopwatch.StartNew();
        //            while (true)
        //            {
        //                foreach (var item in GameWorld.objects)
        //                    if (item.isInGame)
        //                        item.Animate();

        //                double msToWait = minFramePeriodMsec - stopwatch.ElapsedMilliseconds;
        //                if (msToWait > 0) Thread.Sleep(1); //Thread.Sleep((int)msToWait);
        //                else
        //                {
        //                    mainCanvasView.InvalidateSurface();
        //                    stopwatch.Restart();
        //                }
        //            }
        //            return true;
        //        }
    }
}
