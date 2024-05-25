using System;
using SkiaSharp;


using Engine.Graphics;


namespace Engine
{
    public class Position
    {
        public int col;
        public int row;
    }

    public class Animated : Something 
    {
        public const int FramesCount = 3;
        public double FrameNumber;

        public Animated(Settings settingsHost) : base(settingsHost, "") { }

        public virtual void doAnimate(SKCanvas canvas, /*SKPaintSurfaceEventArgs*/ object args) {
            Draw(canvas, args);
            FrameNumber+=0.1;
            if (FrameNumber>=FramesCount) FrameNumber = 0;
        }
    }

    /// <summary>
    /// Класс - существо, от которого будут унаследованы все другие.
    /// </summary>
    public class Something : I2DGraphicMember, IMapAction
    {
        protected Settings sHost;
        protected MoveDirection moveDirection;
        protected float speed;
        protected static readonly Random random = new Random();

        public string Name;

        public Position Pos = new Position();
        public SKPoint PosXY;

        public float PosX {
            get => Pos.col * sHost.SpriteSize.Width;
            set { PosXY.X = value; }
        }
        public float PosY
        {
            get => Pos.row * sHost.SpriteSize.Height;
            set { PosXY.X = value; }
        }

        public SKRect rect;
        private SKColor color => SKColor.FromHsl(sHost.ОсновнойОттенокФона, 100, 20, 150 );

        public bool isCollision(SKRect nr)
        {
            //TODO: Требуется оптимизация
            return !(nr.Left >= 0 && nr.Top >= 0 && nr.Right < sHost.Width && nr.Bottom < sHost.Height);
        }

        public Something(Settings settingsHost, string Name) {
            this.Name = Name;

            sHost = settingsHost;
            moveDirection = (MoveDirection) random.Next(0, 4);
            speed = (float) random.Next(1, 10)/5;
            rect.Size = sHost.SpriteSize;
        }
        public virtual void Draw(SKCanvas canvas, object args)
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
        public bool SetXY(SKPoint point)
        {
            SKRect tmpRect = rect;
            tmpRect.Offset(point);
            if (!isCollision(tmpRect))
            {
                rect.Offset(point);
                return true;
            } else return false;
        }
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
            if (!isCollision(nr))
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
                moveDirection = (MoveDirection)random.Next(1, 5);
            }
        }
    }
}
