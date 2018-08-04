using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MazeV.Maze_Logic
{
    public interface ICommand
    {
        void Execute();
    }

}