using MazeV.MazeLogic.Units;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeV.MazeLogic
{
    public class DefaultMovementLogic : IMovementLogic
    {
        private readonly Dictionary<Func<IDirection, IUnit, IMazeNodeData, IMazeViewData, bool>, Action<IDirection, IUnit, IMazeViewData>>
            fMovementPriorityList = new Dictionary<Func<IDirection, IUnit, IMazeNodeData, IMazeViewData, bool>, Action<IDirection, IUnit, IMazeViewData>>();

        /// <summary>
        // 1: try move player in last received direction
        // 2: try move player in last succesful move direction
        // 3: try move player in reverse of old direction
        /// </summary>
        public DefaultMovementLogic()
        {
            fMovementPriorityList.Add(TryNewDirection, AssignNewDirection);
            fMovementPriorityList.Add(TryCurrentDirection, AssignCurrentDirection);
            fMovementPriorityList.Add(TryReverseDirection, AssignReverseDirection);
        }

        public Action<IDirection, IUnit, IMazeViewData> GetMovementToPreform(IDirection direction, IUnit player,
            IMazeNodeData nodeData, IMazeViewData mazeView)
        {
            return fMovementPriorityList.FirstOrDefault(kp => kp.Key(direction, player, nodeData, mazeView)).Value;
        }

        private void AssignCurrentDirection(IDirection direction, IUnit player, IMazeViewData mazeView)
        {
            ILocation futureLocation = player.CurrentLocation
                                        .GetCopy()
                                        .Add(mazeView.MovementCube[player.CurrentMovementDirection.Value]);
            player.AssignLocation(futureLocation);
        }

        private void AssignNewDirection(IDirection direction, IUnit player, IMazeViewData mazeView)
        {
            player.CurrentMovementDirection = direction;
            ILocation futureLocation = player.CurrentLocation.GetCopy().Add(mazeView.MovementCube[direction.Value]);
            player.AssignLocation(futureLocation);
        }

        private void AssignReverseDirection(IDirection direction, IUnit player, IMazeViewData mazeView)
        {
            IDirection reverseDirection = player.CurrentMovementDirection.ReverseDirection;
            player.CurrentMovementDirection = reverseDirection;
            player.FutureMovementDirection = reverseDirection;

            ILocation reverseLocation = player.CurrentLocation
                                            .GetCopy()
                                            .Add(mazeView.MovementCube[reverseDirection.Value]);

            player.AssignLocation(reverseLocation);
        }

        private bool TryCurrentDirection(IDirection direction, IUnit player, IMazeNodeData nodeData, IMazeViewData mazeView)
        {
            ILocation location = player.CurrentLocation.GetCopy().Add(mazeView.MovementCube[player.CurrentMovementDirection.Value]);
            return Validator.IsFutureLocationValid(player.CurrentLocation, location, nodeData);
        }

        private bool TryNewDirection(IDirection direction, IUnit player, IMazeNodeData nodeData, IMazeViewData mazeView)
        {
            ILocation location = player.CurrentLocation.GetCopy().Add(mazeView.MovementCube[direction.Value]);
            return Validator.IsFutureLocationValid(player.CurrentLocation, location, nodeData);
        }

        private bool TryReverseDirection(IDirection direction, IUnit player, IMazeNodeData nodeData, IMazeViewData mazeView)
        {
            IDirection reverseDirection = player.CurrentMovementDirection.ReverseDirection;
            ILocation futureLocation = player.CurrentLocation.GetCopy().Add(mazeView.MovementCube[reverseDirection.Value]);
            return Validator.IsFutureLocationValid(player.CurrentLocation, futureLocation, nodeData);
        }
    }
}