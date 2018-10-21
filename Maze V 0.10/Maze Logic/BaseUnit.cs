using System;
using System.Drawing;

namespace MazeV.Maze_Logic
{
    public class BaseUnit : IUnit
    {
        public ILocation CurrentLocation { get; set; }

        public IDirection CurrentMovementDirection { get; set; }

        public IDirection FutureMovementDirection { get; set; }

        public Guid Id { get; set; }

        public ILocation PreviousLocation { get; set; }

        public IAxis ViewingAxis { get; set; }

        public BaseUnit()
        {
            Id = Guid.NewGuid();
            CurrentLocation = new Location();
            PreviousLocation = new Location();

            CurrentMovementDirection = new UpDirection();
            FutureMovementDirection = new UpDirection();
        }

        public void AssignLocation(ILocation location)
        {
            PreviousLocation = CurrentLocation.GetCopy();
            CurrentLocation = location?.GetCopy();
        }

        public void Draw(Graphics graphics, Rectangle rectangle, INode node)
        {
            int x = 6;
            if (!node.Location.Equals(CurrentLocation))
                return;

            graphics?.FillRectangle(Brushes.Red, rectangle);
        }
    }
}