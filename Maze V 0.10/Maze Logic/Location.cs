using System;
using System.Collections.Generic;

namespace MazeV.Maze_Logic
{
    public sealed class Location :  ILocation
    {
        private readonly static ILocation[] fVectors = new ILocation[]{
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

        public ILocation Add(ILocation location)
        {
            Location newLocation = new Location()
                                    {
                                        PointX = PointX + location.PointX,
                                        PointY = PointY + location.PointY,
                                        PointZ = PointZ + location.PointZ,
                                    };
            return newLocation;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return Equals(obj as Location);
        }

        public bool Equals(ILocation other)
        {
            return Equals(other as Location);
        }

        public bool Equals(Location other)
        {
            return (PointX == other?.PointX && PointY == other?.PointY && PointZ == other?.PointZ);
        }

        public List<ILocation> GetAllPossibleNeighbours()
        {
            List<ILocation> neighbours = new List<ILocation>();

            foreach (Location location in fVectors)
            {
                ILocation neighbour = Add(location);
                neighbours.Add(neighbour);
            }

            return neighbours;
        }

        public ILocation GetCopy()
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