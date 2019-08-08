using MazeV.MazeLogic.MazeViews;

namespace MazeV.MazeLogic.Rotation
{
    public interface IRotation
    {
        double GetAngle();

        IAxis GetRotationAxis(IMazeViewData mazeViewData);

        void SetRotationAxisForDirection(IMazeViewData mazeViewData, IAxis freeAxis);
    }
}