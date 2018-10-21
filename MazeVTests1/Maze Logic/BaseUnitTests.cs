using MazeV.Maze_Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeV.Maze_Logic.Tests
{
    [TestClass()]
    public class BaseUnitTests
    {
        [TestMethod()]
        public void AssignLocationTest()
        {
            BaseUnit baseUnit = new BaseUnit();
            Location newLocation = new Location(1, 2, 3);
            int x = 5;

            Assert.IsTrue(baseUnit.CurrentLocation.Equals(new Location(0, 0, 0)));

            baseUnit.AssignLocation(newLocation);
            Assert.IsTrue(baseUnit.CurrentLocation.Equals(newLocation));
        }

        [TestMethod()]
        public void DrawTest()
        {
            Assert.IsTrue(1 == 1);
        }
    }
}