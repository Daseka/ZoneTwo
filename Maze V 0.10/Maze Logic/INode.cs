using System.Collections.Generic;
using System.Drawing;

namespace MazeV.Maze_Logic
{
    public interface INode
    {
        ICollectableItem CollectablePoint { get; }

        int Id { get; set; }

        ILocation Location { get; set; }

        IList<NeighbourInfo> Neighbours { get; set; }

        IList<int> Path { get; set; }

        Shape Shape { get; set; }

        int SquareSize { get; }

        IUnit Unit { get; set; }

        void Draw(INode node, Graphics grapic, IMazeViewData mazeView, Point topLeft, Point topRight, Point bottomLeft, Point bottomRight);

        IList<ILocation> GetAllPossibleNeighbours();

        INode GetNeigbour(IMazeViewData mazeView, IDirection direction);
    }
}