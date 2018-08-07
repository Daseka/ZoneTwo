using System.Collections.Generic;
using System.Linq;

namespace MazeV.Maze_Logic
{
    public class MazeNodeData
    {
        public int Count { get => NodesByIndex.Count; }
        public Dictionary<int, Node> NodesByIndex { get; }
        public Dictionary<Location, Node> NodesByLocation { get; }

        public MazeNodeData(Dictionary<int, Node> nodesByIndex, Dictionary<Location, Node> nodesByLocation)
        {
            NodesByIndex = nodesByIndex;
            NodesByLocation = nodesByLocation;
        }

        public void ClearAllPaths()
        {
            foreach (Node node in NodesByIndex?.Values)
            {
                node.Path.Clear();
            }
        }

        public int GetEmptyNodeCount()
        {
            return NodesByIndex.Count(x => x.Value.CollectablePoint == 0);
        }

        /// <summary>
        /// returns the node of given location. Returns null if node is not pressent in the NodesByLocation Dictionairy
        /// </summary>
        public Node GetNode(Location location)
        {
            NodesByLocation.TryGetValue(location, out Node node);
            return node;
        }

        /// <summary>
        /// returns the node of given index, Rturns null if node is not present in the NodesByLocation Dictionairy
        /// </summary>
        public Node GetNode(int index)
        {
            NodesByIndex.TryGetValue(index, out Node node);
            return node;
        }

        public int GetTotalNodeCount()
        {
            return NodesByLocation.Count;
        }
    }
}