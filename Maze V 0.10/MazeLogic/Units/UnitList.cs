using MazeV.MazeLogic.MazeNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MazeV.MazeLogic.Units
{
    public class UnitList : IUnitList
    {
        private readonly Dictionary<Guid, IUnit> _units;

        public int Count => _units.Count;


        public UnitList()
        {
            _units = new Dictionary<Guid, IUnit>();
        }

        public void Add(IUnit unit)
        {
            _units.Add(unit.Id, unit);
        }

        public Player GetPlayer()
        {
            return _units.Values.FirstOrDefault(x => x is Player) as Player;
        }

        public IEnumerable<IUnit> FindUnit(Type type)
        {
            return _units.Values.Where(x => x.GetType() == type);
        }

        public IUnit FindUnit(ILocation location)
        {
            return _units.Values.FirstOrDefault(x => x.CurrentLocation.Equals(location));
        }
    }
}