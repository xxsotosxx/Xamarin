﻿using SkiaSharp;
using System;


using Engine;


namespace Game.Logic.Classes
{
    internal class Cat : Animated, IAnimated
    {
        internal Cat(Settings settings): base(settings) {
            moveDirection = MoveDirection.Вправо;
        }
        public override void MoveTo(MoveDirection direction)
        {
            //var Enemyes = Engine.SearchEnemy(this);
            ////TODO: Найти ближайшего врага 
            //foreach (var enemy in Enemyes)
            //{
            //    var deltaX = enemy.PosX;
            //}
            base.MoveTo(moveDirection);
        }

        public override void Draw(SKCanvas canvas, /*SKPaintSurfaceEventArgs*/ object args)
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

        public override void doAnimate(SKCanvas canvas, /*SKPaintSurfaceEventArgs */ object args)
        {
            base.doAnimate(canvas, args);

            string frameName = $"CatRightStep{Math.Floor(this.FrameNumber)+1}";
            canvas.DrawBitmap(GamePrepare.AllImages[frameName], rect);
        }

        public override void onCollision(Action<Something, Something> param)
        {
            moveDirection = moveDirection == MoveDirection.Вправо 
                ? MoveDirection.Влево
                : moveDirection == MoveDirection.Влево 
                    ? moveDirection = MoveDirection.Вправо
                    : moveDirection;


            //if (moveDirection == MoveDirection.Вправо)
            //    moveDirection = MoveDirection.Влево;
            //else if (moveDirection == MoveDirection.Влево)
            //    moveDirection = MoveDirection.Вправо;
        }
    }
}
