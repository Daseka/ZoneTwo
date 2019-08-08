using MazeV.MazeLogic.CollectableItems;
using MazeV.MazeLogic.Drawing;
using MazeV.MazeLogic.MazeViews;
using MazeV.MazeLogic.Movement;
using MazeV.MazeLogic.Settings;
using MazeV.MazeLogic.Units;
using MazeV.MazeLogic.Validators;
using System.Collections.Generic;
using System.Drawing;

namespace MazeV.MazeLogic.MazeNodes
{
    public class Node : INode
    {
        public ICollectableItem CollectablePoint { get; }

        public int Id { get; set; }

        public ILocation Location { get; set; }

        /// <summary>
        /// A list of all neigbours of current node
        /// </summary>
        public IList<NeighbourInfo> Neighbours { get; set; }

        /// <summary>
        /// A list of ids of neigbouring nodes that has a path leading to current node
        /// </summary>
        public IList<int> Path { get; set; }        

        public int SquareSize { get; }

        private readonly Validator _validator;

        public IUnit Unit { get; set; }

        public Node(CoinBuilder coinBuilder, DefaultSettings settings, Validator validator)
        {
            _validator = validator;
            Location = new Location();
            CollectablePoint = coinBuilder.Build();
            Path = new List<int>();
            Neighbours = new List<NeighbourInfo>();
            SquareSize = settings.NodeSize;            
        }

        public void Draw(INode node, IMazeGraphic graphic, IMazeViewData mazeView, Point topLeft, Point topRight, Point bottomLeft, Point bottomRight)
        {
            INode leftNode = node.GetNeigbour(mazeView, new LeftDirection());
            INode bottomNode = node.GetNeigbour(mazeView, new DownDirection());
            INode rightNode = node.GetNeigbour(mazeView, new RightDirection());
            INode topNode = node.GetNeigbour(mazeView, new UpDirection());

            var pen = new Pen(Color.Blue);
            if (!_validator.DoesPathToNodeExist(node, topNode))
                graphic.DrawLine(pen, topLeft, topRight);

            if (!_validator.DoesPathToNodeExist(node, rightNode))
                graphic.DrawLine(pen, topRight, bottomRight);

            if (!_validator.DoesPathToNodeExist(node, bottomNode))
                graphic.DrawLine(pen, bottomRight, bottomLeft);

            if (!_validator.DoesPathToNodeExist(node, leftNode))
                graphic.DrawLine(pen, bottomLeft, topLeft);
        }

        public IList<ILocation> GetAllPossibleNeighbours()
        {
            return Location.GetAllPossibleNeighbours();
        }

        public INode GetNeigbour(IMazeViewData mazeView, IDirection direction)
        {
            ILocation location = Location.Add(mazeView.MovementCube[direction.Value]);

            return mazeView.GetNodeAt(location);
        }

        public override string ToString()
        {
            return $"{Location}";
        }
    }
}