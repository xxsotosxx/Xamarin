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

        public virtual void doAnimate(SKCanvas canvas) {
            Draw(canvas);
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
        public bool isInGame = true;

        public string Name;

        public Position Pos = new Position();
        //public SKPoint PosXY;

        //public float PosX {
        //    get => Pos.col * sHost.SpriteSize.Width;
        //    set { PosXY.X = value; }
        //}
        //public float PosY
        //{
        //    get => Pos.row * sHost.SpriteSize.Height;
        //    set { PosXY.X = value; }
        //}

        public enum CollisionType { ChangeDirection, Continue, Destroy }

        public virtual CollisionType GetCollisionType(Something targteObject)
        {
            return CollisionType.Destroy;
        }

        public enum ShowingColision { yes, no };

        public virtual ShowingColision ShowingColisionAnswer(Showing targetObject)
        {
            return ShowingColision.yes;
        }

        public SKRect rect;
        private SKColor color => SKColor.FromHsl(sHost.ОсновнойОттенокФона, 100, 20, 150 );

        public bool isCollision(SKRect targetRect)
        {
            //TODO: Требуется оптимизация
            //var isWorldCollision = !targetRect.IntersectsWithInclusive(sHost.bounds);
            var isWorldCollision = !(targetRect.Left >= 0 && targetRect.Top >= 0 && 
                targetRect.Right < sHost.bounds.Width && targetRect.Bottom < sHost.bounds.Height);

            if (isWorldCollision) return true;

            string thisName = GetType().Name;

            foreach (var showObj in GameWorld.gameMap.Biom)
            {
                if (targetRect.IntersectsWith(showObj.obj.rect))
                //if (showObj.rect.IntersectsWith(targetRect))
                {
                    switch (ShowingColisionAnswer(showObj.obj))
                    {
                        case ShowingColision.yes:
                            moveDirection = (MoveDirection)(((int)moveDirection + 1) % (int)MoveDirection.Влево);
                            return true;
                        case ShowingColision.no:
                            break;
                    }
                }
            }

            foreach (var item in GameWorld.objects)
            {
                string name = item.GetType().Name;
                if (!item.isInGame || thisName == name) continue;

                if (item.rect.IntersectsWith(targetRect))
                {
                    switch (GetCollisionType(item))
                    {
                        case CollisionType.Continue:
                            continue;
                        case CollisionType.Destroy:
                            item.isInGame = false;
                            //GameWorld.objects.Remove(item);
                            break;
                        case CollisionType.ChangeDirection:
                            moveDirection = (MoveDirection)(((int)moveDirection + 1) % (int)MoveDirection.Влево);
                            break;
                    }
                }
            }
            return isWorldCollision;
        }

        public Something(Settings settingsHost, string Name) {
            this.Name = Name;

            sHost = settingsHost;
            moveDirection = (MoveDirection) random.Next(0, 4);
            speed = (float) random.Next(1, 10)/5;
            rect.Size = sHost.SpriteSize;
        }
        public virtual void Draw(SKCanvas canvas)
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
                moveDirection = (MoveDirection)random.Next(0, 4);
            }
        }
    }
}
