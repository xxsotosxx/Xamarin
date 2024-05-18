using SkiaSharp;
using System;


using Engine;


namespace Game.Logic.Classes
{
    internal class Cat : Animated, IAnimated
    {
        internal Cat(Settings settings): base(settings) {
            moveDirection = MoveDirection.Вправо;
        }

        public override void Draw(SKCanvas canvas, object args)
        {

        }

        public override void MoveTo(MoveDirection direction)
        {
            base.MoveTo(moveDirection);
        }

        public override void doAnimate(SKCanvas canvas, object args)
        {
            base.doAnimate(canvas, args);
            string frameName = string.Empty;

            switch (moveDirection)
            {
                case MoveDirection.Вправо:
                    frameName = $"CatRightStep{Math.Floor(FrameNumber) + 1}";
                    break;
                case MoveDirection.Влево:
                    frameName = $"CatLeftStep{Math.Floor(FrameNumber) + 1}";
                    break;
                case MoveDirection.Вверх:
                    if (FrameNumber >= 2) FrameNumber = 0;
                    frameName = $"CatUpStep{Math.Floor(FrameNumber) + 1}";
                    //TODO: Исправить! Архитектурая городулька! Необходимо использовать FramesCount
                    break;
                case MoveDirection.Вниз:
                    if (FrameNumber >= 2) FrameNumber = 0;
                    frameName = $"CatDownStep{Math.Floor(FrameNumber) + 1}";
                    break;
            }
            if (frameName != string.Empty)
                canvas.DrawBitmap(GamePrepare.AllImages[frameName], rect);
        }

        public override void onCollision(Action<Something, Something> param)
        {
            base.onCollision(param);
            //moveDirection = moveDirection == MoveDirection.Вправо 
            //    ? MoveDirection.Влево
            //    : moveDirection == MoveDirection.Влево 
            //        ? moveDirection = MoveDirection.Вправо
            //        : moveDirection;


            //if (moveDirection == MoveDirection.Вправо)
            //    moveDirection = MoveDirection.Влево;
            //else if (moveDirection == MoveDirection.Влево)
            //    moveDirection = MoveDirection.Вправо;
        }
    }
}
