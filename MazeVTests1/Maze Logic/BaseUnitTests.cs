using Microsoft.VisualStudio.TestTools.UnitTesting;
using MazeV.Maze_Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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