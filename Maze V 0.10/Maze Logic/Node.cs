using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeV.Maze_Logic
{
    public class Node : INode
    {
        public int Id { get; set; }
        /// <summary>
        /// A list of all neigbours of current node
        /// </summary>
        public List<NeighbourInfo> Neighbours { get; set; }
        /// <summary>
        /// A list of ids of neigbouring nodes that has a path leading to current node
        /// </summary>
        public List<int> Path { get; set; }
        public Shape Shape { get; set; }
        public ILocation Location { get; set; }
        public ICollectableItem CollectablePoint { get; }
        public IUnit Unit { get; set; }
        public int SquareSize { get; }

        public Node()
        {
            Location = new Location();
            CollectablePoint = new Coin();
            Path = new List<int>();
            Neighbours = new List<NeighbourInfo>();
            SquareSize = DefaultSettings.NodeSize;
        }

        public List<ILocation> GetAllPossibleNeighbours()
        {
            return Location.GetAllPossibleNeighbours();
        }

        public INode GetNeigbour(IMazeViewData mazeView, IDirection direction)
        {
            ILocation location = Location.Add(mazeView.MovementCube[direction.Value]);            

            return mazeView.GetNodeAt(location); 
        }
        
        public void Draw(INode node, Graphics grapic, IMazeViewData mazeView, Point topLeft, Point topRight, Point bottomLeft, Point bottomRight)
        {
            INode leftNode = node.GetNeigbour(mazeView, new LeftDirection());
            INode bottomNode = node.GetNeigbour(mazeView, new DownDirection());
            INode rightNode = node.GetNeigbour(mazeView, new RightDirection());
            INode topNode = node.GetNeigbour(mazeView, new UpDirection());

            Pen pen = new Pen(Color.Blue);
            if (!Validator.DoesPathToNodeExist(node, topNode))
                grapic.DrawLine(pen, topLeft, topRight);

            if (!Validator.DoesPathToNodeExist(node, rightNode))
                grapic.DrawLine(pen, topRight, bottomRight);

            if (!Validator.DoesPathToNodeExist(node, bottomNode))
                grapic.DrawLine(pen, bottomRight, bottomLeft);

            if (!Validator.DoesPathToNodeExist(node, leftNode))
                grapic.DrawLine(pen, bottomLeft, topLeft);
        }
    }
}
