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
        ILocation CurrentLocation { get; }
        ILocation PreviousLocation { get; }
        Guid Id { get; set; }
        IDirection CurrentMovementDirection { get; set; }
        IDirection FutureMovementDirection { get; set; }

        void AssignLocation(ILocation location);
        void Draw(Graphics graphics, Rectangle rectangle, INode node);
        IAxis ViewingAxis { get; set; }
    }
}
