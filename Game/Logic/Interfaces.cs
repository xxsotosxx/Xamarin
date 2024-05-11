using SkiaSharp.Views.Forms;
using SkiaSharp;
using System;
 

namespace Game.Logic
{
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
        void onCollision(Action<Something, Something> param);
    }
}
