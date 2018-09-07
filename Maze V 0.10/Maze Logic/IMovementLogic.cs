using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeV.Maze_Logic
{
    public interface IMovementLogic
    {
        Action<Direction, Player, MazeViewData> GetMovementToPreform(Direction direction, Player player, MazeNodeData nodeData, MazeViewData mazeView);
    }

    public class DefaultMovementLogic : IMovementLogic
    {
        private Dictionary<Func<Direction, Player, MazeNodeData, MazeViewData, bool>, Action<Direction,Player,MazeViewData>> 
            fMovementPriorityList = new Dictionary<Func<Direction, Player, MazeNodeData, MazeViewData, bool>, Action<Direction, Player, MazeViewData>>();
        
        /// <summary>
        // 1: try move player in last received direction
        // 2: try move player in last succesful move direction
        // 3: try move player in reverse of old direction 
        /// </summary>
        public DefaultMovementLogic()
        {           
            fMovementPriorityList.Add(TryNewDirection, AssignNewDirection );
            fMovementPriorityList.Add(TryCurrentDirection, AssignCurrentDirection );
            fMovementPriorityList.Add(TryReverseDirection, AssignReverseDirection);
        }

        public Action<Direction, Player, MazeViewData> GetMovementToPreform(Direction direction, Player player, MazeNodeData nodeData, MazeViewData mazeView)
        {
            return fMovementPriorityList.Where((kp => kp.Key(direction, player, nodeData, mazeView))).FirstOrDefault().Value;
        }

        private bool TryNewDirection(Direction direction, Player player, MazeNodeData nodeData, MazeViewData mazeView)
        {
            Location location = player.CurrentLocation.GetCopy() + mazeView.MovementCube[(int)direction];
            return Validator.IsFutureLocationValid(player.CurrentLocation, location, nodeData);
        }

        private void AssignNewDirection(Direction direction, Player player, MazeViewData mazeView)
        {
            player.CurrentMovementDirection = direction;
            Location futureLocation = player.CurrentLocation.GetCopy() + mazeView.MovementCube[(int)direction];
            player.AssignLocation(futureLocation);
        }

        private bool TryCurrentDirection(Direction direction, Player player, MazeNodeData nodeData, MazeViewData mazeView)
        {
            Location location = player.CurrentLocation.GetCopy() + mazeView.MovementCube[(int)player.CurrentMovementDirection];
            return Validator.IsFutureLocationValid(player.CurrentLocation, location, nodeData);
        }

        private void AssignCurrentDirection(Direction direction, Player player, MazeViewData mazeView)
        {
            player.CurrentMovementDirection = player.CurrentMovementDirection;
            Location futureLocation = player.CurrentLocation.GetCopy() + mazeView.MovementCube[(int)player.CurrentMovementDirection];
            player.AssignLocation(futureLocation);
        }
        
        private bool TryReverseDirection(Direction direction, Player player, MazeNodeData nodeData, MazeViewData mazeView)
        {
            Direction reverseDirection = GetReverseDirection(player.CurrentMovementDirection);
            Location futureLocation = player.CurrentLocation.GetCopy() + mazeView.MovementCube[(int)reverseDirection];
            return Validator.IsFutureLocationValid(player.CurrentLocation, futureLocation, nodeData);
        }

        private void AssignReverseDirection(Direction direction, Player player, MazeViewData mazeView)
        {
            Direction reverseDirection = GetReverseDirection(player.CurrentMovementDirection);
            player.CurrentMovementDirection = reverseDirection;
            player.FutureMovementDirection = reverseDirection;
            
            Location reverseLocation = player.CurrentLocation.GetCopy() + mazeView.MovementCube[(int)reverseDirection];

            player.AssignLocation(reverseLocation);
        }

        private Direction GetReverseDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return Direction.Right;

                case Direction.Right:
                    return Direction.Left;

                case Direction.Up:
                    return Direction.Down;

                case Direction.Down:
                default:
                    return Direction.Up;
            }
        }
    }

}