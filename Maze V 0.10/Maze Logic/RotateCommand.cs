using System.Collections.Generic;

namespace MazeV.Maze_Logic
{
    public class RotateCommand : ICommand
    {
        private readonly IAxisFactory fAxisFactory;
        private readonly IMazeViewData fMazeView;
        private readonly Dictionary<ILocation, INode> fNodesByLocation;
        private readonly IUnit fPlayer;
        private readonly IRotation fRotation;

        public RotateCommand(IRotation rotation, IUnit player, IMazeViewData mazeView, IMazeNodeData nodeData, IAxisFactory factory)
        {
            fRotation = rotation;
            fPlayer = player;
            fMazeView = mazeView;
            fNodesByLocation = nodeData.NodesByLocation;
            fAxisFactory = factory;
        }

        public void Execute()
        {
            MazeViewRotator rotator = new MazeViewRotator(fAxisFactory);
            rotator.RotateView(fRotation, fPlayer, fMazeView, fNodesByLocation);
        }
    }
}