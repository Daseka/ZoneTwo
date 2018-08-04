using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeV.Maze_Logic
{
    class NodeLine
    {
        public Point Point1{ get; set; }
        public Point Point2 { get; set; }

        public override string ToString()
        {
            return string.Format("{0} : {1}", Point1, Point2);
        }

        public bool Equals(NodeLine line)
        {
            if (Point1.Equals(line.Point1) && Point2.Equals(line.Point2) ||
                Point2.Equals(line.Point1) && Point1.Equals(line.Point2))
                return true;
            return false;
        }
    }
}
