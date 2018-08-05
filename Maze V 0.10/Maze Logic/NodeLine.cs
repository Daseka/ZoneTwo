using System;
using System.Collections.Generic;
using System.Drawing;

namespace MazeV.Maze_Logic
{
    internal class NodeLine : IEquatable<NodeLine>
    {
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }

        public bool Equals(NodeLine line)
        {
            return Point1.Equals(line?.Point1) && Point2.Equals(line?.Point2)
                   || Point2.Equals(line?.Point1) && Point1.Equals(line?.Point2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as NodeLine);
        }

        public override int GetHashCode()
        {
            var hashCode = 363529913;
            hashCode = hashCode * -1521134295 + EqualityComparer<Point>.Default.GetHashCode(Point1);
            hashCode = hashCode * -1521134295 + EqualityComparer<Point>.Default.GetHashCode(Point2);
            return hashCode;
        }

        public override string ToString()
        {
            return string.Format("{0} : {1}", Point1, Point2);
        }
    }
}