using MazeV.MazeLogic.Movement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeV.MazeLogic.MazeNodes
{
    public sealed class Location : ILocation, IEquatable<Location>
    {
        private static readonly ILocation[] _vectors = new ILocation[]
        {
            new Location(1,0,0),
            new Location(-1,0,0),
            new Location(0,1,0),
            new Location(0,-1,0),
            new Location(0,0,1),
            new Location(0,0,-1),
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

        public Location(double pointX, double pointY, double pointZ)
        {
            PointX = pointX;
            PointY = pointY;
            PointZ = pointZ;
        }

        public ILocation Add(ILocation location)
        {
            double pointX = PointX + location.PointX;
            double pointY = PointY + location.PointY;
            double pointZ = PointZ + location.PointZ;

            var newLocation = new Location()
            {
                PointX = pointX,
                PointY = pointY,
                PointZ = pointZ,
            };

            return newLocation;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as Location);
        }

        public bool Equals(Location other)
        {
            return PointX == other?.PointX
                && PointY == other?.PointY
                && PointZ == other?.PointZ;
        }

        public IEnumerable<ILocation> GetAllPossibleNeighbours()
        {
            IEnumerable<ILocation> temp = _vectors.Select(item => Add(item));
            return temp.ToList();
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