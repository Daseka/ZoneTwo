namespace MazeV.MazeLogic
{
    public interface IAxisFactory
    {
        IAxis CreateXAxis();

        IAxis CreateYAxis();

        IAxis CreateZAxis();
    }
}