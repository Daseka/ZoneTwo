using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeV.Maze_Logic
{
    public class Player : BaseUnit
    {
        public Player()
        {
            //empty constructor
        }
        public Axis ViewingAxis { get; set; }

    }
}
