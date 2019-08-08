using MazeV.MazeLogic.MazeNodes;
using MazeV.MazeLogic.MazeViews;
using MazeV.MazeLogic.Units;
using System;

namespace MazeV.MazeLogic.Movement
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