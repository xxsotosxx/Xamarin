using Engine;
using SkiaSharp;
using System.Diagnostics;
using System.Text.Json;

namespace Tanki.Game;

internal static class Startup
{
    internal static Form? globalForm = null;
    //internal static Settings globalSettings = new();
    internal static SKSizeI WorldSize => GameWorld.gameMap.BiomSize;
    internal static bool Init(Form form, SKSize size)
    {
        var globalSettings = new Settings(new SKRect(0, 0, size.Width, size.Height));
        GameWorld.DefaultSettings = globalSettings;
        Scene.backgroundColor = SKColors.Black;

        #region Загрузка Композиций спрайтов
        var Tank = BitmapExtender.Empty.LoadFromFile(@"Images\tanki.png");
        try
        {
            GameWorld.ShowingImageLibrary = Tank.Cutter(nameof(Tank), 8, 4);
        }
        catch { return false; }
        finally { Tank.Dispose(); }

        #endregion Загрузка Композиций спрайтов

        #region Загрузка карты (Биом)
        SKBitmap bitmapWall = BitmapExtender.Empty.LoadFromFile(@"Images\Wall1.png");

        string data = string.Empty;
        try
        {
            data = File.ReadAllText(Settings.worldMapFileName);
            GameWorld.gameMap = JsonSerializer.Deserialize<JsonMap>(data);
            if (GameWorld.gameMap == null) return false;
            foreach (var item in GameWorld.gameMap.Biom)
            {
                item.obj = new Showing(globalSettings, item.blockType, bitmapWall);
                item.obj.rect = SKRect.Create(new SKPoint(item.x, item.y), globalSettings.spriteSize);
            }
        }
        catch { return false; }
        #endregion Загрузка карты (Биом)

        #region Стенки по периметру 

        float y = (GameWorld.gameMap.BiomSize.Height - 1) * globalSettings.SpriteSize.Height;
        float maxW = 0;
        for (int i = 0; i < GameWorld.gameMap.BiomSize.Width; i++)
        {
            Showing wallU = new Showing(globalSettings, "Wall1", bitmapWall);
            wallU.rect = SKRect.Create(i * globalSettings.SpriteSize.Width, 0, globalSettings.SpriteSize.Width, globalSettings.SpriteSize.Height);
            Showing wallD = new Showing(globalSettings, "Wall1", bitmapWall);
            wallD.rect = SKRect.Create(i * globalSettings.SpriteSize.Width, y, globalSettings.SpriteSize.Width, globalSettings.SpriteSize.Height);

            maxW = Math.Max(maxW, wallU.rect.Right);
            GameWorld.gameMap.AddToBiom(wallU);
            GameWorld.gameMap.AddToBiom(wallD);
        }

        float x = (GameWorld.gameMap.BiomSize.Width - 1) * globalSettings.SpriteSize.Width;
        float maxH = 0;
        for (int i = 0; i < GameWorld.gameMap.BiomSize.Height; i++)
        {
            Showing wallU = new Showing(globalSettings, "Wall1", bitmapWall);
            wallU.rect = SKRect.Create(0, i * globalSettings.SpriteSize.Height, globalSettings.SpriteSize.Width, globalSettings.SpriteSize.Height);
            Showing wallD = new Showing(globalSettings, "Wall1", bitmapWall);
            wallD.rect = SKRect.Create(x, i * globalSettings.SpriteSize.Height, globalSettings.SpriteSize.Width, globalSettings.SpriteSize.Height);

            GameWorld.gameMap.AddToBiom(wallU);
            GameWorld.gameMap.AddToBiom(wallD);
            maxH = Math.Max(maxH, wallU.rect.Bottom);
        }
        #endregion Стенки по периметру
        form.Width =  (int)Math.Round(maxW);
        form.Height = (int)Math.Round(maxH + globalSettings.SpriteSize.Height);

        Stopwatch watch = new Stopwatch();
        watch.Start();

        var renderThread = new System.Threading.Timer((p) => {
            if (watch.ElapsedMilliseconds > 5000)
            {
                watch.Reset();
                Tank tank = new Tank(globalSettings);
                tank.SetXY(new(9 * globalSettings.spriteSize.Width, 9 * globalSettings.spriteSize.Height));
                GameWorld.objects.Add(tank);
                watch.Start();
            }

            Scene.AnimatiomLoopTimer();
            if (globalForm != null)
            {
                globalForm.Invalidate();
            }
        });
        renderThread.Change(0, 10);
        globalForm = form;
        return true;
    }
}
