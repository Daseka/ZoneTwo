using MazeV.MazeLogic.Movement;
using MazeV.MazeLogic.Units;

namespace MazeV.MazeLogic.Keybindings.Commands
{
    public class MoveCommand : ICommand
    {
        private readonly IDirection _direction;
        private readonly IUnit _unit;

        public MoveCommand(IDirection direction, IUnit unit)
        {
            _unit = unit;
            _direction = direction;
        }

        public void Execute()
        {
            _unit.FutureMovementDirection = _direction;
        }
    }
}