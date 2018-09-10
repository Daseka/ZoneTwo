using System.Collections.Generic;
using System.Drawing;

namespace MazeV.Maze_Logic
{
    public interface INode
    {
        ICollectableItem CollectablePoint { get; }
        int Id { get; set; }
        ILocation Location { get; set; }
        List<NeighbourInfo> Neighbours { get; set; }
        List<int> Path { get; set; }
        Shape Shape { get; set; }
        int SquareSize { get; }
        IUnit Unit { get; set; }

        void Draw(INode node, Graphics grapic, IMazeViewData mazeView, Point topLeft, Point topRight, Point bottomLeft, Point bottomRight);
        List<ILocation> GetAllPossibleNeighbours();
        INode GetNeigbour(IMazeViewData mazeView, IDirection direction);
    }
}