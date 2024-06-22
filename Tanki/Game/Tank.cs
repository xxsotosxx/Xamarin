
using Engine;
using SkiaSharp;

namespace Tanki.Game;

public class Tank : Animated, IAnimated
{
    protected int _tankID = 0;

    private static class TankiSettings
    {

        internal static readonly AllRotatedBitmaps[,] tanki = new AllRotatedBitmaps[2,8];
        static TankiSettings()
        {
            //int r = 0;
            int c = 1;
            SKBitmap? img;

            for (int tankID = 0; tankID < 2; tankID++)
            {
                // Реорганизация спрайтов из исходного файла в массив
                // Нулевой спрайт, который лежит в позиции строка 0, столбец 1
                if (GameWorld.ShowingImageLibrary.TryGetValue($"Tank_{c}_{tankID}", out img)) tanki[tankID, 0] = img.AllRotated();
                // Первый спрайт, который лежит в позиции строка 1, столбец 0
                if (GameWorld.ShowingImageLibrary.TryGetValue($"Tank_{c - 1}_{tankID + 1}", out img)) tanki[tankID, 1] = img.AllRotated();

                c = 7;
                // Цикл записи оставшихся спрайтов, расположенных в одной строке (анимация в обратном порядке)
                do
                {
                    if (GameWorld.ShowingImageLibrary.TryGetValue($"Tank_{c}_{tankID}", out img))
                        tanki[tankID, 8 - c + 1] = img.AllRotated();
                } while (!(c-- <= 2));
            }
        }
    }

    public Tank(Settings settings) : base(settings)
    {
        moveDirection = MoveDirection.Вправо;
        FramesCount = 8;
    }

    public override void Draw(SKCanvas canvas)
    {

    }

    public override void doAnimate(SKCanvas canvas)
    {
        base.doAnimate(canvas);
        var rotatedImg = TankiSettings.tanki[_tankID, frameNumberInt][moveDirection];
        canvas.DrawBitmap(rotatedImg, rect);
    }
}

public class ComanderTank : Tank
{
    public ComanderTank(Settings settings) : base(settings)
    {
        _tankID = 1;
    }
}