using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeV.Maze_Logic
{
    public class Maze
    {              
        private UnitFactory fUnitFactory;
        private MazeViewRotator fMazeViewRotator;
        private UnitMover fUnitMover;

        public MazeNodeData NodeData { get; private set; }
        public UnitList UnitList { get; set; }        
        public MazeViewData ViewData { get; private set; }
        public Player Hero { get { return UnitList.GetPlayer(); }  }

        public Maze(MazeNodeData nodeData, MazeViewData viewData)
        {
            NodeData = nodeData;
            ViewData = viewData;
        }                                                          

        private bool SpawnUnitAtLocation(UnitType unitType, Location location)
        {
            IUnit spawn = fUnitFactory.CreateUnit(unitType);
            if (spawn == null)
                return false;

            if (Validator.IsLocationOccupied(location, UnitList))
                return false;

            if (unitType == UnitType.Player && Validator.IsPlayerMaximumReached(UnitList))
                return false;

            spawn.AssignLocation(location);
            UnitList.Add(spawn);
            return true;
        }        
        
        /// <summary>
        /// Initializes the Node Location list and the Node Id list 
        /// </summary>
        /// <param name="gridSize"></param>
        public void Initialize()
        {
            UnitList = new UnitList();
            fUnitFactory = new UnitFactory();                                                         
            fMazeViewRotator = new MazeViewRotator();

            fUnitMover = new UnitMover();
            SpawnUnitAtLocation(UnitType.Player, new Location());
        }                

        public void ProcessPlayerInNode()
        {
            Node NodeThatPlayerIsIn = NodeData.NodesByIndex.Where(x => x.Value.Location == Hero.CurrentLocation).Select(x => x.Value).FirstOrDefault();
            if (NodeThatPlayerIsIn == null)
                return;

            if (NodeThatPlayerIsIn.CollectablePoint > 0)
                NodeThatPlayerIsIn.CollectablePoint = 0;
        }                             
    }
}
