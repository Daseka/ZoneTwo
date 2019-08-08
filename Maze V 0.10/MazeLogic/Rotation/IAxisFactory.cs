namespace MazeV.MazeLogic.Rotation
{
    public interface IAxisFactory
    {
        IAxis CreateXAxis();

        IAxis CreateYAxis();

        IAxis CreateZAxis();
    }
}