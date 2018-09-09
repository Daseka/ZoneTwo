using System.Collections.Generic;

namespace MazeV.Maze_Logic
{
    public class RotateCommand : ICommand
    {
        private readonly Rotation fRotation;
        private readonly IUnit fPlayer;
        private readonly IMazeViewData fMazeView;
        private readonly Dictionary<ILocation, INode> fNodesByLocation;

        public RotateCommand(Rotation rotation, IUnit player, IMazeViewData mazeView, MazeNodeData nodeData)
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