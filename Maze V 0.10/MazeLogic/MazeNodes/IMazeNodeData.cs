using System.Collections.Generic;

namespace MazeV.MazeLogic.MazeNodes
{
    public interface IMazeNodeData
    {
        int Count { get; }

        Dictionary<int, INode> NodesByIndex { get; }

        Dictionary<ILocation, INode> NodesByLocation { get; }

        void ClearAllPaths();

        int GetCollectableNodes();

        INode GetNode(ILocation location);

        INode GetNode(int index);

        int GetTotalNodeCount();
    }
}