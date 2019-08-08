namespace MazeV.MazeLogic
{
    public class LeftRotation : IRotation
    {
        public double GetAngle()
        {
            return -90;
        }

        public IAxis GetRotationAxis(IMazeViewData mazeViewData)
        {
            return mazeViewData.LeftRightRotationAxis;
        }

        public void SetRotationAxisForDirection(IMazeViewData mazeViewData, IAxis freeAxis)
        {
            mazeViewData.UpDownRotationAxis = freeAxis;
        }
    }
}