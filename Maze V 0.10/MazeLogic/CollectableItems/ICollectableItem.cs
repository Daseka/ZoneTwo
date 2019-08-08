using MazeV.MazeLogic.Drawing;
using System.Drawing;

namespace MazeV.MazeLogic.CollectableItems
{
    public interface ICollectableItem
    {
        bool IsCollected { get; }

        void Collect();

        void Draw(IMazeGraphic graphics, Rectangle rectangle);

        int GetSize();
    }
}