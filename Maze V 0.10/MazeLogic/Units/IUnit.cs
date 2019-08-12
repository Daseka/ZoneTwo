using MazeV.MazeLogic.Drawing;
using MazeV.MazeLogic.MazeNodes;
using MazeV.MazeLogic.Movement.Directions;
using MazeV.MazeLogic.Rotation;
using System;
using System.Drawing;

namespace MazeV.MazeLogic.Units
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

        void Draw(IMazeGraphic graphics, Rectangle rectangle, INode node);
    }
}