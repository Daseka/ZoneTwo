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
}