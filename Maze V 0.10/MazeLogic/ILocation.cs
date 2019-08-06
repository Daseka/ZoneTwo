using System.Collections.Generic;

namespace MazeV.MazeLogic
{
    public interface ILocation
    {
        RoundedDouble PointX { get; set; }

        RoundedDouble PointY { get; set; }

        RoundedDouble PointZ { get; set; }

        ILocation Add(ILocation location);

        bool Equals(object obj);

        List<ILocation> GetAllPossibleNeighbours();

        ILocation GetCopy();

        int GetHashCode();

        string ToString();
    }
}