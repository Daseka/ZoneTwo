﻿using System.Collections.Generic;
using Xunit;
using MazeV.MazeLogic;
using MazeV.MazeLogic.Units;

namespace MazeVTests1.MazeLogic
{

    public class MazeTests
    {        
        public void InitializeTest()
        {
            IAxisFactory axisFactory = new AxisFactory();
            IMazeViewDataFactory mazeViewDataFactory = new MazeViewDataFactory();
            var settings = new DefaultSettings();
            var nodeBuilder = new NodeBuilder(settings, new CoinBuilder(settings));
            var randomizer = new Randomizer();
            var nodeDataBuilder = new MazeNodeDataBuilder(new FakeMazeNodeDataBuilderSettings(3,3), randomizer, nodeBuilder);
            IMazeNodeData nodeData = nodeDataBuilder.GenerateNodeData(12345);
            IMazeViewData viewData = nodeDataBuilder.GenerateViewData(nodeData, axisFactory, mazeViewDataFactory);
            var maze = new Maze(new UnitList(), new UnitFactory(randomizer));
            maze.Initialize(nodeData, viewData);

            int numberOfNodesTotal = maze.NodeData.Count;
            int numberOfNodesView = maze.ViewData.MazeNodes.Count;
            int numberOfUnits = maze.UnitList.Count;

            Assert.Equal(1, numberOfUnits);
            Assert.Equal(9, numberOfNodesView);
            Assert.Equal(27, numberOfNodesTotal);
        }

        //    [TestMethod()]
        //    public void GenerateLayoutDataTest()
        //    {
        //        Maze maze = new Maze();
        //        bool isLayoutValid = maze.GenerateLayoutData(12345);

        //        Assert.IsTrue(!isLayoutValid, $"Maze not Initialized isLayoutValid should be false : {isLayoutValid}");

        //        maze.Initialize(3, 3);
        //        isLayoutValid = maze.GenerateLayoutData(12345);

        //        Assert.IsTrue(isLayoutValid, $"Maze is Initialized isLayoutValid should be true : {isLayoutValid}");
        //    }

        //    [TestMethod()]
        //    public void ProcessPlayerInNodeTest()
        //    {
        //        Maze maze = new Maze();
        //        maze.Initialize(3, 3);

        //        int totalNodeCount = maze.GetTotalNodeCount();
        //        int emptyNodeCount = maze.GetEmptyNodeCount();

        //        Assert.AreEqual(27,totalNodeCount, $"The 3D maze is a 3 by 3 by 3 cube. Node count should be 27 : {totalNodeCount}");
        //        Assert.AreEqual(0, emptyNodeCount, $"Player in node has not been procesed. Empty nodes should be 0: {emptyNodeCount}");

        //        maze.ProcessPlayerInNode();
        //        emptyNodeCount = maze.GetEmptyNodeCount();

        //        Assert.AreEqual(1, emptyNodeCount, $"Player in node has not been procesed. Empty nodes should be 0: {emptyNodeCount}");
        //    }
    }
}