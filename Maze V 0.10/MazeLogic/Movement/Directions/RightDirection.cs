namespace MazeV.MazeLogic.Movement.Directions
{
    public sealed class RightDirection : IDirection
    {
        public IDirection ReverseDirection => new LeftDirection();

        public int Value => 1;

        public bool Equals(IDirection other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RightDirection);
        }

        public override int GetHashCode()
        {
            return Value;
        }
    }
}