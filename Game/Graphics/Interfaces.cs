using Game.Logic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;

namespace Game.Graphics
{

    public enum MoveDirection { Вверх, Вниз, Вправо, Влево };

    //Интерфейс, обязывающий вести объект, который унаследовал этот интерфейс, как графический объект двумерного пространства.
    internal interface I2DGraphicMember
    {
        void Draw(SKCanvas canvas, SKPaintSurfaceEventArgs args);
        //void Animate();
        void SetXY(SKPoint point);
    }

    internal interface IAnimated
    {
        void doAnimate(SKCanvas canvas, SKPaintSurfaceEventArgs args);
    }
    internal interface IMapAction
    {
        void onCollision(Action<Something,Something> param);
    }
}