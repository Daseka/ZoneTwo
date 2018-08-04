using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeV.Maze_Logic
{
    public class UnitList : Dictionary<Guid, IUnit>
    {
        public Player GetPlayer()
        {
            return Values.FirstOrDefault(x => x is Player) as Player;
        }

        public void Add(IUnit unit)
        {
            Add(unit.Id, unit);
        }
    }
}
