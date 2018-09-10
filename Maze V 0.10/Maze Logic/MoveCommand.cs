using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MazeV.Maze_Logic
{
    public class MoveCommand : ICommand
    {
        private readonly IUnit fUnit;
        private readonly IDirection fDirection;

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