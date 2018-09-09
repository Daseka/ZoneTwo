using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeV.Maze_Logic
{
    public class NeighbourInfo
    {
        public int Id { get; set; }
        public Axis Axis { get; set; }        
        public ILocation Location { get; set; }
    }
}
