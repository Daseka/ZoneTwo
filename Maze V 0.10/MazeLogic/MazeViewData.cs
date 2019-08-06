using System.Collections.Generic;
using System.Linq;

namespace MazeV.MazeLogic
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
            MazeNodes = new List<INode>();

            CreateInitialView(nodeData, 0);
            InitializeMovementCube();
        }

        public INode GetNodeAt(ILocation location)
        {
            return MazeNodes.FirstOrDefault(x => x.Location.Equals(location));
        }

        private void CreateInitialView(IMazeNodeData nodeData, int zLevel)
        {
            for (int y = ViewStart; y <= ViewEnd; y++)
            {
                for (int x = ViewStart; x <= ViewEnd; x++)
                {
                    if (nodeData.NodesByLocation.TryGetValue(new Location(x, y, zLevel), out INode node))
                        MazeNodes.Add(node);
                }
            }
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