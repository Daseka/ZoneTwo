using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MazeV.Maze_Logic
{
    [Serializable]
    public class UnitList : Dictionary<Guid, IUnit>
    {
        public UnitList()
        {
            // empty constructor. needed because of serialization constructor
        }

        protected UnitList(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            // sonarqube requested it
        }

        public void Add(IUnit unit)
        {
            Add(unit.Id, unit);
        }

        public Player GetPlayer()
        {
            return Values.FirstOrDefault(x => x is Player) as Player;
        }
    }
}