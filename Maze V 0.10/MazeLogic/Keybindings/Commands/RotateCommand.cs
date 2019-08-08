using MazeV.MazeLogic.MazeNodes;
using MazeV.MazeLogic.MazeViews;
using MazeV.MazeLogic.Rotation;
using MazeV.MazeLogic.Units;
using System.Collections.Generic;

namespace MazeV.MazeLogic.Keybindings.Commands
{
    public class RotateCommand : ICommand
    {
        private readonly IAxisFactory _axisFactory;
        private readonly IMazeViewData _mazeView;
        private readonly Dictionary<ILocation, INode> _nodesByLocation;
        private readonly IUnit _player;
        private readonly IRotation _rotation;

        public RotateCommand(
            IRotation rotation, 
            IUnit player, 
            IMazeViewData mazeView, 
            IMazeNodeData nodeData, 
            IAxisFactory factory)
        {
            _rotation = rotation;
            _player = player;
            _mazeView = mazeView;
            _nodesByLocation = nodeData.NodesByLocation;
            _axisFactory = factory;
        }

        public void Execute()
        {
            var rotator = new MazeViewRotator(_axisFactory);
            rotator.RotateView(_rotation, _player, _mazeView, _nodesByLocation);
        }
    }
}