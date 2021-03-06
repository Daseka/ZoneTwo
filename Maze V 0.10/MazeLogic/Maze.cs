﻿using MazeV.MazeLogic.Units;
using System.Linq;

namespace MazeV.MazeLogic
{
    public class Maze
    {
        private UnitFactory _unitFactory;

        public IUnit Hero { get { return UnitList.GetPlayer(); } }

        public IMazeNodeData NodeData { get; private set; }

        public IUnitList UnitList { get; set; }

        public IMazeViewData ViewData { get; private set; }

        public Maze(IUnitList unitList, UnitFactory unitFactory)
        {
            UnitList = unitList;
            _unitFactory = unitFactory;            
        }

        /// <summary>
        /// Initializes the Node Location list and the Node Id list
        /// </summary>
        /// <param name="gridSize"></param>
        public void Initialize(IMazeNodeData nodeData, IMazeViewData viewData)
        {
            NodeData = nodeData;
            ViewData = viewData;

            SpawnUnitAtLocation(UnitType.Player, new Location());
        }

        public void ProcessPlayerInNode()
        {
            INode NodeThatPlayerIsIn = NodeData.NodesByIndex.Where(x => x.Value.Location.Equals(Hero.CurrentLocation)).Select(x => x.Value).FirstOrDefault();
            NodeThatPlayerIsIn?.CollectablePoint.Collect();
        }

        private void SpawnUnitAtLocation(UnitType unitType, Location location)
        {
            IUnit spawn = _unitFactory.CreateUnit(unitType);
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