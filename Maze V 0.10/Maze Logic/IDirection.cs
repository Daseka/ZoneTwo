namespace MazeV.Maze_Logic
{
    public interface IDirection
    {
        IDirection ReverseDirection { get; }

        int Value { get; }
    }

    public class DownDirection : IDirection
    {
        public IDirection ReverseDirection => new UpDirection();

        public int Value => 3;
    }

    public class LeftDirection : IDirection
    {
        public IDirection ReverseDirection => new RightDirection();

        public int Value => 0;
    }

    public class RightDirection : IDirection
    {
        public IDirection ReverseDirection => new LeftDirection();

        public int Value => 1;
    }

    public class UpDirection : IDirection
    {
        public IDirection ReverseDirection => new DownDirection();

        public int Value => 2;
    }
}