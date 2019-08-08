using MazeV.MazeLogic.Units;
using System;
using System.Linq;
using System.Reflection;

namespace MazeV.MazeLogic
{
    public class UnitFactory
    {
        private readonly Random fRandomizer;

        public UnitFactory(Randomizer randomizer)
        {
            fRandomizer = randomizer.GenerateRandom(12345);
        }

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
            var typeList = Assembly.GetExecutingAssembly().GetTypes().Where( item => typeof(IUnit).IsAssignableFrom(item));
            Type type = typeList.FirstOrDefault(n => n.Name == typeName);
           
           return type is null 
                ? null
                : Activator.CreateInstance(type) as IUnit;            
        }
    }
}