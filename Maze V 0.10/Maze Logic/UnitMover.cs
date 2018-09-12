using System;

namespace MazeV.Maze_Logic
{
    public class UnitMover
    {
        private readonly IMovementLogic fMovementLogic;

        public UnitMover(IMovementLogic movementLogic)
        {
            fMovementLogic = movementLogic;
        }

        public void MovePlayer(IDirection direction, IUnit player, IMazeNodeData nodeData, IMazeViewData mazeView)
        {
            Action<IDirection, IUnit, IMazeViewData> movementAction = fMovementLogic.GetMovementToPreform(direction, player, nodeData, mazeView);
            movementAction?.Invoke(direction, player, mazeView);
        }
    }
}