using System.Collections.Generic;
using System.Linq;

namespace MazeV.Maze_Logic
{
    public class MazeNodeData : IMazeNodeData
    {
        public int Count { get => NodesByIndex.Count; }

        public Dictionary<int, INode> NodesByIndex { get; }

        public Dictionary<ILocation, INode> NodesByLocation { get; }

        public MazeNodeData(Dictionary<int, INode> nodesByIndex, Dictionary<ILocation, INode> nodesByLocation)
        {
            NodesByIndex = nodesByIndex;
            NodesByLocation = nodesByLocation;
        }

        public void ClearAllPaths()
        {
            foreach (INode node in NodesByIndex?.Values)
            {
                node.Path.Clear();
            }
        }

        public int GetCollectableNodes()
        {
            return NodesByIndex.Count(x => !x.Value.CollectablePoint.IsCollected);
        }

        /// <summary>
        /// returns the node of given location. Returns null if node is not pressent in the NodesByLocation Dictionairy
        /// </summary>
        public INode GetNode(ILocation location)
        {
            NodesByLocation.TryGetValue(location, out INode node);
            return node;
        }

        /// <summary>
        /// returns the node of given index, Rturns null if node is not present in the NodesByLocation Dictionairy
        /// </summary>
        public INode GetNode(int index)
        {
            NodesByIndex.TryGetValue(index, out INode node);
            return node;
        }

        public int GetTotalNodeCount()
        {
            return NodesByLocation.Count;
        }
    }
}