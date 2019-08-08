using MazeV.MazeLogic;
using MazeV.MazeLogic.Units;
using System.Linq;
using Xunit;

namespace MazeVTests1.MazeLogic
{
    public class UnitListTest
    {
        [Fact]
        public void ShouldGetPlayerOne()
        {
            var unitList = new UnitList();
            var playerOne = new Player() { Name = "One", CurrentLocation = new Location(1, 1, 1) };
            var playerTwo = new Player() { Name = "Two", CurrentLocation = new Location(2, 2, 2) };

            unitList.Add(playerOne);
            unitList.Add(playerTwo);

            var foundPlayer = unitList.GetPlayer();

            Assert.Equal(playerOne, foundPlayer);
        }


        [Fact]
        public void ShouldNotGetPlayerOne()
        {
            var unitList = new UnitList();
            var playerOne = new Player() { Name = "One", CurrentLocation = new Location(1, 1, 1) };
            var playerTwo = new Player() { Name = "Two", CurrentLocation = new Location(2, 2, 2) };

            unitList.Add(playerTwo);
            unitList.Add(playerOne);

            var foundPlayer = unitList.GetPlayer();

            Assert.NotEqual(playerOne, foundPlayer);
        }

        [Fact]
        public void ShouldFindTwo()
        {
            var unitList = new UnitList();
            var player = new Player() { CurrentLocation = new Location(1, 0, 0) };
            var monsterOne = new Monster() { CurrentLocation = new Location(2, 0, 0) };
            var monsterTwo = new Monster() { CurrentLocation = new Location(3, 0, 0) };

            unitList.Add(player);
            unitList.Add(monsterOne);
            unitList.Add(monsterTwo);

            var foundUnits = unitList.FindUnit(typeof(Monster));

            Assert.True(foundUnits.Count() == 2);
        }

        [Fact]
        public void ShouldFindMonster()
        {
            var unitList = new UnitList();
            var player = new Player() { CurrentLocation = new Location(1, 0, 0) };            
            var monsterTwo = new Monster() { CurrentLocation = new Location(3, 0, 0) };

            unitList.Add(player);            
            unitList.Add(monsterTwo);

            var foundUnit = unitList.FindUnit(new Location(3,0,0));

            Assert.True(foundUnit is Monster);
        }

        [Fact]
        public void ShouldNotFind()
        {
            var unitList = new UnitList();
            var player = new Player() { CurrentLocation = new Location(1, 0, 0) };
            var monsterTwo = new Monster() { CurrentLocation = new Location(3, 0, 0) };

            unitList.Add(player);
            unitList.Add(monsterTwo);

            var foundUnit = unitList.FindUnit(new Location(2, 2, 2));

            Assert.Null(foundUnit);
        }
    }
}