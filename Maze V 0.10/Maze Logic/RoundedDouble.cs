using System;

namespace MazeV.Maze_Logic
{
    public sealed class RoundedDouble : IEquatable<RoundedDouble>
    {
        public double Value { get; }

        public RoundedDouble(double value)
        {
            Value = Math.Round(value, 8);
        }

        /// <summary>
        /// Implicit RoundedDouble to double converter. Makes this possible: double num = new RoundedDouble(8);
        /// </summary>
        public static implicit operator double(RoundedDouble value)
        {
            return value.Value;
        }

        /// <summary>
        /// Implicit double to RoundedDouble converter. Makes this possible: RoundedDouble num = 8.0d;
        /// </summary>
        public static implicit operator RoundedDouble(double value)
        {
            return new RoundedDouble(value);
        }

        public static bool operator !=(RoundedDouble x1, RoundedDouble x2)
        {
            return !x1.Equals(x2);
        }

        public static bool operator ==(RoundedDouble x1, RoundedDouble x2)
        {
            return x1.Equals(x2);
        }

        public bool Equals(RoundedDouble other)
        {
            return Math.Round(Value, 8) == Math.Round(other, 8);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RoundedDouble);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}