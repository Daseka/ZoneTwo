using MazeV.MazeLogic.MazeNodes;
using System.Collections;
using System.Collections.Generic;

namespace MazeV.MazeLogic.Movement
{
    public sealed class MovementCube : IEnumerable<ILocation>, IEnumerator<ILocation>
    {
        private readonly IList<ILocation> _vectors;
        private int _position = -1;

        public object Current
        {
            get
            {
                return _vectors[_position];
            }
        }

        ILocation IEnumerator<ILocation>.Current
        {
            get
            {
                return Current as ILocation;
            }
        }

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

        public ILocation this[int direction] => _vectors[direction];

        public void Dispose()
        {
            // dont know what i should dispose of
        }

        public IEnumerator<ILocation> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool MoveNext()
        {
            _position++;

            return _position < _vectors.Count;
        }

        public void Reset()
        {
            _position = -1;
        }
    }
}