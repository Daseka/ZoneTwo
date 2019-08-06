using MazeV.MazeLogic.Drawing;
using System;
using System.Drawing;

namespace MazeV.MazeLogic
{
    internal class Coin : ICollectableItem
    {
        private readonly Size fSize;
        private Action<IMazeGraphics, Rectangle> fDoDrawing;

        public bool IsCollected { get; private set; }

        public Coin()
        {
            IsCollected = false;
            fSize = new Size(DefaultSettings.CollectableSize, DefaultSettings.CollectableSize);
            fDoDrawing = (grapics, rectangle) => grapics.FillEllipse(Brushes.Gold, rectangle);
        }

        public void Collect()
        {
            IsCollected = true;
            fDoDrawing = null;
        }

        public void Draw(IMazeGraphics graphics, Rectangle rectangle)
        {
            fDoDrawing?.Invoke(graphics, rectangle);
        }

        public int GetSize()
        {
            return fSize.Height;
        }
    }
}