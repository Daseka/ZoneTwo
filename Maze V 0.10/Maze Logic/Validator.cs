using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeV.Maze_Logic
{
    public static class Validator
    {
        public static bool IsFutureLocationValid(Location currentLocation, Location futureLocation, Dictionary<int, Node> listOfNodes)
        {
            Node currentNode = listOfNodes.Where(x => x.Value.Location == currentLocation).Select(x => x.Value).FirstOrDefault();
            Node futureNode = listOfNodes.Where(x => x.Value.Location == futureLocation).Select(x => x.Value).FirstOrDefault();

            if (currentNode == null || futureNode == null)
                return false;

            return currentNode.Path.Contains(futureNode.Id);
        }

        public static bool IsLocationOccupied(Location location, UnitList unitList)
        {
            IUnit unit = unitList.Values.FirstOrDefault(x => x.CurrentLocation == location);

            return unit != null;
        }

        public static bool IsPlayerMaximumReached(UnitList unitList)
        {
            IUnit player = unitList.Values.FirstOrDefault(x => x is Player);

            if (player == null)
                return false;

            return true;
        }

        /// <summary>
        /// checks if the layout is valid. A layout is valid if you can make a route from a 
        /// given vertex to all other vertices
        /// </summary>
        /// <returns></returns>
        public static bool IsLayoutValid(MazeNodeData nodeData)
        {
            if (nodeData.NodesByIndex.Count == 0)
                return false;

            int position = -1;
            List<int> foundNodeIds = new List<int>();

            foundNodeIds.Add(1);

            do
            {
                position++;
                IEnumerable<int> vertices = nodeData.NodesByIndex[foundNodeIds[position]].Path.Except(foundNodeIds);
                if (vertices != null)
                    foundNodeIds.AddRange(vertices);

            } while (position + 1 < foundNodeIds.Count);

            return foundNodeIds.Count >= nodeData.NodesByIndex.Count;            
        }

        public static bool DoesPathToNodeExist(Node currentNode, Node nextNode)
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
    }
}
