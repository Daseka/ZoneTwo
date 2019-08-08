using MazeV.MazeLogic.Drawing;
using MazeV.MazeLogic.Settings;
using System;
using System.Drawing;

namespace MazeV.MazeLogic.CollectableItems
{
    public class Coin : ICollectableItem
    {
        private readonly Size _size;
        private Action<IMazeGraphic, Rectangle> _doDrawing;

        public bool IsCollected { get; private set; }

        public Coin(DefaultSettings settings)
        {
            IsCollected = false;
            _size = new Size(settings.CollectableSize, settings.CollectableSize);
            _doDrawing = (grapics, rectangle) => grapics.FillEllipse(Brushes.Gold, rectangle);
        }

        public void Collect()
        {
            IsCollected = true;
            _doDrawing = null;
        }

        public void Draw(IMazeGraphic graphics, Rectangle rectangle)
        {
            _doDrawing?.Invoke(graphics, rectangle);
        }

        public int GetSize()
        {
            return _size.Height;
        }
    }
}