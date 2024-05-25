using SkiaSharp;
using System;

namespace Engine
{

    public enum MoveDirection { Вверх = 1, Вниз = 2, Вправо = 3, Влево = 4 };

    //Интерфейс, обязывающий вести объект, который унаследовал этот интерфейс, как графический объект двумерного пространства.
    public interface I2DGraphicMember
    {
        void Draw(SKCanvas canvas, object args);
        //void Animate();
        bool SetXY(SKPoint point);
    }

    public interface IAnimated
    {
        void doAnimate(SKCanvas canvas, object args);
    }
    public interface IMapAction
    {
        void onCollision(Action<Something,Something> param);
    }
}