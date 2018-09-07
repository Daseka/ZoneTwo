using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeV.Maze_Logic
{
    public class UnitMover
    {
        IMovementLogic fMovementLogic;

        public UnitMover(IMovementLogic movementLogic)
        {
            fMovementLogic = movementLogic;
        }

        public void MovePlayer(Direction direction, Player player, MazeNodeData nodeData, MazeViewData mazeView)
        {
            Action<Direction, Player, MazeViewData> movementAction = fMovementLogic.GetMovementToPreform(direction, player, nodeData, mazeView);
            movementAction?.Invoke(direction, player, mazeView);                      
        }       
    }
}