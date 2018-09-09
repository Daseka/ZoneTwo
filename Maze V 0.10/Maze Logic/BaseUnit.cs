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

        public ILocation CurrentLocation { get; set; }
        public ILocation PreviousLocation { get; set; }
        public IDirection CurrentMovementDirection { get; set; }
        public IDirection FutureMovementDirection { get; set; }
        public Axis ViewingAxis { get; set; }


        public void AssignLocation(ILocation location)
        {
            PreviousLocation = CurrentLocation.GetCopy();
            CurrentLocation = location?.GetCopy();
        }

        public void Draw(Graphics graphics, Rectangle rectangle, INode node)
        {
            if (!node.Location.Equals( CurrentLocation) )
                return;
            
            graphics?.FillRectangle(Brushes.Red, rectangle);
        }

        public BaseUnit()
        {
            Id = Guid.NewGuid();
            CurrentLocation = new Location();
            PreviousLocation = new Location();

            CurrentMovementDirection = new UpDirection();
            FutureMovementDirection = new UpDirection();
        }
    }
}
