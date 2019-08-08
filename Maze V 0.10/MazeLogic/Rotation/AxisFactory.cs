namespace MazeV.MazeLogic.Rotation
{
    public class AxisFactory : IAxisFactory
    {
        public IAxis CreateXAxis()
        {
            return new XAxis();
        }

        public IAxis CreateYAxis()
        {
            return new YAxis();
        }

        public IAxis CreateZAxis()
        {
            return new ZAxis();
        }
    }
}