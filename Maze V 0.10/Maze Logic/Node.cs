using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeV.Maze_Logic
{
    public class Node
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
        public Shape shape { get; set; }
        public Location Location { get; set; }
        public ICollectableItem CollectablePoint { get; set; }
        public int SquareSize { get; }

        public Node()
        {
            Location = new Location();
            CollectablePoint = new Coin();
            Path = new List<int>();
            Neighbours = new List<NeighbourInfo>();
            SquareSize = DefaultSettings.NodeSize;
        }

        public List<Location> GetAllPossibleNeighbours()
        {
            return Location.GetAllPossibleNeighbours();
        }

        public Node GetNeigbour(MazeViewData mazeView, Direction direction)
        {
            Location location = Location + mazeView.MovementCube[(int)direction];            

            return mazeView.GetNodeAt(location); 
        }
        
        public void Draw(Node node, Graphics grapic, MazeViewData mazeView, Point topLeft, Point topRight, Point bottomLeft, Point bottomRight)
        {
            Node leftNode = node.GetNeigbour(mazeView, Direction.Left);
            Node bottomNode = node.GetNeigbour(mazeView, Direction.Down);
            Node rightNode = node.GetNeigbour(mazeView, Direction.Right);
            Node topNode = node.GetNeigbour(mazeView, Direction.Up);

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
