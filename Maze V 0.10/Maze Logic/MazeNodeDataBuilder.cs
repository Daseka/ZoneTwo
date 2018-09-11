using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeV.Maze_Logic
{
    public class MazeNodeDataBuilder
    {
        private readonly int fGridEnd;
        private readonly int fGridSize;
        private readonly int fGridStart;
        private readonly int fMininumRequiredPaths;

        public MazeNodeDataBuilder(int gridSize, int minimumPaths)
        {
            // Gridsize needs to be uneven to have a middle position that is not a fraction
            fGridSize = MakeGridSizeUneven(gridSize);
            fGridStart = GetGridStart(gridSize);
            fGridEnd = GetGridEnd(gridSize);

            fMininumRequiredPaths = minimumPaths;
        }

        /// <summary>
        /// generates layout
        /// </summary>
        public IMazeNodeData GenerateNodeData(int seed)
        {
            IMazeNodeData nodeData = InitializeMazeNodeData();
            nodeData = InitializeNeigbours(nodeData);
            nodeData = CreatePathData(seed, nodeData);

            return nodeData;
        }

        public IMazeViewData GenerateViewData(IMazeNodeData nodeData, IAxisFactory axisFactory)
        {
            return new MazeViewData(fGridStart, fGridEnd, fGridSize, nodeData, axisFactory);
        }

         private IMazeNodeData CreatePathData(int seed, IMazeNodeData nodeData)
        {
            ResetPathData(nodeData);
            IOrderedEnumerable<INode> sortedNodes = SortNodeData(nodeData);
            Random randomizer = new Random(seed);
            return SetPathsForNodeData(nodeData, sortedNodes, randomizer);            
        }

        private IMazeNodeData SetPathsForNodeData(IMazeNodeData nodeData, IOrderedEnumerable<INode> sortedNodes, Random randomizer)
        {
            List<int> copyOfNeigours = new List<int>();

            foreach (INode node in sortedNodes)
            {
                copyOfNeigours.Clear();
                copyOfNeigours.AddRange(node.Neighbours.Select(x => x.Id));

                SetMinimumRequiredPathsForNode(nodeData, randomizer, copyOfNeigours, node);
            }
            return nodeData;
        }

        private  IOrderedEnumerable<INode> SortNodeData(IMazeNodeData nodeData)
        {
            return nodeData.NodesByIndex.Values.OrderBy(x => x.Neighbours.Count);
        }

        private void SetMinimumRequiredPathsForNode(IMazeNodeData nodeData, Random randomizer, List<int> copyOfNeigours, INode node)
        {
            while (!HasMinimumRequiredPaths(node) && copyOfNeigours.Count > 0)
            {
                int neigbourId = randomizer.Next(copyOfNeigours.Count);
                int pathToNodeId = copyOfNeigours[neigbourId];
                copyOfNeigours.RemoveAt(neigbourId);

                SetPath(node, pathToNodeId, nodeData);
            }
        }

        /// <summary>
        /// returns a list containing neigbours of a given node
        /// </summary>
        private List<NeighbourInfo> FindNeighbouringNodes(INode node, IMazeNodeData nodeData)
        {
            List<NeighbourInfo> neighbours = new List<NeighbourInfo>();
            IEnumerable<INode> allPossibleNeigbours = GetNeigboursOfNode(node, nodeData);

            foreach (INode neighbourNode in allPossibleNeigbours)
            {
                NeighbourInfo tempNeighbour = new NeighbourInfo { Id = neighbourNode.Id, Location = neighbourNode.Location };
                neighbours.Add(tempNeighbour);
            }

            return neighbours;
        }

        private int GetGridEnd(int gridSize)
        {
            int size = MakeGridSizeUneven(gridSize);
            int subtractor = GetSubtractor(size);

            return size - subtractor;
        }

        private int GetGridStart(int gridSize)
        {
            int subtractor = GetSubtractor(gridSize);
            return 1 - subtractor;
        }

        /// <summary>
        /// Returns a sequence of nodes that are all the neigbours of the given node.
        /// </summary>
        private IEnumerable<INode> GetNeigboursOfNode(INode node, IMazeNodeData nodeData)
        {
            return node.GetAllPossibleNeighbours().Select(x => nodeData.GetNode(x)).Where(x => x != null);
        }

        private int GetSubtractor(int gridSize)
        {
            int unevenGridSize = MakeGridSizeUneven(gridSize);
            int divider = 2;
            return (unevenGridSize / divider) + (unevenGridSize % divider);
        }

        private bool HasMinimumRequiredPaths(INode vertex)
        {
            return vertex.Neighbours.Count == vertex.Path.Count || vertex.Path.Count == fMininumRequiredPaths;
        }

        private IMazeNodeData InitializeMazeNodeData()
        {
            Dictionary<ILocation, INode> nodesByLocation = new Dictionary<ILocation, INode>();
            Dictionary<int, INode> nodesByIndex = new Dictionary<int, INode>();

            int count = 0;
            for (int z = fGridStart; z <= fGridEnd; z++)
            {
                for (int y = fGridStart; y <= fGridEnd; y++)
                {
                    for (int x = fGridStart; x <= fGridEnd; x++)
                    {
                        count = AddNodeAtLocaction(nodesByLocation, nodesByIndex, count, z, y, x);
                    }
                }
            }

            return new MazeNodeData(nodesByIndex, nodesByLocation);
        }

        private static int AddNodeAtLocaction(Dictionary<ILocation, INode> nodesByLocation, Dictionary<int, INode> nodesByIndex, int count, int z, int y, int x)
        {
            count++;
            Location location = new Location(x, y, z);
            INode node = new Node() { Id = count, Location = location };

            nodesByIndex.Add(count, node);
            nodesByLocation.Add(location, nodesByIndex[count]);
            return count;
        }

        /// <summary>
        /// Fills in the neighbour list of each vertex
        /// </summary>
        private IMazeNodeData InitializeNeigbours(IMazeNodeData nodeData)
        {
            //initialize neigbours
            foreach (INode node in nodeData.NodesByIndex.Values)
            {
                node.Neighbours = FindNeighbouringNodes(node, nodeData);
            }

            return nodeData;
        }

        /// <summary>
        /// Checks if path is valid. path is not valid if it makes a loop using only 4 nodes. 
        /// A loop is made by trying to reach original node by traveling trough the neigbourNodes list 
        /// Recursive method
        /// </summary>
        private bool IsPathValid(INode fromNode, INode toNode, INode nodeToFind, int level, IMazeNodeData nodeData)
        {
            if (toNode.Path.Except(new[] { fromNode.Id }).Contains(nodeToFind.Id))
                return false;

            if (level < 3)
            {
                level++;
                foreach (int path in toNode.Path)
                {
                    if (!IsPathValid(toNode, nodeData.NodesByIndex[path], nodeToFind, level, nodeData))
                        return false;
                }
            }

            return true;
        }

        private int MakeGridSizeUneven(int gridSize)
        {
            return gridSize % 2 != 0 ? gridSize : gridSize + 1;
        }

        /// <summary>
        /// Clears all Path data
        /// </summary>
        private void ResetPathData(IMazeNodeData nodeData)
        {
            nodeData.ClearAllPaths();
        }

        /// <summary>
        /// Adds path from given node to destination node
        /// </summary>
        private void SetPath(INode node, int idOfDestinationNode, IMazeNodeData nodeData)
        {
            if ((node.Path.Contains(idOfDestinationNode) || !IsPathValid(node, nodeData.NodesByIndex[idOfDestinationNode], node, 1, nodeData)))
                return;

            node.Path.Add(idOfDestinationNode);
            nodeData.NodesByIndex[idOfDestinationNode].Path.Add(node.Id);
        }
    }
}