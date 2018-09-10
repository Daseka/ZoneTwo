using System;

namespace MazeV.Maze_Logic
{
    public interface IRotation
    {
        IAxis GetRotationAxis(IMazeViewData mazeViewData);
        void SetRotationAxisForDirection(IMazeViewData mazeViewData, IAxis freeAxis);
        double GetAngle();
    }
}