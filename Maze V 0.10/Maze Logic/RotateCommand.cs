using System.Collections.Generic;

namespace MazeV.Maze_Logic
{
    public class RotateCommand : ICommand
    {
        private readonly Rotation fRotation;
        private readonly Player fPlayer;
        private readonly MazeViewData fMazeView;
        private readonly Dictionary<Location, Node> fNodesByLocation;

        public RotateCommand(Rotation rotation, Player player, MazeViewData mazeView, MazeNodeData nodeData)
        {
            fRotation = rotation;
            fPlayer = player;
            fMazeView = mazeView;
            fNodesByLocation = nodeData.NodesByLocation;
        }

        public void Execute()
        {
            MazeViewRotator rotator = new MazeViewRotator();
            rotator.RotateView(fRotation, fPlayer, fMazeView, fNodesByLocation);
        }
    }
}