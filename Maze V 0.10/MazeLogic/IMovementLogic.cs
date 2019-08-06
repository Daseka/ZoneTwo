using MazeV.MazeLogic.Units;
using System;

namespace MazeV.MazeLogic
{
    public interface IMovementLogic
    {
        Action<IDirection, IUnit, IMazeViewData> GetMovementToPreform(IDirection direction, IUnit player, IMazeNodeData nodeData, IMazeViewData mazeView);
    }
}