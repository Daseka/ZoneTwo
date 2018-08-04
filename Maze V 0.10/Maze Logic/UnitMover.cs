using System;
using System.Collections.Generic;


namespace MazeV.Maze_Logic
{
    public class UnitMover
    {
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

        //TODO: Need to make simpler
        private bool DoMovePlayer(Direction direction, Player player, Dictionary<int, Node> listOfNodes, MazeViewData mazeView,bool tryReverseDirection)
        {
            Location futureLocation = player.CurrentLocation.GetCopy() + mazeView.MovementCube[(int)direction];            

            if (Validator.IsFutureLocationValid(player.CurrentLocation, futureLocation, listOfNodes))
            {
                player.CurrentMovementDirection = direction;
                player.AssignLocation(futureLocation);
                return true;
            }
            else if(tryReverseDirection)
            {
                Direction reverseDirection = GetReverseDirection(direction);
                futureLocation = player.CurrentLocation.GetCopy() + mazeView.MovementCube[(int)reverseDirection];

                if (Validator.IsFutureLocationValid(player.CurrentLocation,futureLocation,listOfNodes))
                {
                    player.CurrentMovementDirection = reverseDirection;
                    player.FutureMovementDirection = reverseDirection;

                    player.AssignLocation(futureLocation);
                    return true;
                }
            }                        

            return false;
        }

        public bool MovePlayer(Direction direction, Player player, Dictionary<int, Node> listOfNodes, MazeViewData mazeView)
        {
            if (!DoMovePlayer(direction, player, listOfNodes, mazeView,false))
            {
                if (!DoMovePlayer(player.CurrentMovementDirection, player, listOfNodes, mazeView, true))
                    return false;
            }

            return true;
        }
    }

}