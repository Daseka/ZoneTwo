using System;
using System.Collections.Generic;

namespace MazeV.Maze_Logic
{
    public class Location : IEquatable<Location>
    {
        private static Location[] fVectors = new[]{
                                                new Location(1,0,0),new Location(-1,0,0),
                                                new Location(0,1,0),new Location(0,-1,0),
                                                new Location(0,0,1),new Location(0,0,-1)
                                             };

        public RoundedDouble PointX { get; set; }

        public RoundedDouble PointY { get; set; }

        public RoundedDouble PointZ { get; set; }

        public Location()
        {
            PointX = 0;
            PointY = 0;
            PointZ = 0;
        }

        public Location(double x, double y, double z)
        {
            PointX = x;
            PointY = y;
            PointZ = z;
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

        public static bool operator ==(Location a, Location b)
        {
            return a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return Equals(obj as Location);
        }

        public bool Equals(Location location)
        {
            return (PointX == location?.PointX && PointY == location?.PointY && PointZ == location?.PointZ);
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
    }
}