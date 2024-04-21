using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;


using Game.Graphics;

namespace Game.Logic
{
    /// <summary>
    /// Класс - существо, от которого будут унаследованы все другие.
    /// </summary>
    internal class Something : I2DGraphicMember
    {
        protected SKRect rect;
        protected MoveDirection moveDirection;
        protected float speed;
        protected float distance;
        protected static readonly Random random = new Random();
        private static readonly SKColor color = SKColor.FromHsl(Settings.ОсновнойОттенокФона, 100, 20, 150);
        public Something() {
            moveDirection = (MoveDirection) random.Next(0, 4);
            speed = (float) random.Next(1, 10)/5;
            distance = random.Next(100, 1000);
            rect.Size = Settings.SpriteSize;
        }
        public virtual void Draw(SKCanvas canvas, SKPaintSurfaceEventArgs args)
        {
            Figures.GradientSphere(canvas, rect, color, 80);
#if DEBUG_SPRITES
            using (SKPaint paint = new SKPaint())
            {
                paint.Color = SKColors.Black;
                paint.IsStroke = true;
                canvas.DrawRect(rect, paint);
            }
#endif
        }

        public void Animate() => MoveTo(moveDirection);
        public void SetXY(SKPoint point) => rect.Offset(point);
        private void MoveTo(MoveDirection direction)
        {
            float X = 0; 
            float Y = 0;
            switch (direction)
            {
                case MoveDirection.Вправо: X=speed;break;
                case MoveDirection.Влево:  X=-speed;break;
                case MoveDirection.Вверх:  Y=-speed;break;
                case MoveDirection.Вниз:   Y=speed;break;
            }
            var nr = rect;
            nr.Offset(X, Y);
            if (nr.Left >= 0 && nr.Top >= 0 && nr.Right < Scene.mainCanvasView.Width && nr.Bottom < Scene.mainCanvasView.Height)
            {
                rect = nr;
                distance -= speed;
                if (distance <= 0)
                {
                    distance = random.Next(100, 1000);
                    moveDirection = (MoveDirection) random.Next(0, 4);
                }
            } else moveDirection = (MoveDirection) random.Next(0, 4);
        }
    }
}
