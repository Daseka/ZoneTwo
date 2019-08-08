namespace MazeV.MazeLogic.Movement
{
    public class UpDirection : IDirection
    {
        public IDirection ReverseDirection => new DownDirection();

        public int Value => 2;
    }
}