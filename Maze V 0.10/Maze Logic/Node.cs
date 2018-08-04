using System;
using System.Collections.Generic;
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

        public int CollectablePoint { get; set; }

        public Node()
        {
            Location = new Location();
            CollectablePoint = 1;
            Path = new List<int>();
            Neighbours = new List<NeighbourInfo>();
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
    }
}
