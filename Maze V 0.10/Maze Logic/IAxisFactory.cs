namespace MazeV.Maze_Logic
{
    public interface IAxisFactory
    {
        IAxis CreateZAxis();
        IAxis CreateXAxis();
        IAxis CreateYAxis();
    }
}
