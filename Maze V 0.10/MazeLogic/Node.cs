﻿using MazeV.MazeLogic.Drawing;
using MazeV.MazeLogic.Units;
using System.Collections.Generic;
using System.Drawing;

namespace MazeV.MazeLogic
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

        public Shape Shape { get; set; }

        public int SquareSize { get; }

        public IUnit Unit { get; set; }

        public Node(CoinBuilder coinBuilder, DefaultSettings settings)
        {
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
            if (!Validator.DoesPathToNodeExist(node, topNode))
                graphic.DrawLine(pen, topLeft, topRight);

            if (!Validator.DoesPathToNodeExist(node, rightNode))
                graphic.DrawLine(pen, topRight, bottomRight);

            if (!Validator.DoesPathToNodeExist(node, bottomNode))
                graphic.DrawLine(pen, bottomRight, bottomLeft);

            if (!Validator.DoesPathToNodeExist(node, leftNode))
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