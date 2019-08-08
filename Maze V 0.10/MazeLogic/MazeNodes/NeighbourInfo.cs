using MazeV.MazeLogic.Rotation;

namespace MazeV.MazeLogic.MazeNodes
{
    public class NeighbourInfo
    {
        public IAxis Axis { get; set; }

        public int Id { get; set; }

        public ILocation Location { get; set; }
    }
}