using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeV.Maze_Logic
{
    public interface IVisualizer
    {
        void Draw(MazeViewData mazeView,UnitList unitList);
    }

}