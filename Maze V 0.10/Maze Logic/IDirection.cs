using System;


namespace MazeV.Maze_Logic
{
    public interface IDirection
    {
        int Value { get; }
        IDirection ReverseDirection { get; }
    }
   
    public class LeftDirection : IDirection
    {
        public int Value => 0;
        public IDirection ReverseDirection => new RightDirection();       
    }

    public class RightDirection : IDirection
    {
        public int Value => 1;
        public IDirection ReverseDirection => new LeftDirection();       
    }

    public class UpDirection : IDirection
    {
        public int Value => 2;
        public IDirection ReverseDirection => new DownDirection();
    }

    public class DownDirection : IDirection
    {
        public int Value => 3;
        public IDirection ReverseDirection => new UpDirection();
    }
}
