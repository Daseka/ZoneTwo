using System;
using System.Drawing;

namespace MazeV.Maze_Logic
{
    internal class Coin : ICollectableItem
    {
        private readonly Size fSize;
        private Action<Graphics, Rectangle> fDoDrawing;

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

        public void Draw(Graphics graphics, Rectangle rectangle)
        {
            fDoDrawing?.Invoke(graphics, rectangle);
        }

        public int GetSize()
        {
            return fSize.Height;
        }
    }
}