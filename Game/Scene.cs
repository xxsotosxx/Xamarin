using Game.Logic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;


namespace Game
{
    public static class Scene
    {
        /// <summary>
        /// В этой коллекции (потокобезопасном списке) хранятся все "живые" объекты игрового мира
        /// </summary>
        internal static readonly ConcurrentBag<Something> objects = new ConcurrentBag<Something>();
        public static SKCanvasView mainCanvas;
        /// <summary>
        /// Создание "живых" существ при старте программы. В дальнейшем будем загружать из файла сохраненных объектов
        /// </summary>
        static Scene() {
            for (int i = 0; i < 10; i++)
            {
                Something obj = new Something();
                obj.SetXY(new SKPoint(new Random().Next(0, 400), new Random().Next(0, 200)));
                objects.Add(obj);
            }

            var renderThread = new Thread(new ThreadStart(AnimatiomLoop));
            renderThread.Start();

            //var anim = new Animation(_ => onValueCallback(_currStrokeDash));
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
                    obj.SetXY(new SKPoint(e.Location.X,e.Location.Y));
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
        /// Главная сцена отрисовки игрового мира
        /// </summary>
        public static void Paint(SKCanvas canvas, SKPaintSurfaceEventArgs args)
        {
            // Заливаем весь фон канвы заданным цветом (иначе говоря - очищаем фон)
            canvas.Clear(SKColors.Goldenrod);

            //Отрисовываем фон игрового мира (карту)
            //Scene.DrawSkyBox()

            //Отрисовываем все объекты игрового мира
            foreach (var item in objects) item.Draw(canvas, args);

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
                    mainCanvas.InvalidateSurface();
                    stopwatch.Restart();
                }
            }
        }
    }
}
