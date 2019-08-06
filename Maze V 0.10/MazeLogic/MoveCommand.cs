using MazeV.MazeLogic.Units;

namespace MazeV.MazeLogic
{
    public class MoveCommand : ICommand
    {
        private readonly IDirection fDirection;
        private readonly IUnit fUnit;

        public MoveCommand(IDirection direction, IUnit unit)
        {
            fUnit = unit;
            fDirection = direction;
        }

        public void Execute()
        {
            fUnit.FutureMovementDirection = fDirection;
        }
    }
}