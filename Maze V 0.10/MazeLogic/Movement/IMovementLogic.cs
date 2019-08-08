using MazeV.MazeLogic.MazeNodes;
using MazeV.MazeLogic.MazeViews;
using MazeV.MazeLogic.Units;
using System;

namespace MazeV.MazeLogic.Movement
{
    public interface IMovementLogic
    {
        Action<IDirection, IUnit, IMazeViewData> GetMovementToPreform(IDirection direction, IUnit player, IMazeNodeData nodeData, IMazeViewData mazeView);
    }
}