namespace MazeV.MazeLogic.Movement
{
    public class DownDirection : IDirection
    {
        public IDirection ReverseDirection => new UpDirection();

        public int Value => 3;
    }
}