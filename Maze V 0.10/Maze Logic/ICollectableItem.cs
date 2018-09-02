using System;
using System.Drawing;

namespace MazeV.Maze_Logic
{
    public interface ICollectableItem
    {
        bool IsCollected { get; }

        void Collect();

        void Draw(Graphics graphics, Rectangle rectangle);

        int GetSize();
    }

    internal class Coin : ICollectableItem
    {
        private Action<Graphics, Rectangle> fDoDrawing;
        private bool fIsCollected;
        private Size fSize;
        bool ICollectableItem.IsCollected => fIsCollected;

        public Coin()
        {
            fIsCollected = false;
            fSize = new Size(DefaultSettings.CollectableSize, DefaultSettings.CollectableSize);
            fDoDrawing = (grapics, rectangle) => grapics.FillEllipse(Brushes.Gold, rectangle);
        }

        public void Collect()
        {
            fIsCollected = true;
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