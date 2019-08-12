namespace MazeV.MazeLogic.Movement.Directions
{
    public sealed class UpDirection : IDirection
    {
        public IDirection ReverseDirection => new DownDirection();

        public int Value => 2;

        public bool Equals(IDirection other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as UpDirection);
        }

        public override int GetHashCode()
        {
            return Value;
        }
    }
}