using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MazeV.Maze_Logic
{
    /// <summary>
    /// Enum of all the UnitTypes available. Must have exactly the same name as 
    /// the corresponding Class to create unit with CreatUnit() method
    /// </summary>
    public enum UnitType
    {
        Player = 1,
        Monster = 2,
        Skeleton = 3,
    }

    public class UnitFactory
    {
        private Random fRandomizer = new Random(12345);
        public IUnit CreateRandomUnit()
        {
            int min = 1;
            int max = Enum.GetValues(typeof(UnitType)).Length + 1;

            int index = fRandomizer.Next(min, max);

            return CreateUnit((UnitType)index);
        }

        public IUnit CreateUnit(UnitType unitType)
        {
            string typeName = unitType.ToString();
            Type[] typeList = Assembly.GetExecutingAssembly().GetTypes();
            Type type = typeList.FirstOrDefault(n => n.Name == typeName);

            object unit = Activator.CreateInstance(type);
            if (!(unit is IUnit))
                return null;

            return unit as IUnit;
        }
    }
}
