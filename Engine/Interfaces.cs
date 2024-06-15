using SkiaSharp;
using System;

namespace Engine
{

    public enum MoveDirection { Вверх = 0, Вниз, Вправо, Влево };

    //Интерфейс, обязывающий вести объект, который унаследовал этот интерфейс, как графический объект двумерного пространства.
    public interface I2DGraphicMember
    {
        void Draw(SKCanvas canvas);
        //void Animate();
        bool SetXY(SKPoint point);
    }

    public interface IAnimated
    {
        void doAnimate(SKCanvas canvas);
    }
    public interface IMapAction
    {
        void onCollision(Action<Something,Something> param);
    }
}