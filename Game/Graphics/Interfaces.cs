using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Game.Graphics
{

    internal enum MoveDirection { Вверх, Вниз, Вправо, Влево };

    //Интерфейс, обязывающий вести объект, который унаследовал этот интерфейс, как графический объект двумерного пространства.
    internal interface I2DGraphicMember
    {
        void Draw(SKCanvas canvas, SKPaintSurfaceEventArgs args);
        void Animate();
        void SetXY(SKPoint point);
    }
}
