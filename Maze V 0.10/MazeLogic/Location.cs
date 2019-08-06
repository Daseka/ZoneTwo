using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeV.MazeLogic
{
    public sealed class Location : ILocation, IEquatable<Location>
    {
        private static readonly ILocation[] fVectors = new ILocation[]{
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
            var newLocation = new Location()
            {
                PointX = PointX + location.PointX,
                PointY = PointY + location.PointY,
                PointZ = PointZ + location.PointZ,
            };
            return newLocation;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as Location);
        }

        public bool Equals(Location other)
        {
            return PointX == other?.PointX && PointY == other?.PointY && PointZ == other?.PointZ;
        }

        public List<ILocation> GetAllPossibleNeighbours()
        {
            return (from ILocation location in fVectors select Add(location)).ToList();
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