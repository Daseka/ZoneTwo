using System;

namespace MazeV.Maze_Logic
{
    public interface IMovementLogic
    {
        Action<IDirection, IUnit, IMazeViewData> GetMovementToPreform(IDirection direction, IUnit player, MazeNodeData nodeData, IMazeViewData mazeView);
    }
}