namespace MazeV.Maze_Logic
{
    public interface IAxisFactory
    {
        IAxis CreateXAxis();

        IAxis CreateYAxis();

        IAxis CreateZAxis();
    }
}