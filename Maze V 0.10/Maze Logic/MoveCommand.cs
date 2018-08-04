using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MazeV.Maze_Logic
{

    public class MoveCommand : ICommand
    {
        private IUnit fUnit;
        private Direction fDirection;

        public MoveCommand(Direction direction, IUnit unit)
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