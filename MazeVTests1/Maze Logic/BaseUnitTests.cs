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

            Assert.IsTrue(baseUnit.CurrentLocation.Equals(new Location(0, 0, 0)));

            baseUnit.AssignLocation(newLocation);
            Assert.IsTrue(baseUnit.CurrentLocation.Equals(newLocation));
        }
    }
}