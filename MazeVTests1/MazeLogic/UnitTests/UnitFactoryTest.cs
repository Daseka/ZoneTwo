using MazeV.MazeLogic;
using MazeV.MazeLogic.Units;
using Xunit;

namespace MazeVTests1.MazeLogic
{
    public class UnitFactoryTest
    {
        private readonly UnitFactory _unitFactory;

        public UnitFactoryTest()
        {
            _unitFactory = new UnitFactory();
        }

        [Fact]
        public void ShouldCreateRandomUnit()
        {           
            IUnit randomUnit = _unitFactory.CreateRandomUnit();

            Assert.NotNull(randomUnit);
        }

        [Fact]
        public void ShouldCreatePlayer()
        {
            IUnit createdUnit = _unitFactory.CreateUnit(UnitType.Player);

            Assert.True(createdUnit is Player);
        }

        [Fact]
        public void ShouldCreateMonster()
        {
            IUnit createdUnit = _unitFactory.CreateUnit(UnitType.Monster);

            Assert.True(createdUnit is Monster);
        }

        [Fact]
        public void ShouldNotCreate()
        {
            IUnit createdUnit = _unitFactory.CreateUnit(UnitType.Skeleton);

            Assert.True(createdUnit is Monster);
        }
    }
}