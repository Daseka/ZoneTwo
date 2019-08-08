using MazeV.MazeLogic.MazeNodes;
using System.Collections.Generic;

namespace MazeV.MazeLogic.Movement
{
    public class MovementCube
    {
        public ILocation this[int direction] => _vectors[direction];

        private readonly IList<ILocation> _vectors;

        public MovementCube()
        {
            _vectors = new List<ILocation>()
            {
                new Location(-1,0,0),
                new Location(1,0,0),
                new Location(0,-1,0),
                new Location(0,1,0),
                new Location(0,0,-1),
                new Location(0,0,1)
            };
        }
    }
}