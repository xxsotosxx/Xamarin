using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;

namespace Game
{
    public static class Scene
    {
        internal static readonly List<object> objects = new List<object>();

        /// <summary>
        /// Процедура - обработчик на нажатие кнопки мыши или прикосновение к экрану на смартфоне/планшете
        /// </summary>
        public static void OnTouch(object sender, SKTouchEventArgs e)
        {
            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    //var color = new SKColor((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));
                    //points.Add(new Tuple<SKPoint, SKColor>(e.Location, color));
                    //gameField.InvalidateSurface();
                    break;
                case SKTouchAction.Moved:
                    break;
                case SKTouchAction.Released:
                    break;
                case SKTouchAction.Cancelled:
                    break;
            }
            if (e.InContact) ((SKCanvasView)sender).InvalidateSurface();
            e.Handled = true;
        }

        /// <summary>
        /// Главная сцена отрисовки игрового мира
        /// </summary>
        public static void Paint(SKCanvas canvas)
        {
            // Заливаем весь фон канвы заданным цветом (иначе говоря - очищаем фон)
            canvas.Clear(SKColors.Goldenrod);


            //Отрисовываем все объекты игрового мира
            foreach (var item in objects) {
                //item.Draw();
            }

            // Выводим статистику по игре
            canvas.TextStrokeHead($"Человеческая популяция: {objects.Count}") ;

        }
    }
}
