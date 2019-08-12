namespace MazeV.MazeLogic.Movement.Directions
{
    public sealed class DownDirection : IDirection
    {
        public IDirection ReverseDirection => new UpDirection();

        public int Value => 3;

        public bool Equals(IDirection other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DownDirection);
        }

        public override int GetHashCode()
        {
            return Value;
        }
    }
}