using MazeV.MazeLogic.MazeNodes;
using MazeV.MazeLogic.Rotation;
using System.Collections.Generic;
using System.Linq;

namespace MazeV.MazeLogic.MazeViews
{
    public class MazeViewData : IMazeViewData
    {
        public IAxis LeftRightRotationAxis { get; set; }

        public IList<INode> MazeNodes { get; set; }

        public List<ILocation> MovementCube { get; set; }

        public IAxis UpDownRotationAxis { get; set; }

        public int ViewEnd { get; set; }

        public int ViewSize { get; set; }

        public int ViewStart { get; set; }

        public MazeViewData(int start, int end, int size, IMazeNodeData nodeData, IAxisFactory axisFactory)
        {
            ViewSize = size;
            ViewEnd = end;
            ViewStart = start;

            UpDownRotationAxis = axisFactory.CreateXAxis();
            LeftRightRotationAxis = axisFactory.CreateYAxis();
            MazeNodes = CreateInitialView(nodeData, 0);

            InitializeMovementCube();
        }

        public INode GetNodeAt(ILocation location)
        {
            return MazeNodes.FirstOrDefault(x => x.Location.Equals(location));
        }

        private IEnumerable<Location> CreateAllNodesForZLevel(int zLevel)
        {
            int count = ViewEnd - ViewStart + 1;
            IEnumerable<int> range = Enumerable.Range(ViewStart, count);

            return range.SelectMany(_ => range, (yAxis, xAxis) => new Location(xAxis, yAxis, zLevel)).ToList();
        }

        private IList<INode> CreateInitialView(IMazeNodeData nodeData, int zLevel)
        {
            IList<INode> nodes = new List<INode>();
            IEnumerable<Location> allCombinations = CreateAllNodesForZLevel(zLevel);
            foreach (var item in allCombinations)
            {
                if (nodeData.NodesByLocation.TryGetValue(item, out INode node))
                {
                    nodes.Add(node);
                }
            }

            return nodes;
        }

        private void InitializeMovementCube()
        {
            MovementCube = new List<ILocation>(){
                                new Location(-1,0,0)
                                ,new Location(1,0,0)
                                ,new Location(0,-1,0)
                                ,new Location(0,1,0)
                                ,new Location(0,0,-1)
                                ,new Location(0,0,1)
                            };
        }
    }
}