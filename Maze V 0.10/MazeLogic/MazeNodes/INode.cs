using MazeV.MazeLogic.CollectableItems;
using MazeV.MazeLogic.Drawing;
using MazeV.MazeLogic.MazeViews;
using MazeV.MazeLogic.Movement;
using MazeV.MazeLogic.Units;
using System.Collections.Generic;
using System.Drawing;

namespace MazeV.MazeLogic.MazeNodes
{
    public interface INode
    {
        ICollectableItem CollectablePoint { get; }

        int Id { get; set; }

        ILocation Location { get; set; }

        IList<NeighbourInfo> Neighbours { get; set; }

        IList<int> Path { get; set; }        

        int SquareSize { get; }

        IUnit Unit { get; set; }

        void Draw(INode node, IMazeGraphic graphic, IMazeViewData mazeView, Point topLeft, Point topRight, Point bottomLeft, Point bottomRight);

        IList<ILocation> GetAllPossibleNeighbours();

        INode GetNeigbour(IMazeViewData mazeView, IDirection direction);
    }
}