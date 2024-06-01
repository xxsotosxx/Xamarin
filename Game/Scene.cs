using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Text.Json;
using System.Collections.Generic;


using SkiaSharp;
using SkiaSharp.Views.Forms;


using Game.Logic.Classes;
using Engine;
using System.Linq;


namespace Game
{

    public static class Scene
    {
        public static string labirint = "{{\"x\" : \"64\", \"y\" : \"64\"}}"; //, {'x' = '96', 'y' = '64' }, {'x' = '64' , 'y' = '96'}}";

        /// <summary>
        /// В этой коллекции (потокобезопасном списке) хранятся все "живые" объекты игрового мира
        /// </summary>
        internal static SKCanvasView mainCanvasView;
//        internal static SKBitmap bitmap = null;
        internal static Settings globalSettings = null;

        internal class smartPoint
        {
            int x; int y;
        }
        /// <summary>
        /// Создание "живых" существ при старте программы. В дальнейшем будем загружать из файла сохраненных объектов
        /// </summary>
        public static void Init(SKCanvasView canvasView) {

            //if (Properties.Resources.ResourceManager.GetObject("moon") is byte[] bytes)
            //{
            //    using (Stream stream = new MemoryStream(bytes)) { bitmap = SKBitmap.Decode(stream); }
            //} 

            string data = string.Empty;
            try { 
                data = File.ReadAllText(Settings.worldMapFileName);
                GameWorld.map = JsonSerializer.Deserialize<List<Showing>>(data);
            }
            catch {}

            mainCanvasView = canvasView;
            globalSettings = new Settings(new SKRect(0,0, (float)canvasView.Width, (float)canvasView.Height) /*canvasView.Width, canvasView.Height*/);

            for (int i = 0; i < 10; i++)
            {
                Something obj = new Cat(globalSettings);//Something();
                obj.SetXY(new SKPoint(new Random().Next(0, 400), new Random().Next(0, 200)));
                GameWorld.objects.Add(obj);
            }

            for (int i = 0; i < 10; i++)
            {
                Something obj = new Dog(globalSettings);//Something();
                obj.SetXY(new SKPoint(new Random().Next(0, 400), new Random().Next(0, 200)));
                GameWorld.objects.Add(obj);
            }

            //Showing wall1 = new Showing(globalSettings, "Wall1");
            //worldmap.Add(wall1);

            var bitmap = Properties.Resources.ResourceManager.GetObject("Wall1");

            float y = (float)globalSettings.bounds.Height - globalSettings.SpriteSize.Height;
            for (int i = 0; i < globalSettings.bounds.Width / globalSettings.SpriteSize.Width; i++)
            {
                Showing wallU = new Showing(bitmap, globalSettings, "Wall1");
                wallU.rect = SKRect.Create(i* globalSettings.SpriteSize.Width, 0, globalSettings.SpriteSize.Width, globalSettings.SpriteSize.Height);
                Showing wallD = new Showing(bitmap, globalSettings, "Wall1");
                wallD.rect = SKRect.Create(i * globalSettings.SpriteSize.Width, y, globalSettings.SpriteSize.Width, globalSettings.SpriteSize.Height);

                GameWorld.map.Add(wallU);
                GameWorld.map.Add(wallD);
            }
            float x = (float)globalSettings.bounds.Width - globalSettings.SpriteSize.Width;
            for (int i = 1; i < globalSettings.bounds.Height/ globalSettings.SpriteSize.Height - 1; i++)
            {
                Showing wallU = new Showing(bitmap, globalSettings, "Wall1");
                wallU.rect = SKRect.Create(0, i * globalSettings.SpriteSize.Height, globalSettings.SpriteSize.Width, globalSettings.SpriteSize.Height);
                Showing wallD = new Showing(bitmap, globalSettings, "Wall1");
                wallD.rect = SKRect.Create(x, i * globalSettings.SpriteSize.Height, globalSettings.SpriteSize.Width, globalSettings.SpriteSize.Height);

                GameWorld.map.Add(wallU);
                GameWorld.map.Add(wallD);
            }

            List<smartPoint> l = JsonSerializer.Deserialize<List<smartPoint>>(labirint);
            foreach (var item in l)
            {

            }

            var renderThread = new Timer((p) => { AnimatiomLoopTimer(); });
            renderThread.Change(0, 10);

            //var renderThread = new Thread(new ThreadStart(AnimatiomLoop));
            //renderThread.Start();
        }

