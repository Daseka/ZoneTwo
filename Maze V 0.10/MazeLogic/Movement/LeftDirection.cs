namespace MazeV.MazeLogic.Movement
{
    public class LeftDirection : IDirection
    {
        public IDirection ReverseDirection => new RightDirection();

        public int Value => 0;
    }
}