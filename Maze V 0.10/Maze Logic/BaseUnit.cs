using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeV.Maze_Logic
{
    public class BaseUnit : IUnit
    {
        public Guid Id { get; set; }

        public Location CurrentLocation { get; set; }
        public Location PreviousLocation { get; set; }
        public Direction CurrentMovementDirection { get; set; }
        public Direction FutureMovementDirection { get; set; }       

        public void AssignLocation(Location location)
        {
            PreviousLocation = CurrentLocation.GetCopy();
            CurrentLocation = location.GetCopy();
        }

        public void Draw(Graphics graphics, Rectangle rectangle, Node node)
        {
            if (node.Location != CurrentLocation)
                return;
            
            graphics.FillRectangle(Brushes.Red, rectangle);
        }

        public BaseUnit()
        {
            Id = Guid.NewGuid();
            CurrentLocation = new Location();
            PreviousLocation = new Location();
        }
    }
}
