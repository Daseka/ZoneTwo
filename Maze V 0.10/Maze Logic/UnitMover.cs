namespace MazeV.Maze_Logic
{
    public class UnitMover
    {
        public bool MovePlayer(Direction direction, Player player, MazeNodeData nodeData, MazeViewData mazeView)
        {
            return DoMovePlayer(direction, player, nodeData, mazeView, false) || DoMovePlayer(player.CurrentMovementDirection, player, nodeData, mazeView, true);
        }

        //TODO: Need to make simpler
        private bool DoMovePlayer(Direction direction, Player player, MazeNodeData nodeData, MazeViewData mazeView, bool tryReverseDirection)
        {
            Location futureLocation = player.CurrentLocation.GetCopy() + mazeView.MovementCube[(int)direction];

            if (Validator.IsFutureLocationValid(player.CurrentLocation, futureLocation, nodeData))
            {
                player.CurrentMovementDirection = direction;
                player.AssignLocation(futureLocation);
                return true;
            }
            else if (tryReverseDirection)
            {
                Direction reverseDirection = GetReverseDirection(direction);
                futureLocation = player.CurrentLocation.GetCopy() + mazeView.MovementCube[(int)reverseDirection];

                if (Validator.IsFutureLocationValid(player.CurrentLocation, futureLocation, nodeData))
                {
                    player.CurrentMovementDirection = reverseDirection;
                    player.FutureMovementDirection = reverseDirection;

                    player.AssignLocation(futureLocation);
                    return true;
                }
            }

            return false;
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