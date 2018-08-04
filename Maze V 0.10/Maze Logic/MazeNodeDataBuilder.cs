using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeV.Maze_Logic
{
    public class MazeNodeDataBuilder
    {
        private int fGridEnd;
        private int fGridSize;
        private int fGridStart;
        private int fMininumRequiredPaths;

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
        public MazeNodeData GenerateNodeData(int seed)
        {
            MazeNodeData nodeData = InitializeMazeNodeData();
            nodeData = InitializeNeigbours(nodeData);
            nodeData = CreatePathData(seed, nodeData);

            return nodeData;
        }

        public MazeViewData GenerateViewData(MazeNodeData nodeData)
        {
            return new MazeViewData(fGridStart, fGridEnd, fGridSize, nodeData);
        }

        private MazeNodeData CreatePathData(int seed, MazeNodeData nodeData)
        {
            ResetPathData(nodeData);
            Random randomizer = new Random(seed);
            List<int> copyOfNeigours = new List<int>();

            foreach (Node node in nodeData.NodesByIndex.Values.ToList().OrderBy(x => x.Neighbours.Count))
            {
                copyOfNeigours.Clear();
                copyOfNeigours.AddRange(node.Neighbours.Select(x => x.Id));

                int neighbourCount = node.Neighbours.Count;

                while (!HasMinimumRequiredPaths(node) && copyOfNeigours.Count > 0)
                {
                    int neigbourId = randomizer.Next(copyOfNeigours.Count);
                    int pathToNodeId = copyOfNeigours[neigbourId];
                    copyOfNeigours.RemoveAt(neigbourId);

                    if (node.Path.Contains(pathToNodeId) || !IsPathValid(node, nodeData.NodesByIndex[pathToNodeId], node, 1, nodeData))
                        continue;

                    SetPath(node, pathToNodeId, nodeData);
                }
            }

            return nodeData;
        }

        /// <summary>
        /// returns a list containing neigbours of a given node
        /// </summary>
        private List<NeighbourInfo> FindNeighbouringNodes(Node node, MazeNodeData nodeData)
        {
            List<NeighbourInfo> neighbours = new List<NeighbourInfo>();
            IEnumerable<Node> allPossibleNeigbours = GetNeigboursOfNode(node, nodeData);

            foreach (Node neighbourNode in allPossibleNeigbours)
            {
                NeighbourInfo tempNeighbour = new NeighbourInfo() { Id = neighbourNode.Id, Location = neighbourNode.Location };
                neighbours.Add(tempNeighbour);
            }

            return neighbours;
        }

        private int GetGridEnd(int gridSize)
        {
            gridSize = MakeGridSizeUneven(gridSize);
            int subtractor = GetSubtractor(gridSize);

            return gridSize - subtractor;
        }

        private int GetGridStart(int gridSize)
        {
            int subtractor = GetSubtractor(gridSize);
            return 1 - subtractor;
        }

        /// <summary>
        /// Returns a sequence of nodes that are all the neigbours of the given node.
        /// </summary>
        private IEnumerable<Node> GetNeigboursOfNode(Node node, MazeNodeData nodeData)
        {
            return node.GetAllPossibleNeighbours().Select(x => nodeData.GetNode(x)).Where(x => x != null);
        }

        private int GetSubtractor(int gridSize)
        {
            gridSize = MakeGridSizeUneven(gridSize);
            int divider = 2;
            return (gridSize / divider) + (gridSize % divider);
        }

        private bool HasMinimumRequiredPaths(Node vertex)
        {
            return vertex.Neighbours.Count == vertex.Path.Count || vertex.Path.Count == fMininumRequiredPaths;
        }

        private MazeNodeData InitializeMazeNodeData()
        {
            Dictionary<Location, Node>  nodesByLocation = new Dictionary<Location, Node>();
            Dictionary<int, Node> nodesByIndex = new Dictionary<int, Node>();

            int count = 0;
            for (int z = fGridStart; z <= fGridEnd; z++)
            {
                for (int y = fGridStart; y <= fGridEnd; y++)
                {
                    for (int x = fGridStart; x <= fGridEnd; x++)
                    {
                        count++;
                        Location location = new Location(x, y, z);
                        Node node = new Node() { Id = count, Location = location };

                        nodesByIndex.Add(count, node);
                        nodesByLocation.Add(location, nodesByIndex[count]);
                    }
                }
            }

            return new MazeNodeData(nodesByIndex, nodesByLocation);
        }

        /// <summary>
        /// Fills in the neighbour list of each vertex
        /// </summary>
        private MazeNodeData InitializeNeigbours(MazeNodeData nodeData)
        {
            //initialize neigbours
            foreach (Node node in nodeData.NodesByIndex.Values)
            {
                node.Neighbours = FindNeighbouringNodes(node, nodeData);
            }

            return nodeData;
        }

        /// <summary>
        /// Checks if path is valid. path is not valid if it makes a loop using only 4 nodes.
        /// Recursive method
        /// </summary>
        private bool IsPathValid(Node fromNode, Node toNode, Node nodeToFind, int level, MazeNodeData nodeData)
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
            return gridSize % 2 == 1 ? gridSize : gridSize + 1;
        }

        /// <summary>
        /// Clears all Path data 
        /// </summary>
        private void ResetPathData(MazeNodeData nodeData)
        {
            nodeData.ClearAllPaths();
        }

        /// <summary>
        /// Adds path from given node to destination node
        /// </summary>
        private void SetPath(Node node, int idOfDestinationNode, MazeNodeData nodeData)
        {
            node.Path.Add(idOfDestinationNode);
            nodeData.NodesByIndex[idOfDestinationNode].Path.Add(node.Id);
        }
    }
}
