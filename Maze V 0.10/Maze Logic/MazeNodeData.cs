using System.Collections.Generic;
using System.Linq;

namespace MazeV.Maze_Logic
{
    public class MazeNodeData
    {        
        public Dictionary<int, Node> NodesByIndex { get; }
        public Dictionary<Location, Node> NodesByLocation { get; }
        public int Count { get => NodesByIndex.Count; }

        public MazeNodeData(Dictionary<int, Node> nodesByIndex, Dictionary<Location, Node> nodesByLocation)
        {
            NodesByIndex = nodesByIndex;
            NodesByLocation = nodesByLocation;
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
            return NodesByIndex.Count;
        }

        public int GetEmptyNodeCount()
        {
            return NodesByIndex.Where(x => x.Value.CollectablePoint == 0).Count();
        }

        public void ClearAllPaths()
        {            
            foreach (Node node in NodesByIndex?.Values)
            {
                node.Path.Clear();
            }
        }
    }
}