        public static void Rearange(SKCanvasView canvasView)
        {
            if (GameWorld.objects.Count == 0) Init(canvasView);
            //TODO: Если размер канвы изменился - нужно пересчитать размеры спрайтов
        }


        /// <summary>
        /// Процедура - обработчик на нажатие кнопки мыши или прикосновение к экрану на смартфоне/планшете
        /// </summary>
        public static void OnTouch(object sender, SKTouchEventArgs e)
        {
            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    var obj = new Cat(globalSettings);
                    if (obj.SetXY(new SKPoint(e.Location.X - globalSettings.SpriteSize.Width / 2,e.Location.Y - globalSettings.SpriteSize.Height / 2)))
                        GameWorld.objects.Add(obj);
                    break;
                case SKTouchAction.Moved:
                    break;
                case SKTouchAction.Released:
                    break;
                case SKTouchAction.Cancelled:
                    break;
            }
            //if (e.InContact) ((SKCanvasView)sender).InvalidateSurface();
            e.Handled = true;
        }

        /// <summary>
        /// Главная процедура отрисовки сцены игрового мира
        /// </summary>
        public static void Paint(SKCanvas canvas, SKPaintSurfaceEventArgs args)
        {
            // Заливаем весь фон канвы заданным цветом (иначе говоря - очищаем фон)
            canvas.Clear(globalSettings.backgroundColour);

            //Отрисовываем фон игрового мира(карту)
            for(int i = 0; i < GameWorld.map.Count; i++) {
                GameWorld.map[i].Draw(canvas, args);
#if DEBUG_GRAPHICS
                SKPaint p = new SKPaint()
                {
                    Color = SKColors.Yellow,
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 3
                };
                canvas.DrawRect(GameWorld.map[i].rect, p);
#endif
            }

            //Отрисовываем все объекты игрового мира
            //var LiveObjects = GameWorld.objects.Where(i => i.isInGame = true);
            foreach (var item in GameWorld.objects)
            {
                if (item.isInGame)
                {

                    if (item is IAnimated animatedItem) animatedItem.doAnimate(canvas, args);
                    else item.Draw(canvas, args);
                }

#if DEBUG_GRAPHICS
                SKPaint p = new SKPaint()
                {
                    Color = SKColors.Black,
                    Style = SKPaintStyle.Stroke,
                };
            canvas.DrawRect(item.rect,p);
            canvas.TextStrokeHead(globalSettings, $"Популяция существ: {GameWorld.objects.Count}") ;
#endif
            }
            // Выводим статистику по игре
        }

        /// <summary>
        /// Главный цикл анимации игрового мира.
        /// </summary>

        private static void AnimatiomLoopTimer()
        {
            foreach (var item in GameWorld.objects)
                if (item.isInGame)
                    item.Animate();

            mainCanvasView.InvalidateSurface();
        }


        private static void AnimatiomLoopThread()
        {
            double maxFPS = 60;
            double minFramePeriodMsec = 1000.0 / maxFPS;

            Stopwatch stopwatch = Stopwatch.StartNew();
            while (true)
            {
                foreach (var item in GameWorld.objects)
                    if (item.isInGame)
                        item.Animate();

                double msToWait = minFramePeriodMsec - stopwatch.ElapsedMilliseconds;
                if (msToWait > 0) Thread.Sleep(1); //Thread.Sleep((int)msToWait);
                else
                {
                    mainCanvasView.InvalidateSurface();
                    stopwatch.Restart();
                }
            }
        }
    }
}
