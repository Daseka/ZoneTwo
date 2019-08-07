using MazeV.MazeLogic.Drawing;
using System;
using System.Drawing;

namespace MazeV.MazeLogic
{
    public class Coin : ICollectableItem
    {
        private readonly Size fSize;
        private Action<IMazeGraphic, Rectangle> fDoDrawing;

        public bool IsCollected { get; private set; }

        public Coin(DefaultSettings settings)
        {
            IsCollected = false;
            fSize = new Size(settings.CollectableSize, settings.CollectableSize);
            fDoDrawing = (grapics, rectangle) => grapics.FillEllipse(Brushes.Gold, rectangle);
        }

        public void Collect()
        {
            IsCollected = true;
            fDoDrawing = null;
        }

        public void Draw(IMazeGraphic graphics, Rectangle rectangle)
        {
            fDoDrawing?.Invoke(graphics, rectangle);
        }

        public int GetSize()
        {
            return fSize.Height;
        }
    }
}