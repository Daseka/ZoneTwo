using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace MazeV.Maze_Logic
{
    public class MazeViewData : List<Node>
    {
        public int ViewSize { get; set; }
        public int ViewStart { get; set; }
        public int ViewEnd { get; set; }
        public Axis UpDownRotationAxis { get; set; }
        public Axis LeftRightRotationAxis { get; set; }
        public List<Location> MovementCube { get; set; }

        public MazeViewData(int start, int end, int size,MazeNodeData nodeData)
        {
            ViewSize = size;
            ViewEnd = end;
            ViewStart = start;

            UpDownRotationAxis = Axis.XAxis;
            LeftRightRotationAxis = Axis.YAxis;

            CreateInitialView(nodeData,0);
            InitializeMovementCube();
        }

        private void InitializeMovementCube()
        {
            MovementCube = new List<Location>(){
                                new Location(-1,0,0)
                                ,new Location(1,0,0)
                                ,new Location(0,-1,0)
                                ,new Location(0,1,0)
                                ,new Location(0,0,-1)
                                ,new Location(0,0,1)
                            };
        }     

        private void CreateInitialView(MazeNodeData nodeData, int zLevel)
        {
            for (int y = ViewStart; y <= ViewEnd; y++)
            {
                for (int x = ViewStart; x <= ViewEnd; x++)
                {
                    if (nodeData.NodesByLocation.TryGetValue(new Location(x,y, zLevel), out Node node))                                         
                        this.Add(node);
                }
            }
        }

        public Node GetNodeAt(Location location)
        {
            return this.FirstOrDefault(x => x.Location == location);            
        }            
    }
}
