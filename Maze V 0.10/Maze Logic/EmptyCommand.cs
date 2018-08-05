namespace MazeV.Maze_Logic
{
    public class EmptyCommand : ICommand
    {
        //Empty command instead of null
        public void Execute() { }
    }
}