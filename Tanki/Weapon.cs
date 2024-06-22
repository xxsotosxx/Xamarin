using Engine;
using SkiaSharp;
using Tanki.Game;


namespace Tanki
{
    public class Weapon : Animated, IAnimated
    {
        private int _weapomID = 0;
        private Tank owner;
        private static class WSettings
        {

            internal static readonly AllRotatedBitmaps[,] weapons = new AllRotatedBitmaps[2, 1];
            static WSettings()
            {
                int c = 1;
                SKBitmap? img;
                //Маленький снаряд
                if (GameWorld.ShowingImageLibrary.TryGetValue($"Tank_5_2", out img)) weapons[0, 0] = img.AllRotated();
                //Большой снаряд
                if (GameWorld.ShowingImageLibrary.TryGetValue($"Tank_4_2", out img)) weapons[0, 0] = img.AllRotated();
            }
        }

        public Weapon(Tank owner, int weaponID) : base(GameWorld.DefaultSettings)
        {
            this.owner = owner;
            speed = 10;
            var point = owner.rect;
            _weapomID = weaponID;

            moveDirection = owner.moveDirection;

            if (this.SetXY(new SKPoint(point.Left, point.Top)))
                GameWorld.objects.Add(this);

        }
        public override void Draw(SKCanvas canvas)
        {

        }

        public override CollisionType GetCollisionType(Something targteObject)
        {
            if (targteObject == owner) return CollisionType.Continue;
            else
            {
                return CollisionType.Destroy;
            }

        }

        public override void doAnimate(SKCanvas canvas)
        {
            base.doAnimate(canvas);
            var rotatedImg = WSettings.weapons[_weapomID, 0][moveDirection];
            canvas.DrawBitmap(rotatedImg, rect);
        }
    }
}
