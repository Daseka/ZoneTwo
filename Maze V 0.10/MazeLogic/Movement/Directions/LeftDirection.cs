namespace MazeV.MazeLogic.Movement.Directions
{
    public sealed class LeftDirection : IDirection
    {
        public IDirection ReverseDirection => new RightDirection();

        public int Value => 0;

        public bool Equals(IDirection other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LeftDirection);
        }

        public override int GetHashCode()
        {
            return Value;
        }
    }
}