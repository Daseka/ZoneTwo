﻿namespace MazeV.MazeLogic
{
    public class DownRotation : IRotation
    {
        public double GetAngle()
        {
            return -90;
        }

        public IAxis GetRotationAxis(IMazeViewData mazeViewData)
        {
            return mazeViewData.UpDownRotationAxis;
        }

        public void SetRotationAxisForDirection(IMazeViewData mazeViewData, IAxis freeAxis)
        {
            mazeViewData.LeftRightRotationAxis = freeAxis;
        }
    }
}