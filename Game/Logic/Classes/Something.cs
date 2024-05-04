using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;


using Game.Graphics;
using Game.Logic.Classes;

namespace Game.Logic
{
    /// <summary>
    /// Класс - существо, от которого будут унаследованы все другие.
    /// </summary>
    /// 
    public class Position
    {
        public int col;
        public int row;
    }

    public class Animated : Something
    {
        public const int FramesCount = 3;
        public double FrameNumber;
        public virtual void doAnimate(SKCanvas canvas, SKPaintSurfaceEventArgs args) {
            Draw(canvas, args);
            FrameNumber+=0.1;
            if (FrameNumber>=FramesCount) FrameNumber = 0;
        }
    }

    public class Something : I2DGraphicMember, IMapAction
    {
        public Position Pos;
        internal int _x;
        internal int _y;
        public int PosX {
            get => Pos.col * Engine.SpriteWidth;
            set { _x = value; }
        }
        public int PosY
        {
            get => Pos.row * Engine.SpriteHeight;
            set { _y = value; }
        }
        protected SKRect rect;
        protected MoveDirection moveDirection;
        protected float speed;
        //protected float distance;
        protected static readonly Random random = new Random();
        private static readonly SKColor color = SKColor.FromHsl(Settings.ОсновнойОттенокФона, 100, 20, 150);
        public Something() {
            moveDirection = (MoveDirection) random.Next(0, 4);
            speed = (float) random.Next(1, 10)/5;
//            distance = random.Next(100, 1000);
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
        public virtual void MoveTo(MoveDirection direction)
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
                //distance -= speed;
                //if (distance <= 0)
                //{
                //    distance = random.Next(100, 1000);
                //    moveDirection = (MoveDirection) random.Next(0, 4);
                //}
            } // Столкновение с иобъектами игрового мира 
            else
            {
                this.onCollision(null);
               // moveDirection = (MoveDirection)random.Next(0, 4);
            }
        }

        public virtual void onCollision(Action<Something, Something> param)
        {
            if (param == null)
            {
                moveDirection = (MoveDirection)random.Next(0, 4);
            }
        }
    }
}
