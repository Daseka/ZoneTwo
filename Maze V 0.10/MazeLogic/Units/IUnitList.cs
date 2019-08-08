using MazeV.MazeLogic.MazeNodes;
using System;
using System.Collections.Generic;

namespace MazeV.MazeLogic.Units
{
    public interface IUnitList
    {
        int Count { get; }

        void Add(IUnit unit);
        IEnumerable<IUnit> FindUnit(Type type);
        IUnit FindUnit(ILocation location);
        Player GetPlayer();
    }
}