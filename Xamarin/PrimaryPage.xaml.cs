using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game;

namespace Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrimaryPage : ContentPage
    {
        public PrimaryPage()
        {
            InitializeComponent();

            gameField.Touch += Scene.OnTouch;
            gameField.EnableTouchEvents = true;
        }

    }
    public class GameField : SKCanvasView {

        public GameField() { 
//            Scene.Init(this); 
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs args)
        {
            var canvas = args.Surface.Canvas;
            
            // Масштабирование канвы под разрешение экрана
            var scale = args.Info.Width / (float)Width;
            canvas.Scale(scale);

            Scene.Paint(canvas, args);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            Scene.Rearange(this);
        }
    }
}
