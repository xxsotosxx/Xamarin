using SkiaSharp.Views.Forms;
using SkiaSharp;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game;

namespace Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrimaryPage : ContentPage
    {
        private readonly List<Tuple<SKPoint, SKColor>> points = new List<Tuple<SKPoint, SKColor>>();
        private readonly Random random = new Random();

        public PrimaryPage()
        {
            InitializeComponent();

            gameField.Touch += Scene.OnTouch;
            gameField.EnableTouchEvents = true;
        }

    }
    public class GameField : SKCanvasView {

        public GameField()
        {
            Scene.mainCanvas = this;
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs args)
        {
            var canvas = args.Surface.Canvas;
            
            // Масштабирование канвы под разрешение экрана
            var scale = args.Info.Width / (float)Width;
            canvas.Scale(scale);

            Scene.Paint(canvas, args);
        }
    }
}
