using MazeV.MazeLogic.MazeNodes;
using MazeV.MazeLogic.Units;
using System.Collections.Generic;
using System.Linq;

namespace MazeV.MazeLogic.Validators
{
    public class Validator
    {
        public bool DoesPathToNodeExist(INode currentNode, INode nextNode)
        {
            if (currentNode == null || nextNode == null)
                return false;

            foreach (int id in currentNode.Path)
            {
                if (id == nextNode.Id)
                    return true;
            }

            return false;
        }

        public bool IsFutureLocationValid(ILocation currentLocation, ILocation futureLocation, IMazeNodeData nodeData)
        {
            INode currentNode = GetNodeAtLocation(currentLocation, nodeData);
            INode futureNode = GetNodeAtLocation(futureLocation, nodeData);

            int id = (futureNode?.Id).GetValueOrDefault(-1);
            return currentNode.Path.Contains(id);
        }

        /// <summary>
        /// checks if the layout is valid. A layout is valid if you can make a route from a
        /// given vertex to all other vertices
        /// </summary>
        /// <returns></returns>
        public bool IsLayoutValid(IMazeNodeData nodeData)
        {
            if (nodeData.NodesByIndex.Count == 0)
                return false;

            int position = -1;
            var foundNodeIds = new List<int>() { 1 };

            do
            {
                position++;
                IEnumerable<int> vertices = nodeData.NodesByIndex[foundNodeIds[position]].Path.Except(foundNodeIds);
                if (vertices != null)
                    foundNodeIds.AddRange(vertices);
            } while (position + 1 < foundNodeIds.Count);

            return foundNodeIds.Count >= nodeData.NodesByIndex.Count;
        }

        public bool IsLocationOccupied(Location location, IUnitList unitList)
        {
            IUnit unit = unitList.FindUnit(location);

            return unit != null;
        }

        public bool IsPlayerMaximumReached(IUnitList unitList)
        {
            var players = unitList.FindUnit(typeof(Player));
            
            return players.Any();
        }

        private INode GetNodeAtLocation(ILocation currentLocation, IMazeNodeData nodeData)
        {
            return nodeData
                .NodesByIndex
                .Where(x => x.Value.Location.Equals(currentLocation))
                .Select(x => x.Value).FirstOrDefault();
        }
    }
}