using MazeV.MazeLogic.Drawing;
using System;
using System.Drawing;

namespace MazeV.MazeLogic.Units
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

        public void Draw(IMazeGraphic graphics, Rectangle rectangle, INode node)
        {
            if (!node.Location.Equals(CurrentLocation))
            {
                return;
            }

            graphics?.FillRectangle(Brushes.Red, rectangle);
        }
    }
}