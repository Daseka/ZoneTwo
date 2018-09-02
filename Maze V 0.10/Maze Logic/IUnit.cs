using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeV.Maze_Logic
{
    public delegate void AssignedHandler();

    public interface IUnit
    {
        Location CurrentLocation { get; }
        Location PreviousLocation { get; }
        Guid Id { get; set; }
        Direction CurrentMovementDirection { get; set; }
        Direction FutureMovementDirection { get; set; }       

        void AssignLocation(Location location);
        void Draw(Graphics graphics, Rectangle rectangle, Node node);
    }
}
