using System;

namespace MazeV.MazeLogic.Movement.Directions
{
    public interface IDirection : IEquatable<IDirection>
    {
        IDirection ReverseDirection { get; }

        int Value { get; }
    }
}