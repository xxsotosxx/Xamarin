using SkiaSharp;
using System;

namespace Engine
{

    //При добавлении новых вариантов в это перечисление, вариант "Влево" должен оставаться последним.
    //TODO: Желательно перейти на перечислимую/итерируемую коллекцию (IEnumerable).
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