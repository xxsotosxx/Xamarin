using Game.Graphics;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;

namespace Game.Logic.Classes
{
    internal class Cat : Animated, IAnimated
    {
        public override void MoveTo(MoveDirection direction)
        {
            var Enemyes = Engine.SearchEnamy(this);
            //TODO: Найти ближайшего врага 
            foreach (var enemy in Enemyes)
            {
                var deltaX = enemy.PosX;
            }
            base.MoveTo(MoveDirection.Вправо);
        }

        public override void Draw(SKCanvas canvas, SKPaintSurfaceEventArgs args)
        {
            base.Draw(canvas, args);
            //canvas.DrawBitmap(Engine.AllImages["CatRightStep1"], rect);
            //switch (moveDirection)
            //{
            //    case MoveDirection.Вправо: canvas.DrawBitmap(catBitmap);
            //    case MoveDirection.Влево: X = -speed; break;
            //    case MoveDirection.Вверх: Y = -speed; break;
            //    case MoveDirection.Вниз: Y = speed; break;
            //}
        }

        public override void doAnimate(SKCanvas canvas, SKPaintSurfaceEventArgs args)
        {
            base.doAnimate(canvas, args);
            string frameName = $"CatRightStep{Math.Floor(this.FrameNumber)+1}";
            canvas.DrawBitmap(Engine.AllImages[frameName], rect);
        }
    }
}
