using MazeV.MazeLogic.CollectableItems;
using MazeV.MazeLogic.MazeViews;
using MazeV.MazeLogic.Movement.Directions;
using MazeV.MazeLogic.Settings;
using MazeV.MazeLogic.Units;
using MazeV.MazeLogic.Visualizer;
using System.Collections.Generic;
using System.Linq;

namespace MazeV.MazeLogic.MazeNodes
{
    public class Node : INode
    {
        private readonly NodeVisualizer _nodeVisualizer;
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
        public IUnit Unit { get; set; }

        public Node(CoinBuilder coinBuilder, DefaultSettings settings, NodeVisualizer nodeVisualizer)
        {
            _nodeVisualizer = nodeVisualizer;
            Location = new Location();
            CollectablePoint = coinBuilder.Build();
            Path = new List<int>();
            Neighbours = new List<NeighbourInfo>();
            SquareSize = settings.NodeSize;
        }

        public void Draw(NodeVisualizerInfo nodeVisualizerInfo)
        {
            _nodeVisualizer.DrawNode(nodeVisualizerInfo);
        }

        public IList<ILocation> GetAllPossibleNeighbours()
        {
            return Location.GetAllPossibleNeighbours().ToList();
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