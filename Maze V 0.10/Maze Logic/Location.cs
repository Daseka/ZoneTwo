using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeV.Maze_Logic
{
           
    public class Location
    {
        private static Location[] fVectors = new Location[]{
                                                new Location(1,0,0),new Location(-1,0,0),
                                                new Location(0,1,0),new Location(0,-1,0),
                                                new Location(0,0,1),new Location(0,0,-1)
                                             };
        

        public double PointX { get; set; }
        public double PointY { get; set; }
        public double PointZ { get; set; }

        public Location()
        {
            PointX = 0;
            PointY = 0;
            PointZ = 0;
        }

        public Location(double x, double y = 0, double z = 0)
        {
            PointX = x;
            PointY = y;
            PointZ = z;
        }

        public static bool operator ==(Location a, Location b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Location a, Location b)
        {
            return !(a == b);
        }

        public static Location operator +(Location a, Location b)
        {
            Location location = new Location();
            location.PointX = a.PointX + b.PointX;
            location.PointY = a.PointY + b.PointY;
            location.PointZ = a.PointZ + b.PointZ;

            return location;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Location location = obj as Location;
            if (location == null)
                return false;

            return (PointX == location.PointX && PointY == location.PointY && PointZ == location.PointZ);
        }

        public Location GetCopy()
        {
            return new Location(PointX, PointY, PointZ);
        }

        public override int GetHashCode()
        {
            return (int)(PointX + PointY + PointZ);
        }

        public override string ToString()
        {
            return string.Format("x={0} y={1} z={2}", PointX, PointY, PointZ);
        }

        public List<Location> GetAllPossibleNeighbours()
        {
            List<Location> neighbours = new List<Location>();

            foreach (Location vector in Location.fVectors)
            {
                Location neighbour = this + vector;
                neighbours.Add(neighbour);
                
            }

            return neighbours;
        }
    }
}
