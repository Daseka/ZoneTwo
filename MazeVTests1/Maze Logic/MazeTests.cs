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
    public class MazeTests
    {
        [TestMethod()]
        public void InitializeTest()
        {
            Maze maze = new Maze();
            maze.Initialize(3, 3);

            int numberOfNodesTotal = maze.NodesByIndex.Count;
            int numberOfNodesView = maze.MazeView.Count;
            int numberOfUnits = maze.UnitList.Count;

            Assert.AreEqual(1, numberOfUnits, $"Only the player should be created. Unit count should be 1 : {numberOfUnits}");
            Assert.AreEqual(9, numberOfNodesView, $"The 2D view is 3 by 3 plane. Node count should be 9 : {numberOfNodesView}");
            Assert.AreEqual(27, numberOfNodesTotal, $"The 3D maze is a 3 by 3 by 3 cube. Node count should be 27 : {numberOfNodesTotal}");
        }

        [TestMethod()]
        public void GenerateLayoutDataTest()
        {
            Maze maze = new Maze();
            bool isLayoutValid = maze.GenerateLayoutData(12345);

            Assert.IsTrue(!isLayoutValid, $"Maze not Initialized isLayoutValid should be false : {isLayoutValid}");

            maze.Initialize(3, 3);
            isLayoutValid = maze.GenerateLayoutData(12345);

            Assert.IsTrue(isLayoutValid, $"Maze is Initialized isLayoutValid should be true : {isLayoutValid}");
        }

        [TestMethod()]
        public void ProcessPlayerInNodeTest()
        {
            Maze maze = new Maze();
            maze.Initialize(3, 3);

            int totalNodeCount = maze.GetTotalNodeCount();
            int emptyNodeCount = maze.GetEmptyNodeCount();

            Assert.AreEqual(27,totalNodeCount, $"The 3D maze is a 3 by 3 by 3 cube. Node count should be 27 : {totalNodeCount}");
            Assert.AreEqual(0, emptyNodeCount, $"Player in node has not been procesed. Empty nodes should be 0: {emptyNodeCount}");

            maze.ProcessPlayerInNode();
            emptyNodeCount = maze.GetEmptyNodeCount();

            Assert.AreEqual(1, emptyNodeCount, $"Player in node has not been procesed. Empty nodes should be 0: {emptyNodeCount}");
        }
    }
}