﻿using System.Linq;

namespace MazeV.Maze_Logic
{
    public class Maze
    {
        private UnitFactory fUnitFactory;

        public Player Hero { get { return UnitList.GetPlayer(); } }
        public MazeNodeData NodeData { get; private set; }
        public UnitList UnitList { get; set; }
        public MazeViewData ViewData { get; private set; }

        public Maze(MazeNodeData nodeData, MazeViewData viewData)
        {
            NodeData = nodeData;
            ViewData = viewData;
        }

        /// <summary>
        /// Initializes the Node Location list and the Node Id list
        /// </summary>
        /// <param name="gridSize"></param>
        public void Initialize()
        {
            UnitList = new UnitList();
            fUnitFactory = new UnitFactory();

            SpawnUnitAtLocation(UnitType.Player, new Location());
        }

        public void ProcessPlayerInNode()
        {
            Node NodeThatPlayerIsIn = NodeData.NodesByIndex.Where(x => x.Value.Location == Hero.CurrentLocation).Select(x => x.Value).FirstOrDefault();
            if (NodeThatPlayerIsIn == null)
                return;

            NodeThatPlayerIsIn.CollectablePoint.Collect();
        }

        private void SpawnUnitAtLocation(UnitType unitType, Location location)
        {
            IUnit spawn = fUnitFactory.CreateUnit(unitType);
            if (spawn == null)
                return;

            if (Validator.IsLocationOccupied(location, UnitList))
                return;

            if (unitType == UnitType.Player && Validator.IsPlayerMaximumReached(UnitList))
                return;

            spawn.AssignLocation(location);
            UnitList.Add(spawn);
        }
    }
}