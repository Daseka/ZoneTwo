namespace MazeV.MazeLogic
{
    public interface IRotation
    {
        double GetAngle();

        IAxis GetRotationAxis(IMazeViewData mazeViewData);

        void SetRotationAxisForDirection(IMazeViewData mazeViewData, IAxis freeAxis);
    }
}