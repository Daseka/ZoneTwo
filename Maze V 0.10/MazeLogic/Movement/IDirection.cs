namespace MazeV.MazeLogic.Movement
{
    public interface IDirection
    {
        IDirection ReverseDirection { get; }

        int Value { get; }
    }
}