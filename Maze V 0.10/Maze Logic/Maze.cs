using System.Linq;

namespace MazeV.Maze_Logic
{
    public class Maze
    {
        private UnitFactory fUnitFactory;

        public IUnit Hero { get { return UnitList.GetPlayer(); } }
        public IMazeNodeData NodeData { get; private set; }
        public UnitList UnitList { get; set; }
        public IMazeViewData ViewData { get; private set; }

        public Maze(IMazeNodeData nodeData, IMazeViewData viewData)
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
            INode NodeThatPlayerIsIn = NodeData.NodesByIndex.Where(x => x.Value.Location.Equals( Hero.CurrentLocation)).Select(x => x.Value).FirstOrDefault();           
            NodeThatPlayerIsIn?.CollectablePoint.Collect();
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