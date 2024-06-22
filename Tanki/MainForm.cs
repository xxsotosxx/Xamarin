using SkiaSharp.Views.Desktop;
using SkiaSharp;
using System.Drawing.Imaging;
using Engine;
using Tanki.Game;

namespace Tanki
{
    public partial class MainForm : Form
    {
        Bitmap? bitmap = null;

        Tank? comanderTank = null;
             
        public MainForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Left = 0;
            Top = 0;
            //Width = 1024;
            //Height = 768;
            Text = "Танки";

            //Событие возникает перед отобображением окна на экране.
            Activated += MainForm_Activated;
            
            Load += MainForm_Load;

            KeyDown += MainForm_KeyDown;

            MouseClick += MainForm_MouseClick;
        }

        private void MainForm_MouseClick(object? sender, MouseEventArgs e)
        {
            //var obj = new Something(Startup.globalSettings, "Tank");
            comanderTank = new ComanderTank(GameWorld.DefaultSettings);
            if (comanderTank.SetXY(new SKPoint(100,100)))
                GameWorld.objects.Add(comanderTank);
        }

        private void MainForm_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Down:
                    if (comanderTank != null)
                        comanderTank.moveDirection = MoveDirection.Вниз;
                    break;
                case Keys.Up:
                    if (comanderTank != null)
                        comanderTank.moveDirection = MoveDirection.Вверх;
                    break;
                case Keys.Right:
                    if (comanderTank != null)
                        comanderTank.moveDirection = MoveDirection.Вправо;
                    break;
                case Keys.Left:
                    if (comanderTank != null)
                        comanderTank.moveDirection = MoveDirection.Влево;
                    break;
                case Keys.Space:
                    if (comanderTank != null)
                    {
                        new Weapon(comanderTank, 0);
                    }
                    break;
            }
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            bitmap = new Bitmap(Width, Height);
            Startup.Init(this, new(Width, Height));
            Location = new Point(0, 0);
            //Width = Startup.WorldSize.Width * Startup.globalSettings.spriteSizeI.Width;
            //Height = Startup.WorldSize.Height * Startup.globalSettings.spriteSizeI.Height;
        }

        private void MainForm_Activated(object? sender, EventArgs e)
        {
        }

        protected virtual void OnPaintSurface(SKPaintSurfaceEventArgs args)
        {
            var canvas = args.Surface.Canvas;

            // Масштабирование канвы под разрешение экрана
            var scale = args.Info.Width / (float)Width;
            canvas.Scale(scale);

            Scene.Paint(canvas);
        }

        #region OnPaint
        protected override void OnPaint(PaintEventArgs e)
        {
            if (bitmap == null) return;
            base.OnPaint(e);

            var info = CreateBitmap();
            if (info.Width == 0 || info.Height == 0) return;

            var data = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);

            using (var surface = SKSurface.Create(info, data.Scan0, data.Stride))
            {
                OnPaintSurface(new SKPaintSurfaceEventArgs(surface, info));
                surface.Canvas.Flush();
            }

            bitmap.UnlockBits(data);
            e.Graphics.DrawImage(bitmap, 0, 0);
        }

        private SKImageInfo CreateBitmap()
        {
            var info = new SKImageInfo(Width, Height, SKImageInfo.PlatformColorType, SKAlphaType.Premul);
            if (bitmap == null || bitmap.Width != info.Width || bitmap.Height != info.Height)
            {
                FreeBitmap();

                if (info.Width != 0 && info.Height != 0)
                    bitmap = new Bitmap(info.Width, info.Height, PixelFormat.Format32bppPArgb);
            }

            return info;
        }
        private void FreeBitmap()
        {
            if (bitmap != null)
            {
                bitmap.Dispose();
                bitmap = null;
            }
        }
        #endregion
    }
}
