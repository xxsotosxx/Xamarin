using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading;

using Game.Logic;
using Game.Logic.Classes;
using Game.Graphics;

namespace Game
{
    public static class Scene
    {
        /// <summary>
        /// В этой коллекции (потокобезопасном списке) хранятся все "живые" объекты игрового мира
        /// </summary>
        internal static readonly ConcurrentBag<Something> objects = new ConcurrentBag<Something>();
        internal static SKCanvasView mainCanvasView;
        internal static SKBitmap bitmap = null;
        /// <summary>
        /// Создание "живых" существ при старте программы. В дальнейшем будем загружать из файла сохраненных объектов
        /// </summary>
        public static void Init(SKCanvasView canvasView) {

            if (Properties.Resources.ResourceManager.GetObject("moon") is byte[] bytes)
            {
                using (Stream stream = new MemoryStream(bytes)) { bitmap = SKBitmap.Decode(stream); }
            } 

            mainCanvasView = canvasView;
            for (int i = 0; i < 10; i++)
            {
                Something obj = new Cat();//Something();
                obj.SetXY(new SKPoint(new Random().Next(0, 400), new Random().Next(0, 200)));
                objects.Add(obj);
            }

            var renderThread = new Thread(new ThreadStart(AnimatiomLoop));
            renderThread.Start();
        }

        public static void Rearange(SKCanvasView canvasView)
        {
            if (objects.Count == 0) Init(canvasView);
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
                    var obj = new Something();
                    obj.SetXY(new SKPoint(e.Location.X - Settings.SpriteSize.Width / 2,e.Location.Y - Settings.SpriteSize.Height / 2));
                    objects.Add(obj);
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
            canvas.Clear(Settings.backroundColour);

            //Отрисовываем фон игрового мира (карту)
            if (bitmap != null)
            {
                using (SKPaint paint = new SKPaint())
                {
                    paint.BlendMode = SKBlendMode.Multiply;
                    canvas.DrawBitmap(bitmap, (float)mainCanvasView.Width / 2 - bitmap.Width / 2, (float)mainCanvasView.Height / 2 - bitmap.Height / 2, paint);
                }
            }
            //Отрисовываем все объекты игрового мира
            foreach (var item in objects)
            {
                if (item is IAnimated animatedItem) animatedItem.doAnimate(canvas, args);
                else item.Draw(canvas, args);
            }
            // Выводим статистику по игре
            canvas.TextStrokeHead($"Популяция существ: {objects.Count}") ;
        }

        /// <summary>
        /// Главный цикл анимации игрового мира.
        /// </summary>
        private static void AnimatiomLoop()
        {
            double maxFPS = 60;
            double minFramePeriodMsec = 1000.0 / maxFPS;

            Stopwatch stopwatch = Stopwatch.StartNew();
            while (true)
            {
                foreach (var item in objects) item.Animate();

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
