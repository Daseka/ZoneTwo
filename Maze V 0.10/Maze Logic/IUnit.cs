using System;
using System.Drawing;

namespace MazeV.Maze_Logic
{
    public delegate void AssignedHandler();

    public interface IUnit
    {
        ILocation CurrentLocation { get; }

        IDirection CurrentMovementDirection { get; set; }

        IDirection FutureMovementDirection { get; set; }

        Guid Id { get; set; }

        ILocation PreviousLocation { get; }

        IAxis ViewingAxis { get; set; }

        void AssignLocation(ILocation location);

        void Draw(Graphics graphics, Rectangle rectangle, INode node);
    }
}