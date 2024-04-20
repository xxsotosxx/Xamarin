using SkiaSharp;
using SkiaSharp.Views.Forms;


using Game.Graphics;
using System;

namespace Game.Logic
{
    /// <summary>
    /// Класс - существо, от которого будут унаследованы все другие.
    /// </summary>
    internal class Something : I2DGraphicMember
    {
        protected SKPoint xy;
        protected MoveDirection moveDirection;
        protected float speed;
        public Something() {
            moveDirection = (MoveDirection)new Random().Next(0, 4);
            speed = (float)new Random().Next(1, 10)/5;
        }
        public virtual void Draw(SKCanvas canvas, SKPaintSurfaceEventArgs args) => Figures.GradientSphere(canvas, args, xy, SKColors.Bisque);
        public void SetXY(SKPoint point) => xy = point;
        private void MoveTo(MoveDirection direction)
        {
            var predXY = xy;
            switch (direction)
            {
                case MoveDirection.Вправо: xy.X+=speed;break;
                case MoveDirection.Влево:  xy.X-=speed;break;
                case MoveDirection.Вверх:  xy.Y-=speed;break;
                case MoveDirection.Вниз:   xy.Y+=speed;break;
            }
            if (xy.X < 0 || xy.Y < 0 || xy.X > Scene.mainCanvas.Width || xy.Y > Scene.mainCanvas.Height)
            {
                xy = predXY;
                moveDirection = (MoveDirection)new Random().Next(0, 4);
            }
        }

        public void Animate() => MoveTo(moveDirection);
    }
}
