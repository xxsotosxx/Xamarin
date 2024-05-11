using SkiaSharp;
using System;

namespace Engine
{

    public enum MoveDirection { Вверх, Вниз, Вправо, Влево };

    //Интерфейс, обязывающий вести объект, который унаследовал этот интерфейс, как графический объект двумерного пространства.
    public interface I2DGraphicMember
    {
        void Draw(SKCanvas canvas, object args/*SKPaintSurfaceEventArgs args*/);
        //void Animate();
        void SetXY(SKPoint point);
    }

    public interface IAnimated
    {
        void doAnimate(SKCanvas canvas, object args/*SKPaintSurfaceEventArgs args*/);
    }
    public interface IMapAction
    {
        void onCollision(Action<Something,Something> param);
    }
}