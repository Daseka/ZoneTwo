namespace MazeV.MazeLogic.Movement
{
    public class RightDirection : IDirection
    {
        public IDirection ReverseDirection => new LeftDirection();

        public int Value => 1;
    }
}